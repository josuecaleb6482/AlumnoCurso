namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.Spatial;
    using System.Linq;

    [Table("Adjunto")]
    public partial class Adjunto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idAdjunto { get; set; }

        [Required]
        [DisplayAttribute]
        public int? idAlumno { get; set; }

        [Required(ErrorMessage ="Ingrese la direccion del archivo")]
        [StringLength(50)]
        public string Archivo { get; set; }
        [ForeignKey("idAlumno")]
        public virtual Alumnos Alumno { get; set; }

        public List<Adjunto> Listar(int AlumnoId)
        {
            var adjunto = new List<Adjunto>();
            try
            {
                using (var ctx = new TestContext())
                {
                    adjunto = ctx.Adjunto.Where(x => x.idAlumno == AlumnoId)
                        .ToList();
                }
            }
            catch
            {
                throw;
            }
            return adjunto;
        }
        public ResponseModel GuardarAdjunto()
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
    }
}
