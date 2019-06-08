namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Linq;

    [Table("Alumnos")]
    public partial class Alumnos
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Alumnos()
        {
            Adjuntoes = new HashSet<Adjunto>();
            AlumnoCurso = new HashSet<AlumnoCurso>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idAlumno { get; set; }

        [Required]
        [StringLength(50)]
        public string Nombre { get; set; }

        [Required]
        [StringLength(50)]
        public string Apellido { get; set; }

        [Required]
        [StringLength(1)]
        public string Sexo { get; set; }

        [Column(TypeName = "date")]
        public DateTime? FechaNacimiento { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Adjunto> Adjuntoes { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AlumnoCurso> AlumnoCurso { get; set; }

        public List<Alumnos> Listar()
        {
            var alumnos = new List<Alumnos>();

            try
            {
                using (var ctx = new TestContext())
                {
                    alumnos = ctx.Alumnos.ToList();
                }
            }
            catch (Exception e)
            {
                e.ToString();
                throw;
            }

            return alumnos;
        }

        public Alumnos Obtener(int id)
        {
            var alumno = new Alumnos();

            try
            {
                using (var ctx = new TestContext())
                {
                    alumno = ctx.Alumnos.Include("AlumnoCurso")
                        .Include("AlumnoCurso.Curso")
                        .Where(x => x.idAlumno == id)
                        .SingleOrDefault();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                throw;
            }

            return alumno;
        }
        public ResponseModel Guardar()
        {
            var rm = new ResponseModel();
            
            try
            {
                using (var ctx = new TestContext())
                {
                    if (this.idAlumno > 0)
                    {
                        ctx.Entry(this).State = EntityState.Modified;
                    }
                    else
                    {
                        ctx.Entry(this).State = EntityState.Added;
                    }

                    rm.SetResponse(true);
                    ctx.SaveChanges();
                }
            }
            catch (Exception e)
            {
                e.ToString();
                throw;
            }

            return rm;
        }

        public Alumnos Eliminar()
        {
            var alumno = new Alumnos();

            try
            {
                using (var ctx = new TestContext())
                {
                    ctx.Entry(this).State = EntityState.Deleted;
                    ctx.SaveChanges();
                }
            }
            catch (Exception e)
            {
                e.ToString();
                throw;
            }

            return alumno;
        }
    }
}
