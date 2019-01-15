using prjProyecto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace prjProyecto.Controllers
{
    public class FechasController : Controller
    {
        // GET: Fechas
        private ProyectoContext db = new ProyectoContext();

        public ActionResult Buscar(DateTime ?searchDate, DateTime? searchDate2)
        {
            ViewBag.SearchDate = searchDate;
            ViewBag.SearchDate2 = searchDate2;
            var e = db.Entregas.ToList();
            if (searchDate == null || searchDate2==null)
            {
                var entregas = db.Entregas;
                return View(entregas.ToList());
            }
            else
            {
              
                var entregas = db.Entregas.Where(p => p.EntregaFecha >= searchDate && p.EntregaFecha <= searchDate2);

                return View(entregas.ToList());
            }
        }
    }
}