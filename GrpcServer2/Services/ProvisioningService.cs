//using Grpc.Core;
//using GrpcServer2.Models;
//using Microsoft.EntityFrameworkCore;
//using GrpcServer2.Services;
//using GrpcServer2.Data;
//using GrpcServer2.Protos;

//namespace GrpcServer2.Services
//{
//     public class ProvisioningService : Provisioning.ProvisioningBase
//    {
//        private readonly GrpcProtocolo2Context _dbContext;
//        private readonly ILogger<ProvisioningService> _logger;
//        public ProvisioningService(ILogger<ProvisioningService> logger, GrpcProtocolo2Context dBContext)
//        {
//            _dbContext = dBContext;
//            _logger = logger;
//        }
//        public override Task<ReservaReply> Reserva(ReservaRequesito request, ServerCallContext context)
//    {
//        try
//        {
//            var user = _dbContext.Users.FirstOrDefault(u => u.Username == request.UserID);
//            if (user == null || user.Password != request.Passw)
//            {
//                return Task.FromResult(new ReservaReply { Result = "Usuário ou senha incorretos." });
//            }
//            else
//            {
//                var domicilio = _dbContext.Coberturas.FirstOrDefault(d => d.Rua == request.Rua && d.Numero == request.Num);
//                if (domicilio == null || domicilio.Estado != "FREE")
//                {
//                    return Task.FromResult(new ReservaReply { Result = "Domicílio não disponível para reserva." });

//                }

//                domicilio.Operator = user.Operator;
//                domicilio.Estado = "RESERVED";
//                _dbContext.Coberturas.Update(domicilio);
//                _dbContext.SaveChanges();

//                var operacao = new Operaco
//                {
//                    Operacao = "RESERVATION",
//                    Operador = user.Operator,
//                    NumAdministrativo = domicilio.NumAdministrativo,
//                    Dataatual = DateTime.UtcNow
//                };
//                _dbContext.Operacoes.Add(operacao);

//                _dbContext.SaveChanges();

//                return Task.FromResult(new ReservaReply { Result = "Reserva efetuada com sucesso.", NumAdministrativo = domicilio.NumAdministrativo });
//            }
//        }
//        catch (Exception ex)
//        {
//            // Log the exception or return an appropriate error message to the client
//            Console.WriteLine($"An error occurred: {ex.Message}");

//            throw new RpcException(new Status(StatusCode.Internal, "An error occurred while processing the request"));
//        }
//    }
//#endregion

//    #region Serviço de Ativação
//    public override async Task<ActivarReply> Activar(ActivarRequesito request, ServerCallContext context)
//    {
//        try
//        {
//            var user = _dbContext.Users.FirstOrDefault(u => u.Username == request.UserID);
//            if (user == null || user.Password != request.Passw)
//            {
//                return new ActivatarReply { Result = "Usuário ou senha incorretos." };
//            }
//            else
//            {
//                var domicilio = _dbContext.Coberturas.FirstOrDefault(d => d.NumAdministrativo == request.NumAdministrativo);
//                if (domicilio == null || (domicilio.Estado != "RESERVED" && domicilio.Estado != "DEACTIVATED") || domicilio.Operator != user.Operator)
//                {
//                    return new ActivarReply { Result = "Domicílio não disponível para ativação." };

//                }
//                int estimatedTime = 5; // Tempo estimado em segundos
//                await ActivateServiceAsync(user, domicilio, estimatedTime);
//                return new ActivarReply { Result = "Ativação iniciada", ExpectedActivationTime = estimatedTime };
//            }
//        }
//        catch (Exception ex)
//        {
//            // Log the exception or return an appropriate error message to the client
//            Console.WriteLine($"An error occurred: {ex.Message}");

//            throw new RpcException(new Status(StatusCode.Internal, "An error occurred while processing the request"));
//        }
//    }
//    private async Task ActivarServiceAsync(User user, Cobertura cobertura, int estimatedTime)
//    {
//        // Simular tempo de ativação
//        await Task.Delay(TimeSpan.FromSeconds(estimatedTime));

//        // Atualizar estado da cobertura
//        cobertura.Estado = "ACTIVATED";
//        _dbContext.Coberturas.Update(cobertura);
//        await _dbContext.SaveChangesAsync();

//        // Adicionar linha à tabela Operacoes
//        var operacao = new Operaco
//        {
//            Operacao = "ACTIVATION",
//            Operador = user.Operator,
//            NumAdministrativo = cobertura.NumAdministrativo,
//            Dataatual = DateTime.UtcNow
//        };
//        _dbContext.Operacoes.Add(operacao);
//        await _dbContext.SaveChangesAsync();

//        // Publicar mensagem no tópico EVENTS do RabbitMQ
//        //FALTA DEFINIR ESTE MÉTODO
//    }
//    #endregion

//    #region Serviço de Desativação
//    public override async Task<DesactivarReply> Deactivar(DesactivarRequesito request, ServerCallContext context)
//    {
//        try
//        {
//            var user = _dbContext.Users.FirstOrDefault(u => u.Username == request.UserID);
//            if (user == null || user.Password != request.Passw)
//            {
//                return new DesactivarReply { Result = "Usuário ou senha incorretos." };
//            }
//            else
//            {
//                var domicilio = _dbContext.Coberturas.FirstOrDefault(d => d.NumAdministrativo == request.NumAdministrativo);
//                if (domicilio == null || domicilio.Estado != "ACTIVATED" || domicilio.Operator != user.Operator)
//                {
//                    return new DeactivarReply { Result = "Domicílio não disponível para desativação." };

//                }
//                int estimatedTime = 5; // Tempo estimado em segundos
//                await DeactivarServiceAsync(user, domicilio, estimatedTime);
//                return new DeactivarReply { Result = "Desativação iniciada", ExpectedActivationTime = estimatedTime };
//            }
//        }
//        catch (Exception ex)
//        {
//            // Log the exception or return an appropriate error message to the client
//            Console.WriteLine($"An error occurred: {ex.Message}");

//            throw new RpcException(new Status(StatusCode.Internal, "An error occurred while processing the request"));
//        }
//    }
//    private async Task DeactivarServiceAsync(User user, Cobertura cobertura, int estimatedTime)
//    {
//        // Simular tempo de ativação
//        await Task.Delay(TimeSpan.FromSeconds(estimatedTime));

//        // Atualizar estado da cobertura
//        cobertura.Estado = "DEACTIVATED";
//        _dbContext.Coberturas.Update(cobertura);
//        await _dbContext.SaveChangesAsync();

//        // Adicionar linha à tabela Operacoes
//        var operacao = new Operaco
//        {
//            Operacao = "DEACTIVATION",
//            Operador = user.Operator,
//            NumAdministrativo = cobertura.NumAdministrativo,
//            Dataatual = DateTime.UtcNow
//        };
//        _dbContext.Operacoes.Add(operacao);
//        await _dbContext.SaveChangesAsync();

//        // Publicar mensagem no tópico EVENTS do RabbitMQ
//        //FALTA DEFINIR ESTE MÉTODO
//    }
//        #endregion

//        #region Serviço de Término
//        public override async Task<TerminarReply> Terminar(TerminarRequesito request, ServerCallContext context)
//    {
//        try
//        {
//            var user = _dbContext.Users.FirstOrDefault(u => u.Username == request.UserID);
//            if (user == null || user.Password != request.Passw)
//            {
//                return new TerminarReply { Result = "Usuário ou senha incorretos." };
//            }
//            else
//            {
//                var domicilio = _dbContext.Coberturas.FirstOrDefault(d => d.NumAdministrativo == request.NumAdministrativo);
//                if (domicilio == null || domicilio.Estado != "DEACTIVATED" || domicilio.Operator != user.Operator)
//                {
//                    return new TerminarReply { Result = "Domicílio não disponível para término." };

//                }
//                int estimatedTime = 5; // Tempo estimado em segundos
//                await TerminarServiceAsync(user, domicilio, estimatedTime);
//                return new TerminarReply { Result = "Término iniciado", ExpectedActivationTime = estimatedTime };
//            }
//        }
//        catch (Exception ex)
//        {
//            // Log the exception or return an appropriate error message to the client
//            Console.WriteLine($"An error occurred: {ex.Message}");

//            throw new RpcException(new Status(StatusCode.Internal, "An error occurred while processing the request"));
//        }
//    }
//    private async Task TerminarServiceAsync(User user, Cobertura cobertura, int estimatedTime)
//    {
//        // Simular tempo de ativação
//        await Task.Delay(TimeSpan.FromSeconds(estimatedTime));

//        // Atualizar estado da cobertura
//        cobertura.Estado = "FREE";
//        cobertura.Operator = null;
//        cobertura.Modalidade = null;

//        _dbContext.Coberturas.Update(cobertura);
//        await _dbContext.SaveChangesAsync();

//        // Adicionar linha à tabela Operacoes
//        var operacao = new Operaco
//        {
//            Operacao = "TERMINATION",
//            Operador = user.Operator,
//            NumAdministrativo = cobertura.NumAdministrativo,
//            Dataatual = DateTime.UtcNow
//        };
//        _dbContext.Operacoes.Add(operacao);
//        await _dbContext.SaveChangesAsync();

//        // Publicar mensagem no tópico EVENTS do RabbitMQ
//        //FALTA DEFINIR ESTE MÉTODO
//    }
//    #endregion
//}
//}

