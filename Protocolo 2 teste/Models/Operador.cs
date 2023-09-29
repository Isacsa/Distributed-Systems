using System.ComponentModel.DataAnnotations;

namespace Protocolo_2_teste.Models
{
    public class Operador
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public string Senha { get; set; }

        // Adicione outros campos conforme necessário
    }
}
