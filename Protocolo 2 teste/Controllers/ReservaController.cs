using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Protocolo_2_teste.Models;
using Protocolo_2_teste.Data;

namespace Protocolo_2_teste.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReservaController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ReservaController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<Reserva>> PostReserva(Reserva reserva)
        {
            // Validar a modalidade e o domicilio da reserva aqui

            // Gerar um número administrativo único para a reserva
            reserva.NumeroAdministrativo = Guid.NewGuid().ToString();

            // Definir o estado inicial da reserva para "RESERVED"
            reserva.Estado = "RESERVED";

            // Registrar a data e hora da reserva
            reserva.DataReserva = DateTime.Now;

            _context.Reservas.Add(reserva);
            await _context.SaveChangesAsync();

            // Devolver a reserva recém-criada com o número administrativo único para o cliente
            return CreatedAtAction(nameof(GetReserva), new { id = reserva.ReservaId }, reserva);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Reserva>> GetReserva(int id)
        {
            var reserva = await _context.Reservas.FindAsync(id);

            if (reserva == null)
            {
                return NotFound();
            }

            return reserva;
        }

        // Outros métodos para gerenciar reservas (Ativação, Desativação, Terminação) aqui
    }
}
