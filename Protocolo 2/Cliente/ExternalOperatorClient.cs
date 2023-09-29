using System;
using System.Threading.Tasks;
using Greeter;
using Grpc.Core;
using Provisioning;

namespace Client
{
    public class ExternalOperatorClient
    {
        private readonly ProvisioningService.ProvisioningServiceClient _client;

        public ExternalOperatorClient(ProvisioningService.ProvisioningServiceClient client)
        {
            _client = client;
        }

        public void Reserve(string modality, string address)
        {
            // Implemente a lógica de reserva para o operador externo aqui
        }

        public void Activate(int reservationNumber)
        {
            // Implemente a lógica de ativação para o operador externo aqui
        }

        public void Deactivate(int reservationNumber)
        {
            // Implemente a lógica de desativação para o operador externo aqui
        }

        public void Terminate(int reservationNumber)
        {
            // Implemente a lógica de terminação para o operador externo aqui
        }
    }
}
