using System;
using System.Threading.Tasks;
using Grpc.Net.Client;

using grpc_server;

namespace grpc_client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.Write("Please enter your name: ");
            var name = Console.ReadLine();
            Console.Write("Please enter your birthday: ");
            var birthday = Console.ReadLine();
            var req = new PersonInfo() {
                Name = name,
                Birthday = birthday
            };

            var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new BirthdayGreeter.BirthdayGreeterClient(channel);
            var reply = await client.GreetWithBirthdayMessageAsync(req);

            Console.WriteLine(reply.Message);
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            await channel.ShutdownAsync();
        }
    }
}
