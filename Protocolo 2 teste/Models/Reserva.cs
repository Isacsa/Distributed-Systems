using System.ComponentModel.DataAnnotations;

namespace Protocolo_2_teste.Models
{
    public class Reserva
    {
        [Key]
        public int ReservaId { get; set; }
        public string UsuarioId { get; set; } // Usuário que fez a reserva

        [Required]
        public string Modalidade { get; set; } // Modalidade para a reserva

        [Required]
        public string Domicilio { get; set; } // Domicílio para a reserva

        public DateTime DataReserva { get; set; }  // A data e hora em que a reserva foi feita

        // Estado da reserva (RESERVED, ACTIVE, DEACTIVATED, TERMINATED)
        public string Estado { get; set; }

        // Número administrativo único associado à reserva
        public string NumeroAdministrativo { get; set; }
    }
}
