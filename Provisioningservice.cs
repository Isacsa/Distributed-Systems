namespace GrpcServer2.Services
{
    public class ProvisioningService : Provisioning.ProvisioningBase
    {
        private readonly SDfinalDatabaseContext _dbContext;
        private readonly ILogger<ProvisioningService> _logger;

        public ProvisioningService(ILogger<ProvisioningService> logger, SDfinalDatabaseContext dbContext)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public override Task<RespostaReserva> Reserva(RequisicaoReserva request, ServerCallContext context)
        {
            try
            {
                var user = _dbContext.Users.FirstOrDefault(u => u.Username == request.UserId);
                if (user == null || user.Password != request.Passw)
                {
                    return Task.FromResult(new RespostaReserva { NumAdministrativo = 0, Resultado = "Usuario ou senha incorretos." });
                }
                else
                {
                    var domicilio = _dbContext.Coverages.FirstOrDefault(d => d.Rua == request.Rua && d.Num == request.Num);
                    if (domicilio == null || domicilio.Estado != "FREE")
                    {
                        return Task.FromResult(new RespostaReserva { NumAdministrativo = 0, Resultado = "Domicilio nao disponivel para reserva." });
                    }

                    domicilio.Operator = user.Operator;
                    domicilio.Estado = "RESERVED";
                    _dbContext.Coverages.Update(domicilio);
                    _dbContext.SaveChanges();

                    var operacao = new Operaco
                    {
                        Operacao = "RESERVATION",
                        Operador = user.Operator,
                        NumAdministrativo = domicilio.NumAdministrativo,
                        Dataatual = DateTime.UtcNow
                    };
                    _dbContext.Operacoes.Add(operacao);
                    _dbContext.SaveChanges();

                    return Task.FromResult(new RespostaReserva { NumAdministrativo = domicilio.NumAdministrativo, Resultado = "Reserva efetuada com sucesso." });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw new RpcException(new Status(StatusCode.Internal, "An error occurred while processing the request"));
            }
        }

        public override async Task<RespostaAtivar> Ativar(RequisicaoAtivar request, ServerCallContext context)
        {
            try
            {
                var user = _dbContext.Users.FirstOrDefault(u => u.Username == request.UserId);
                if (user == null || user.Password != request.Passw)
                {
                    return new RespostaAtivar { PodeAtivar = false, TempoAtivacaoEsperado = 0, Resultado = "Usuario ou senha incorretos." };
                }
                else
                {
                    var domicilio = _dbContext.Coverages.FirstOrDefault(d => d.NumAdministrativo == request.NumAdministrativo);
                    if (domicilio == null || (domicilio.Estado != "RESERVED" && domicilio.Estado != "DEACTIVATED") || domicilio.Operator != user.Operator)
                    {
                        return new RespostaAtivar { PodeAtivar = false, TempoAtivacaoEsperado = 0, Resultado = "Domicilio nao disponivel para ativacao." };
                    }

                    int tempoAtivacaoEsperado = 5; // Tempo estimado em segundos
                    await AtivarServicoAsync(user, domicilio, tempoAtivacaoEsperado);
                    return new RespostaAtivar { PodeAtivar = true, TempoAtivacaoEsperado = tempoAtivacaoEsperado, Resultado = "Ativacao iniciada" };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw new RpcException(new Status(StatusCode.Internal, "An error occurred while processing the request"));
            }
        }

        private async Task AtivarServicoAsync(User user, Coverage coverage, int tempoAtivacaoEsperado)
        {
            await Task.Delay(TimeSpan.FromSeconds(tempoAtivacaoEsperado));

            coverage.Estado = "ACTIVATED";
            _dbContext.Coverages.Update(coverage);
            await _dbContext.SaveChangesAsync();

            var operacao = new Operaco
            {
                Operacao = "ACTIVATION",
                Operador = user.Operator,
                NumAdministrativo = coverage.NumAdministrativo,
                Dataatual = DateTime.UtcNow
            };
            _dbContext.Operacoes.Add(operacao);
            await _dbContext.SaveChangesAsync();

            // Implementar logica de publicacao de mensagem no topico EVENTS do RabbitMQ
        }

        public override async Task<RespostaDesativar> Desativar(RequisicaoDesativar request, ServerCallContext context)
        {
            try
            {
                var user = _dbContext.Users.FirstOrDefault(u => u.Username == request.UserId);
                if (user == null || user.Password != request.Passw)
                {
                    return new RespostaDesativar { PodeAtivar = false, TempoAtivacaoEsperado = 0, Resultado = "Usuario ou senha incorretos." };
                }
                else
                {
                    var domicilio = _dbContext.Coverages.FirstOrDefault(d => d.NumAdministrativo == request.NumAdministrativo);
                    if (domicilio == null || domicilio.Estado != "ACTIVATED" || domicilio.Operator != user.Operator)
                    {
                        return new RespostaDesativar { PodeAtivar = false, TempoAtivacaoEsperado = 0, Resultado = "Domicilio nao disponivel para desativacao." };
                    }

                    int tempoAtivacaoEsperado = 5; // Tempo estimado em segundos
                    await DesativarServicoAsync(user, domicilio, tempoAtivacaoEsperado);
                    return new RespostaDesativar { PodeAtivar = true, TempoAtivacaoEsperado = tempoAtivacaoEsperado, Resultado = "Desativacao iniciada" };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw new RpcException(new Status(StatusCode.Internal, "An error occurred while processing the request"));
            }
        }

        private async Task DesativarServicoAsync(User user, Coverage coverage, int tempoAtivacaoEsperado)
        {
            await Task.Delay(TimeSpan.FromSeconds(tempoAtivacaoEsperado));

            coverage.Estado = "DEACTIVATED";
            _dbContext.Coverages.Update(coverage);
            await _dbContext.SaveChangesAsync();

            var operacao = new Operaco
            {
                Operacao = "DEACTIVATION",
                Operador = user.Operator,
                NumAdministrativo = coverage.NumAdministrativo,
                Dataatual = DateTime.UtcNow
            };
            _dbContext.Operacoes.Add(operacao);
            await _dbContext.SaveChangesAsync();

            // Implementar logica de publicacao de mensagem no topico EVENTS do RabbitMQ
        }

        public override async Task<RespostaTerminar> Terminar(RequisicaoTerminar request, ServerCallContext context)
        {
            try
            {
                var user = _dbContext.Users.FirstOrDefault(u => u.Username == request.UserId);
                if (user == null || user.Password != request.Passw)
                {
                    return new RespostaTerminar { PodeAtivar = false, TempoAtivacaoEsperado = 0, Resultado = "Usuario ou senha incorretos." };
                }
                else
                {
                    var domicilio = _dbContext.Coverages.FirstOrDefault(d => d.NumAdministrativo == request.NumAdministrativo);
                    if (domicilio == null || domicilio.Estado != "DEACTIVATED" || domicilio.Operator != user.Operator)
                    {
                        return new RespostaTerminar { PodeAtivar = false, TempoAtivacaoEsperado = 0, Resultado = "Domicilio nao disponivel para termino." };
                    }

                    int tempoAtivacaoEsperado = 5; // Tempo estimado em segundos
                    await TerminarServicoAsync(user, domicilio, tempoAtivacaoEsperado);
                    return new RespostaTerminar { PodeAtivar = true, TempoAtivacaoEsperado = tempoAtivacaoEsperado, Resultado = "Termino iniciado" };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw new RpcException(new Status(StatusCode.Internal, "An error occurred while processing the request"));
            }
        }

        private async Task TerminarServicoAsync(User user, Coverage coverage, int tempoAtivacaoEsperado)
        {
            await Task.Delay(TimeSpan.FromSeconds(tempoAtivacaoEsperado));

            coverage.Estado = "FREE";
            coverage.Operator = null;
            coverage.Modalidade = null;

            _dbContext.Coverages.Update(coverage);
            await _dbContext.SaveChangesAsync();

            var operacao = new Operaco
            {
                Operacao = "TERMINATION",
                Operador = user.Operator,
                NumAdministrativo = coverage.NumAdministrativo,
                Dataatual = DateTime.UtcNow
            };
            _dbContext.Operacoes.Add(operacao);
            await _dbContext.SaveChangesAsync();

            // Implementar logica de publicacao de mensagem no topico EVENTS do RabbitMQ
        }
    }
}
