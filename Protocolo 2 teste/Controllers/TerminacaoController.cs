using Microsoft.AspNetCore.Mvc;
using Protocolo_2_teste.Models;

namespace Protocolo_2_teste.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TerminacaoController : ControllerBase
    {
        // POST: api/Terminacao
        [HttpPost]
        public IActionResult TerminarServico([FromBody] Terminacao terminacao)
        {
            // Lógica para verificar se a terminação é possível e retornar resposta síncrona
            // (por exemplo, verificar o estado do serviço e calcular o tempo estimado)

            // Exemplo de resposta síncrona
            if (terminacao.NumeroAdministrativo == "123456")
            {
                terminacao.DataTerminacao = DateTime.Now;
                terminacao.TempoEstimado = "1 hora";
                return Ok(terminacao);
            }
            else
            {
                return BadRequest("A terminação não é possível.");
            }
        }
    }
}
