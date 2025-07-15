using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PG008.Metodos;
using PG008.Models;

namespace PG008.Controllers
{
    public class CategoriaController : Controller
    {
        // GET: Categoria
        public ActionResult Categoria()
        {
            return View();
        }
        [HttpGet]
        public JsonResult consultaCategoria()
        {
            List<Categoria> oLista = new List<Categoria>();

            oLista = CategoriaMetodos.Instancia.Listar();

            return Json(oLista);
        }
        [HttpPost]
        public JsonResult InsertarCategoria(Categoria oCat)
        {
            bool repuesta = false;

            repuesta = (oCat.idCategoria == 0) ? CategoriaMetodos.Instancia.Registrar(oCat) : CategoriaMetodos.Instancia.Modificar(oCat);

            return Json(new { resultado = repuesta }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult ModificarCategoria(Categoria oCat)
        {
            bool respuesta = false;

            if (oCat.idCategoria > 0)
            {
                respuesta = CategoriaMetodos.Instancia.Modificar(oCat);
            }

            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult BorrarCategoria(int id)
        {
            bool Respuesta = false;

            Respuesta = CategoriaMetodos.Instancia.Eliminar(id);

            return Json(new { resultado = Respuesta }, JsonRequestBehavior.AllowGet);
        }
        
    }
}