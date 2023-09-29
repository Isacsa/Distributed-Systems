using System;
using System.Threading.Channels;
using System.Threading.Tasks;
using Grpc.Core;
using Greeter;
using Provisioning;

namespace Client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var channel = new Channel("localhost", 5001, ChannelCredentials.Insecure);
            var client = new ProvisioningService.ProvisioningServiceClient(channel);

            // Implemente o código do cliente "Administrador/Operador" aqui

            channel.ShutdownAsync().Wait();
            Console.WriteLine("Pressione qualquer tecla para sair...");
            Console.ReadKey();
        }
    }
}
