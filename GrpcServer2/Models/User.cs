using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GrpcServer2.Models;

public partial class User
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("username")]
    [StringLength(255)]
    [Unicode(false)]
    public string? Username { get; set; }

    [Column("password")]
    [StringLength(255)]
    [Unicode(false)]
    public string? Password { get; set; }

    [Column("operator")]
    [StringLength(255)]
    [Unicode(false)]
    public string? Operator { get; set; }
}
