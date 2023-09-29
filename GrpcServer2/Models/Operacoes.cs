using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GrpcServer2.Models
{
    public partial class Operacoes
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("operacao")]
        [StringLength(255)]
        [Unicode(false)]
        public string? Operacao { get; set; }

        [Column("operador")]
        [StringLength(255)]
        [Unicode(false)]
        public string? Operador { get; set; }

        [Column("num_administrativo")]
        public int? NumAdministrativo { get; set; }

        [Column("dataatual", TypeName = "datetime")]
        public DateTime? Dataatual { get; set; }
    }

}
