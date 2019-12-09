using System;
using System.Text;
using System.Windows;
using System.Windows.Forms;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace myserv
{
    public class Program
    {
        public static void Main(string[] args)
        { 
            var subscriber = new Client("farmer.cloudmqtt.com",11946, "Oles");
            subscriber.Connect();
            subscriber.Subscribe("test");
            Chat chat = new Chat(ref subscriber);
            chat.Loop();
        }
    }
}