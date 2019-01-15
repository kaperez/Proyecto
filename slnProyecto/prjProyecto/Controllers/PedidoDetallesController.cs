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
    public class PedidoDetallesController : Controller
    {
        private ProyectoContext db = new ProyectoContext();

        // GET: PedidoDetalles
        [Authorize(Roles = "Administrador,Asociado")]
        public ActionResult Index()
        {
            var pedidoDetalles = db.PedidoDetalles.Include(p => p.Pedidos).Include(p => p.Productos);
            return View(pedidoDetalles.ToList());
        }
        [Authorize(Roles = "Administrador,Asociado")]
        // GET: PedidoDetalles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PedidoDetalle pedidoDetalle = db.PedidoDetalles.Find(id);
            if (pedidoDetalle == null)
            {
                return HttpNotFound();
            }
            return View(pedidoDetalle);
        }
        [Authorize(Roles = "Administrador,Asociado,Create")]

        // GET: PedidoDetalles/Create
        public ActionResult Create()
        {
            ViewBag.PedidoId = new SelectList(db.Pedidoes, "PedidoId", "PedidoId");
            ViewBag.ProductoId = new SelectList(db.Productoes, "ProductoId", "ProductoDescripcion");
            return View();
        }

        // POST: PedidoDetalles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PedidoDetalleId,PedidoId,ProductoId,Cantidad,PrecioUnitario")] PedidoDetalle pedidoDetalle)
        {
            if (ModelState.IsValid)
            {
                db.PedidoDetalles.Add(pedidoDetalle);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PedidoId = new SelectList(db.Pedidoes, "PedidoId", "PedidoId", pedidoDetalle.PedidoId);
            ViewBag.ProductoId = new SelectList(db.Productoes, "ProductoId", "ProductoDescripcion", pedidoDetalle.ProductoId);
            return View(pedidoDetalle);
        }

        // GET: PedidoDetalles/Edit/5
        [Authorize(Roles = "Administrador,Asociado,Edit")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PedidoDetalle pedidoDetalle = db.PedidoDetalles.Find(id);
            if (pedidoDetalle == null)
            {
                return HttpNotFound();
            }
            ViewBag.PedidoId = new SelectList(db.Pedidoes, "PedidoId", "PedidoId", pedidoDetalle.PedidoId);
            ViewBag.ProductoId = new SelectList(db.Productoes, "ProductoId", "ProductoDescripcion", pedidoDetalle.ProductoId);
            return View(pedidoDetalle);
        }

        // POST: PedidoDetalles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PedidoDetalleId,PedidoId,ProductoId,Cantidad,PrecioUnitario")] PedidoDetalle pedidoDetalle)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pedidoDetalle).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PedidoId = new SelectList(db.Pedidoes, "PedidoId", "PedidoId", pedidoDetalle.PedidoId);
            ViewBag.ProductoId = new SelectList(db.Productoes, "ProductoId", "ProductoDescripcion", pedidoDetalle.ProductoId);
            return View(pedidoDetalle);
        }

        // GET: PedidoDetalles/Delete/5
        [Authorize(Roles = "Administrador,Asociado,Delete")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PedidoDetalle pedidoDetalle = db.PedidoDetalles.Find(id);
            if (pedidoDetalle == null)
            {
                return HttpNotFound();
            }
            return View(pedidoDetalle);
        }

        // POST: PedidoDetalles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PedidoDetalle pedidoDetalle = db.PedidoDetalles.Find(id);
            db.PedidoDetalles.Remove(pedidoDetalle);
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
