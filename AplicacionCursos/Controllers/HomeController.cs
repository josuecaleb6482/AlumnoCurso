using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model;

namespace AplicacionCursos.Controllers
{
    public class HomeController : Controller
    {
        private Alumnos alumno = new Alumnos();
        private AlumnoCurso alumnoCurso = new AlumnoCurso();
        private Curso curso = new Curso();
        private Adjunto adjunto = new Adjunto();
        // GET: Home
        public ActionResult Index()
        {
            return View(alumno.Listar());  
        }

        //home/ver/#1
        public ActionResult Ver(int id)
        {
            return View(alumno.Obtener(id));
        }

        //home/crud/?Alumno_id=1
        public PartialViewResult Curso(int id)
        {
            //Listar cursos asignados
            ViewBag.CursosAsignados = alumnoCurso.Listar(id);
            //Listar cursos disponibles
            ViewBag.Cursos = curso.Todos(id);
            //Modelo
            alumnoCurso.idAlumno = id;
            return PartialView(alumnoCurso);
        }
        public PartialViewResult Adjuntos(int id)
        {
            ViewBag.Adjuntos = adjunto.Listar(id);

            adjunto.idAlumno = id;
            return PartialView();
        }

        public ActionResult Crud(int id = 0)
        {
            return View(
                id == 0 ? new Alumnos() 
                        : alumno.Obtener(id)
                        );
        }
        public JsonResult Guardar(Alumnos model)
        {
            var rm = new ResponseModel();

            if (ModelState.IsValid)
            {
                rm = model.Guardar();
                if (rm.response)
                {
                    rm.href = Url.Content("~/home");
                }
            }
            return Json(rm);
        }

        public JsonResult GuardarCursos(AlumnoCurso model)
        {
            var rm = new ResponseModel();

            if (ModelState.IsValid)
            {
                rm = model.GuardarCurso();
                if (rm.response)
                {
                    rm.function = "CargarCursos()";
                }
            }
            else
            {
                rm.SetResponse(false,"Debe llenar el campo nota");
            }
            
            return Json(rm);
        }
        public JsonResult GuardarAdjunto(Adjunto model, HttpPostedFileBase Archivo)
        {
            var rm = new ResponseModel();

            if (Archivo != null)
            {
                string archivo = DateTime.Now.ToString("yyyyMMddss") + Path.GetExtension(Archivo.FileName);
                Archivo.SaveAs(Server.MapPath("~/uploads/"+archivo));

                model.Archivo = archivo;

                rm = model.GuardarAdjunto();

                if (rm.response)
                {
                        rm.function = "CargarAdjuntos()";
                }
            }
            
            return Json(rm);
        }
        public ActionResult Eliminar(int id)
        {
            alumno.idAlumno = id;
            alumno.Eliminar();
            return Redirect("/home");
        }
        public ActionResult EliminarCurso(int id, int idAlumnoes)
        {
            alumno.idAlumno = idAlumnoes;
            alumnoCurso.idAlumnoCurso = id;
            alumnoCurso.Eliminar();
            return Redirect("/Home/Crud/"+alumno.idAlumno);
        }
    }
}