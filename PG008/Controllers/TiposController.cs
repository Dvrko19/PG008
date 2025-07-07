using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PG008.Metodos;
using PG008.Models;

namespace PG008.Controllers
{
    public class TiposController : Controller
    {
        // GET: Tipos
        public ActionResult Tipos()
        {
            if (Session["Usuario"] == null)
                return RedirectToAction("Login", "Acceder");

            return View();
        }
        [HttpGet]
        public JsonResult consultaTipos()
        {
            List<Tipos> oLista = new List<Tipos>();

            oLista = TiposMetodos.Instancia.Listar();

            return Json(oLista);
        }

        [HttpPost]
        public JsonResult InsertarTipos(Tipos oCat)
        {
            bool repuesta = false;

            repuesta = (oCat.IdTipos == 0) ? TiposMetodos.Instancia.Registrar(oCat) : TiposMetodos.Instancia.Modificar(oCat);

            return Json(new { resultado = repuesta }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult BorrarTipos(int id)
        {
            bool Respuesta = false;

            Respuesta = TiposMetodos.Instancia.Eliminar(id);

            return Json(new { resultado = Respuesta }, JsonRequestBehavior.AllowGet);
        }


    }
}