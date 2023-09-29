using System;
using System.ComponentModel.DataAnnotations;

namespace Protocolo_2_teste.Models
{
    public class Terminacao
    {
        [Key]
        public int IdTerminacao { get; set; }
        public string NumeroAdministrativo { get; set; }  // Número administrativo único para identificar a terminação
        public string IdUtilizador { get; set; }  // Utilizador que solicitou a terminação
        public DateTime DataTerminacao { get; set; }  // A data e hora em que a terminação foi realizada
        public string TempoEstimado { get; set; }  // Tempo estimado para conclusão da terminação
                                                   // Outras propriedades conforme necessário
    }
}
