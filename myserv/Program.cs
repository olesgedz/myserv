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
            var subscriber = new Client();
            subscriber.Subscribe();
           
        }

      


    }
}