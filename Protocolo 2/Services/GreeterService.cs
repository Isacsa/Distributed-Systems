using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.AspNetCore.Connections;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Provisioning.Protos;

namespace Greeter
{
    public class ProvisioningService : Provisioning.ProvisioningService.ProvisioningServiceBase
    {
        private readonly ILogger<ProvisioningService> _logger;
        private readonly IConnection _rabbitMqConnection;
        private readonly IModel _rabbitMqChannel;
        private readonly EventingBasicConsumer _consumer;

        public ProvisioningService(ILogger<ProvisioningService> logger)
        {
            _logger = logger;

            var factory = new ConnectionFactory() { HostName = "localhost" };
            _rabbitMqConnection = factory.CreateConnection();
            _rabbitMqChannel = _rabbitMqConnection.CreateModel();
            _rabbitMqChannel.QueueDeclare(queue: "events",
                                         durable: false,
                                         exclusive: false,
                                         autoDelete: false,
                                         arguments: null);

            _consumer = new EventingBasicConsumer(_rabbitMqChannel);
        }

        public override Task<ReservationResponse> Reserve(ReservationRequest request, ServerCallContext context)
        {
            // Implemente a lógica de reserva aqui
        }

        public override Task<ActivationResponse> Activate(ActivationRequest request, ServerCallContext context)
        {
            // Implemente a lógica de ativação aqui
        }

        public override Task<DeactivationResponse> Deactivate(DeactivationRequest request, ServerCallContext context)
        {
            // Implemente a lógica de desativação aqui
        }

        public override Task<TerminationResponse> Terminate(TerminationRequest request, ServerCallContext context)
        {
            // Implemente a lógica de terminação aqui
        }
    }
}
