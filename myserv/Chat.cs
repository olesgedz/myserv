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
                    try
                    {
                     input = Console.ReadLine();
                    }
                    catch
                    {
                        Console.WriteLine("Empty input");
                    }
                    switch(input)
                    {
                        case "SUBSCRIBE":
                            String line;
                            try
                            {
                                Console.WriteLine("Enter topic:");
                                line = Console.ReadLine();
                                if(line != null && line.Length > 0)
                                    client.Subscribe(line);
                            }
                            catch (Exception)
                            {
                                Console.WriteLine("Empty input");
                            }
                            break;
                        case "PUBLISH":
                            Console.WriteLine("Enter new topic:");
                            try
                            {
                                topic = Console.ReadLine();
                            }
                            catch
                            {
                                Console.WriteLine("Empty input");
                            }
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
