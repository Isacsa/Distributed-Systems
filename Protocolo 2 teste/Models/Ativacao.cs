using System;
using System.ComponentModel.DataAnnotations;

namespace Protocolo_2_teste.Models
{
    public class Ativacao
    {
        [Key]
        public int IdAtivacao { get; set; }
        public string NumeroAdministrativo { get; set; }  // Número administrativo único para identificar a ativação
        public string IdUtilizador { get; set; }  // Utilizador que solicitou a ativação
        public DateTime DataAtivacao { get; set; }  // A data e hora em que a ativação foi realizada
        public string TempoEstimado { get; set; }  // Tempo estimado para conclusão da ativação
                                                   // Outras propriedades conforme necessário
    }
}
