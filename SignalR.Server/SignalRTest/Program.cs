using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalRTest
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var client = SignalRHubClient.Instance;
            try
            {
                await client.Connect();

                client.OnReceiveEvent += (userId, message) =>
                {
                    Console.WriteLine(userId + ":" + message);
                };

                Task.WaitAll();

                await client.Send("123456", "hello world");
            }
            catch (Exception e)
            {
            }

            Console.ReadLine();
        }

    }
}
