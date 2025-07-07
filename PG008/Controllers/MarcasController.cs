using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PG008.Metodos;
using PG008.Models;

namespace PG008.Controllers
{
    public class MarcasController : Controller
    {
        // GET: Marcas
        public ActionResult Marcas()
        {
            if (Session["Usuario"] == null)
                return RedirectToAction("Login", "Acceder");

            return View();
           
        }

        [HttpGet]
        public JsonResult consultaMarcas()
        {
            List<Marcas> oMarca = new List<Marcas>();

            oMarca = MarcasMetodos.Instancia.Listar();

            return Json(oMarca);
        }

        [HttpPost]
        public JsonResult InsertarMarcas(Marcas oCat)
        {
            bool repuesta = false;
            
            repuesta = (oCat.IDMarca == 0) ? MarcasMetodos.Instancia.Registrar(oCat) : MarcasMetodos.Instancia.Modificar(oCat);

            return Json(new {resultado = repuesta}, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult BorrarMarcas(int id) 
        {
            bool Respuesta = false;

            Respuesta = MarcasMetodos.Instancia.Eliminar(id);

            return Json(new { resultado = Respuesta}, JsonRequestBehavior.AllowGet);
        }
       

    }
}