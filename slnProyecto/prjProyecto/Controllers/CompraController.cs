using prjProyecto.Models;
using prjProyecto.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace prjProyecto.Controllers
{
    public class CompraController : Controller
    {
        // GET: Compra
        ProyectoContext db = new ProyectoContext();
        
        [Authorize(Roles = "Administrador,Asociado")]
        public ActionResult NuevaEntrega()
        {
            EntregaView entregaView = new EntregaView();

            entregaView.Proveedor = new Proveedor();
            entregaView.ProductosEntregados = new List<ProductoEntregado>();

            Session["EntregaView"] = entregaView;

            var listaProveedores = db.Proveedors.ToList();
            ViewBag.ProveedorId = new SelectList(listaProveedores, "ProveedorId", "ProveedorRazonSocial");
            return View(entregaView);
        }

        // POST: Compra
        [HttpPost]
        public ActionResult NuevaEntrega(EntregaView entregaView)
        {
            entregaView = Session["EntregaView"] as EntregaView;
            int idProveedor = int.Parse(Request["ProveedorId"]);

            Entrega entrega = new Entrega()
            {
                ProveedorId=idProveedor,
                EntregaFecha = DateTime.Now
            };
            db.Entregas.Add(entrega);

            db.SaveChanges();

            int ultimaEntregaId = db.Entregas.ToList()
                .Select(p => p.EntregaId).Max();

            foreach (ProductoEntregado item in entregaView.ProductosEntregados)
            {
                var detalle = new EntregaDetalle()
                {
                    EntregaId=ultimaEntregaId,
                    ProductoId = item.ProductoId,
                    Cantidad = item.Cantidad
                };
                db.EntregaDetalles.Add(detalle);
            }

            db.SaveChanges();

            entregaView = Session["EntregaView"] as EntregaView;

            var listaProveedores = db.Proveedors.ToList();
            ViewBag.ProveedorId = new SelectList(listaProveedores, "ProveedorId", "ProveedorRazonSocial");

            return View(entregaView);
        }


        [Authorize(Roles = "Administrador,Asociado")]
        public ActionResult agregarProducto()
        {
            var listaProductos = db.Productoes.ToList();
            ViewBag.ProductoId = new SelectList(listaProductos, "ProductoId", "ProductoDescripcion");
            return View();


        }

        [HttpPost]
        public ActionResult agregarProducto(ProductoEntregado productoentregado)
        {
            var entregaview = Session["EntregaView"] as EntregaView;
            var productoid = int.Parse(Request["ProductoId"]);
            var producto = db.Productoes.Find(productoid);

            productoentregado = new ProductoEntregado()
            {
                ProductoId = producto.ProductoId,
                ProductoDescripcion = producto.ProductoDescripcion,
                ProductoPrecio = producto.ProductoPrecio,
                Cantidad = int.Parse(Request["Cantidad"]),

            };

            entregaview.ProductosEntregados.Add(productoentregado);


            var listaProveedores = db.Proveedors.ToList();
            ViewBag.ProveedorId = new SelectList(listaProveedores, "ProveedorId", "ProveedorRazonSocial");


            var listaProductos = db.Productoes.ToList();
            ViewBag.ProductoId = new SelectList(listaProductos, "ProductoId", "ProductoDescripcion");

            return View("NuevaEntrega", entregaview);


        }
    }
}