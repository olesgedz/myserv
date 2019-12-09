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
            Console.WriteLine("Enter your login:");
            String   login = Console.ReadLine();
            var subscriber = new Client("farmer.cloudmqtt.com",11946,login);
            subscriber.Connect();
            Chat chat = new Chat(ref subscriber);
            chat.Loop();
        }
    }
}