using Microsoft.AspNetCore.Mvc;
using Protocolo_2_teste.Models;

namespace Protocolo_2_teste.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AtivacaoController : ControllerBase
    {
        // POST: api/Ativacao
        [HttpPost]
        public IActionResult AtivarServico([FromBody] Ativacao ativacao)
        {
            // Lógica para verificar se a ativação é possível e retornar resposta síncrona
            // (por exemplo, verificar o estado do serviço e calcular o tempo estimado)

            // Exemplo de resposta síncrona
            if (ativacao.NumeroAdministrativo == "123456")
            {
                ativacao.DataAtivacao = DateTime.Now;
                ativacao.TempoEstimado = "2 horas";
                return Ok(ativacao);
            }
            else
            {
                return BadRequest("A ativação não é possível.");
            }
        }
    }
}
