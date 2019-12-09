using System;
using System.Collections.Generic;
using System.Text;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace myserv
{
    class Client
    {
        private string IotEndpoint = "farmer.cloudmqtt.com";
        private int BrokerPort = 11946; // no SSL 1883 or SSL 8883;
        private const string user = "dtfktlxh";
        private const string pw = "tGbRX26mxSBx";
        private MqttClient client;
        private string clientid;
        public string my_chat;
        public Client(string address, int port, string clientid )
        {
            IotEndpoint = address;
            BrokerPort = port;
            this.clientid = clientid;
        }

        public void Connect()
        {
            //convert to pfx using openssl
            //you'll need to add these two files to the project and copy them to the output
            //var clientCert = new X509Certificate2("YOURPFXFILE.pfx", "YOURPFXFILEPASSWORD");
            //this is the AWS caroot.pem file that you get as part of the download
            //var caCert = X509Certificate.CreateFromSignedFile("root.pem"); // this doesn't have to be a new X509 type...
            //var client = new MqttClient(IotEndpoint, BrokerPort, true, caCert, clientCert, MqttSslProtocols.None);

            client = new MqttClient(IotEndpoint, BrokerPort, false, null, null, MqttSslProtocols.None);

            //event handler for inbound messages
            client.MqttMsgPublishReceived += ClientMqttMsgPublishReceived;

            //client id here is totally arbitary, but I'm pretty sure you can't have more than one client named the same.
            client.Connect(clientid, user, pw);
            Subscribe("users/all");
            Publish("users/all", clientid);
            Subscribe(clientid);

        }
        public void Subscribe(string topic)
        {
            // '#' is the wildcard to subscribe to anything under the 'root' topic
            // the QOS level here - I only partially understand why it has to be this level - it didn't seem to work at anything else.
            client.Subscribe(new[] { topic }, new[] { MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE });
        }

        public void Publish(string topic, string message)
        {
            if (topic == "users/all")
                client.Publish(topic, Encoding.UTF8.GetBytes(message));
            else
                client.Publish(topic, Encoding.UTF8.GetBytes(clientid + ": " + message));
        }
        public void Listen()
        {
            //while (true)
            //{
                
            //    if (Console.ReadKey().Key == ConsoleKey.T)
            //    {
            //        client.Publish("test", Encoding.UTF8.GetBytes("Hello"));
            //    }
            //}
        }

        public void ClientMqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
           //Console.WriteLine(sender);
            //Console.WriteLine(e.Topic);
            if (e.Topic == "users/all")
            {
                Console.Write("New user connected to broker: ");
                Console.WriteLine(Encoding.UTF8.GetChars(e.Message));
                //Subscribe(Encoding.UTF8.GetChars(e.Message).ToString());
                //Console.Write("Subscribed to new user: "); // reading other user messages
                //Console.WriteLine(Encoding.UTF8.GetChars(e.Message));
            }
            else
                Console.WriteLine(Encoding.UTF8.GetChars(e.Message));
        }

    }
}
