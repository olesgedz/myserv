using System;
using System.Text;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace MqttSubscriver
{
    public class Program
    {

        private const string IotEndpoint = "farmer.cloudmqtt.com";
        private const int BrokerPort = 11946; // no SSL 1883 or SSL 8883;
        private const string user = "dtfktlxh";
        private const string pw = "tGbRX26mxSBx";


        public static void Main(string[] args)
        {
            var subscriber = new Program();
            subscriber.Subscribe();
        }

        public void Subscribe()
        {
            //convert to pfx using openssl
            //you'll need to add these two files to the project and copy them to the output
            //var clientCert = new X509Certificate2("YOURPFXFILE.pfx", "YOURPFXFILEPASSWORD");
            //this is the AWS caroot.pem file that you get as part of the download
            //var caCert = X509Certificate.CreateFromSignedFile("root.pem"); // this doesn't have to be a new X509 type...
            //var client = new MqttClient(IotEndpoint, BrokerPort, true, caCert, clientCert, MqttSslProtocols.None);

            var client = new MqttClient(IotEndpoint, BrokerPort, false, null, null, MqttSslProtocols.None);

            //event handler for inbound messages
            client.MqttMsgPublishReceived += ClientMqttMsgPublishReceived;

            //client id here is totally arbitary, but I'm pretty sure you can't have more than one client named the same.
            client.Connect("listener", user, pw);


            // '#' is the wildcard to subscribe to anything under the 'root' topic
            // the QOS level here - I only partially understand why it has to be this level - it didn't seem to work at anything else.
            client.Subscribe(new[] { "test" }, new[] { MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE });

            while (true)
            {
                //listen good!
            }

        }

        public static void ClientMqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            Console.WriteLine("We received a message...");
            Console.WriteLine(Encoding.UTF8.GetChars(e.Message));
        }
    }
}