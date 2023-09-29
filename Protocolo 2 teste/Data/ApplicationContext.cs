using Microsoft.EntityFrameworkCore;
using Protocolo_2_teste.Models;
using System.Collections.Generic;

namespace Protocolo_2_teste.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

        public DbSet<Operador> Operadores { get; set; }
        public DbSet<Reserva> Reservas { get; set; }
        public DbSet<Ativacao> Ativacoes { get; set; }
        public DbSet<Desativacao> Desativacoes { get; set; }
        public DbSet<Terminacao> Terminacoes { get; set; }
    }
}
