using System;
using System.Collections.Generic;
using GrpcServer2.Models;
using Microsoft.EntityFrameworkCore;

namespace GrpcServer2.Data;

public partial class GrpcProtocolo2Context : DbContext
{
    public GrpcProtocolo2Context()
    {
    }

    public GrpcProtocolo2Context(DbContextOptions<GrpcProtocolo2Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Cobertura> Coberturas { get; set; }

    public virtual DbSet<Operaco> Operacoes { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("name=GrpcProtocolo2Context");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cobertura>(entity =>
        {
            entity.HasKey(e => e.NumAdministrativo).HasName("PK__Cobertur__F37EB497EB7D56CC");

            entity.Property(e => e.NumAdministrativo).ValueGeneratedNever();
        });

        modelBuilder.Entity<Operaco>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Operacoe__3213E83FC1820745");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3213E83F44EB8E06");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
