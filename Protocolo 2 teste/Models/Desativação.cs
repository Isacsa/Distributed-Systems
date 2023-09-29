using System;
using System.ComponentModel.DataAnnotations;

namespace Protocolo_2_teste.Models
{
    public class Desativacao
    {
        [Key]
        public int IdDesativacao { get; set; }
        public string NumeroAdministrativo { get; set; }  // Número administrativo único para identificar a desativação
        public string IdUtilizador { get; set; }  // Utilizador que solicitou a desativação
        public DateTime DataDesativacao { get; set; }  // A data e hora em que a desativação foi realizada
        public string TempoEstimado { get; set; }  // Tempo estimado para conclusão da desativação
                                                   // Outras propriedades conforme necessário
    }
}
