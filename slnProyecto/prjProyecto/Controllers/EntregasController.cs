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
    public class EntregasController : Controller
    {
        private ProyectoContext db = new ProyectoContext();

        // GET: Entregas
        [Authorize(Roles = "Administrador,Asociado")]
        public ActionResult Index()
        {
            var entregas = db.Entregas.Include(e => e.Proveedor);
            return View(entregas.ToList());
        }

        [Authorize(Roles = "Administrador,Asociado")]
        // GET: Entregas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Entrega entrega = db.Entregas.Find(id);
            if (entrega == null)
            {
                return HttpNotFound();
            }
            return View(entrega);
        }

        [Authorize(Roles = "Administrador,Asociado,Create")]
        // GET: Entregas/Create
        public ActionResult Create()
        {
            ViewBag.ProveedorId = new SelectList(db.Proveedors, "ProveedorId", "ProveedorRUC");
            return View();
        }

        // POST: Entregas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EntregaId,EntregaFecha,ProveedorId")] Entrega entrega)
        {
            if (ModelState.IsValid)
            {
                db.Entregas.Add(entrega);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProveedorId = new SelectList(db.Proveedors, "ProveedorId", "ProveedorRUC", entrega.ProveedorId);
            return View(entrega);
        }

        // GET: Entregas/Edit/5
        [Authorize(Roles = "Administrador,Asociado,Edit")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Entrega entrega = db.Entregas.Find(id);
            if (entrega == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProveedorId = new SelectList(db.Proveedors, "ProveedorId", "ProveedorRUC", entrega.ProveedorId);
            return View(entrega);
        }

        // POST: Entregas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EntregaId,EntregaFecha,ProveedorId")] Entrega entrega)
        {
            if (ModelState.IsValid)
            {
                db.Entry(entrega).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProveedorId = new SelectList(db.Proveedors, "ProveedorId", "ProveedorRUC", entrega.ProveedorId);
            return View(entrega);
        }

        // GET: Entregas/Delete/5
        [Authorize(Roles = "Administrador,Asociado,Delete")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Entrega entrega = db.Entregas.Find(id);
            if (entrega == null)
            {
                return HttpNotFound();
            }
            return View(entrega);
        }

        // POST: Entregas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Entrega entrega = db.Entregas.Find(id);
            db.Entregas.Remove(entrega);
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
