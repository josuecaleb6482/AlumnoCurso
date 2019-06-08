namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.Spatial;
    using System.Linq;

    [Table("AlumnoCurso")]
    public partial class AlumnoCurso
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idAlumnoCurso { get; set; }

        public int? idAlumno { get; set; }
        [Required]
        public int? idCurso { get; set; }

        [Required(ErrorMessage ="Este campo no puede ir vacio")]
        [Range(1,100, ErrorMessage ="Solo puede ingresar valores del 1 al 100")]
        public int Nota { get; set; }

        [ForeignKey("idAlumno")]
        public virtual Alumnos Alumnos { get; set; }

        [ForeignKey("idCurso")]
        public virtual Curso Curso { get; set; }

        public ResponseModel GuardarCurso()
        {
            var rm = new ResponseModel();

            try
            {
                using (var ctx = new TestContext())
                {
                    ctx.Entry(this).State = EntityState.Added;
                   
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

        public List<AlumnoCurso> Listar(int id)
        {
            var alumnosCursos = new List<AlumnoCurso>();

            try
            {
                using (var ctx = new TestContext())
                {
                    alumnosCursos = ctx.AlumnoCurso.Include("Curso")
                        .Where(x => x.idAlumno == id)
                        .ToList();
                }
            }
            catch (Exception e)
            {
                e.ToString();
                throw;
            }

            return alumnosCursos;
        }
        public AlumnoCurso Eliminar()
        {
            var alumnoCurso = new AlumnoCurso();

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

            return alumnoCurso;
        }
    }
}