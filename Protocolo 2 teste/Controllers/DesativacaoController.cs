using Microsoft.AspNetCore.Mvc;
using Protocolo_2_teste.Models;

namespace Protocolo_2_teste.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DesativacaoController : ControllerBase
    {
        // POST: api/Desativacao
        [HttpPost]
        public IActionResult DesativarServico([FromBody] Desativacao desativacao)
        {
            // Lógica para verificar se a desativação é possível e retornar resposta síncrona
            // (por exemplo, verificar o estado do serviço e calcular o tempo estimado)

            // Exemplo de resposta síncrona
            if (desativacao.NumeroAdministrativo == "123456")
            {
                desativacao.DataDesativacao = DateTime.Now;
                desativacao.TempoEstimado = "1 hora";
                return Ok(desativacao);
            }
            else
            {
                return BadRequest("A desativação não é possível.");
            }
        }
    }
}
