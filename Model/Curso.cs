namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;

    [Table("Cursos")]
    public partial class Curso
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Curso()
        {
            AlumnoCursoes = new HashSet<AlumnoCurso>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int idCurso { get; set; }

        [Required]
        [StringLength(50)]
        public string Nombre { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AlumnoCurso> AlumnoCursoes { get; set; }

        public List<Curso> Todos(int idAlumno = 0)
        {
            var cursos = new List<Curso>();

            try
            {
                using (var ctx = new TestContext())
                {
                    if(idAlumno > 0)
                    {
                        var cursosAsignados = ctx.AlumnoCurso.Where(x => x.idAlumno == idAlumno)
                        .Select(x => x.idCurso)
                        .ToList();
                        cursos = ctx.Curso.Where(x => !cursosAsignados.Contains(x.idCurso)).ToList();
                    }
                    else
                    {
                        cursos = ctx.Curso.ToList();
                    }
                    
                }
            }
            catch(Exception e)
            {
                throw;
            }

            return cursos;
        }
    }
}
