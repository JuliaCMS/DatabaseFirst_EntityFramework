using System;
using System.Collections.Generic;
using DBFirstEntityFramework.DataModels;
using Microsoft.EntityFrameworkCore;

namespace DBFirstEntityFramework;

public partial class DbfirstEntityFrameworkContext : DbContext
{
    public DbfirstEntityFrameworkContext()
    {
    }

    public DbfirstEntityFrameworkContext(DbContextOptions<DbfirstEntityFrameworkContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Aluno> Alunos { get; set; }

    public virtual DbSet<Curso> Cursos { get; set; }

    public virtual DbSet<Matricula> Matriculas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=localhost\\SQLEXPRESS2022;Initial Catalog=DBFirstEntityFramework;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Aluno>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__aluno__3213E83F2ECCF6C0");

            entity.ToTable("aluno");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Nome)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nome");
        });

        modelBuilder.Entity<Curso>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__curso__3213E83FF01660B6");

            entity.ToTable("curso");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Nome)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nome");
        });

        modelBuilder.Entity<Matricula>(entity =>
        {
            entity.HasKey(e => e.Registro).HasName("PK__matricul__58C1F746BCBC5B9F");

            entity.ToTable("matricula");

            entity.Property(e => e.Registro).HasColumnName("registro");
            entity.Property(e => e.FkAlunoId).HasColumnName("fk_alunoId");
            entity.Property(e => e.FkCursoId).HasColumnName("fk_cursoId");

            entity.HasOne(d => d.FkAluno).WithMany(p => p.Matriculas)
                .HasForeignKey(d => d.FkAlunoId)
                .HasConstraintName("FK__matricula__fk_al__4D94879B");

            entity.HasOne(d => d.FkCurso).WithMany(p => p.Matriculas)
                .HasForeignKey(d => d.FkCursoId)
                .HasConstraintName("FK__matricula__fk_cu__4E88ABD4");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
