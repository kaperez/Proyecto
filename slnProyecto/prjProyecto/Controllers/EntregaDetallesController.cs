using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using prjProyecto.Models;

namespace prjProyecto.Controllers
{
    public class EntregaDetallesController : Controller
    {
        private ProyectoContext db = new ProyectoContext();

        // GET: EntregaDetalles
        [Authorize(Roles = "Administrador,Asociado")]
        public ActionResult Index()
        {
            var entregaDetalles = db.EntregaDetalles.Include(e => e.Entregas).Include(e => e.Productos);
            return View(entregaDetalles.ToList());
        }
        [Authorize(Roles = "Administrador,Asociado")]
        // GET: EntregaDetalles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EntregaDetalle entregaDetalle = db.EntregaDetalles.Find(id);
            if (entregaDetalle == null)
            {
                return HttpNotFound();
            }
            return View(entregaDetalle);
        }
        [Authorize(Roles = "Administrador,Asociado,Create")]
        // GET: EntregaDetalles/Create
        public ActionResult Create()
        {
            ViewBag.EntregaId = new SelectList(db.Entregas, "EntregaId", "EntregaId");
            ViewBag.ProductoId = new SelectList(db.Productoes, "ProductoId", "ProductoDescripcion");
            return View();
        }

        // POST: EntregaDetalles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EntregaDetalleId,EntregaId,ProductoId,Cantidad,PrecioUnitario")] EntregaDetalle entregaDetalle)
        {
            if (ModelState.IsValid)
            {
                db.EntregaDetalles.Add(entregaDetalle);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EntregaId = new SelectList(db.Entregas, "EntregaId", "EntregaId", entregaDetalle.EntregaId);
            ViewBag.ProductoId = new SelectList(db.Productoes, "ProductoId", "ProductoDescripcion", entregaDetalle.ProductoId);
            return View(entregaDetalle);
        }

        [Authorize(Roles = "Administrador,Asociado,Edit")]
        // GET: EntregaDetalles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EntregaDetalle entregaDetalle = db.EntregaDetalles.Find(id);
            if (entregaDetalle == null)
            {
                return HttpNotFound();
            }
            ViewBag.EntregaId = new SelectList(db.Entregas, "EntregaId", "EntregaId", entregaDetalle.EntregaId);
            ViewBag.ProductoId = new SelectList(db.Productoes, "ProductoId", "ProductoDescripcion", entregaDetalle.ProductoId);
            return View(entregaDetalle);
        }

        // POST: EntregaDetalles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EntregaDetalleId,EntregaId,ProductoId,Cantidad,PrecioUnitario")] EntregaDetalle entregaDetalle)
        {
            if (ModelState.IsValid)
            {
                db.Entry(entregaDetalle).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EntregaId = new SelectList(db.Entregas, "EntregaId", "EntregaId", entregaDetalle.EntregaId);
            ViewBag.ProductoId = new SelectList(db.Productoes, "ProductoId", "ProductoDescripcion", entregaDetalle.ProductoId);
            return View(entregaDetalle);
        }

        [Authorize(Roles = "Administrador,Asociado,Delete")]
        // GET: EntregaDetalles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EntregaDetalle entregaDetalle = db.EntregaDetalles.Find(id);
            if (entregaDetalle == null)
            {
                return HttpNotFound();
            }
            return View(entregaDetalle);
        }

        // POST: EntregaDetalles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EntregaDetalle entregaDetalle = db.EntregaDetalles.Find(id);
            db.EntregaDetalles.Remove(entregaDetalle);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
