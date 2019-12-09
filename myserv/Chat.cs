using System;
using System.Collections.Generic;
using System.Text;

namespace myserv
{
    class Chat
    {
        List<String> userList;
        Client client;
        private string input;
        private string topic;
        public Chat(ref Client client)
        {
           this.client = client;
        }

        public void Loop()
        {
            
            while (true)
            {
                if (Console.KeyAvailable)
                {
                    input = Console.ReadLine();
                    switch(input)
                    {
                        case "SUBSCRIBE":
                            Console.WriteLine("Enter topic:");
                            client.Subscribe(Console.ReadLine());
                            break;
                        case "PUBLISH":
                            Console.WriteLine("Enter new topic:");
                            topic = Console.ReadLine();
                            break;
                        default:
                            client.Publish(topic, input);
                            break;
                    }
                }
            }
        }
    }
}
