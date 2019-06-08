namespace Model
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class TestContext : DbContext
    {
        public TestContext()
            : base("name=TestContext")
        {
        }

        public virtual DbSet<Adjunto> Adjunto { get; set; }
        public virtual DbSet<AlumnoCurso> AlumnoCurso { get; set; }
        public virtual DbSet<Alumnos> Alumnos { get; set; }
        public virtual DbSet<Curso> Curso { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Adjunto>()
                .Property(e => e.Archivo)
                .IsUnicode(false);

            modelBuilder.Entity<Alumnos>()
                .Property(e => e.Nombre)
                .IsUnicode(false);

            modelBuilder.Entity<Alumnos>()
                .Property(e => e.Apellido)
                .IsUnicode(false);

            modelBuilder.Entity<Alumnos>()
                .Property(e => e.Sexo)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Curso>()
                .Property(e => e.Nombre)
                .IsUnicode(false);
        }
    }
}
