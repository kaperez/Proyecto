using prjProyecto.Models;
using prjProyecto.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace prjProyecto.Controllers
{
    public class VentaController : Controller
    {
        ProyectoContext db = new ProyectoContext();
        // GET: Venta
        [Authorize(Roles = "Administrador,Asociado")]
        public ActionResult NuevoPedido()
        {
            PedidoView pedidoView = new PedidoView();

            pedidoView.Cliente = new Cliente();
            pedidoView.ProductosPedidos = new List<ProductoPedido>();

            Session["PedidoView"] = pedidoView;

            var listaClientes = db.Clientes.ToList();
            ViewBag.ClienteId = new SelectList(listaClientes, "ClienteId", "ClienteRazonSocial");
            return View(pedidoView);
        }

        // POST: Venta
        [HttpPost]
        public ActionResult NuevoPedido(PedidoView pedidoView)
        {
            pedidoView = Session["PedidoView"] as PedidoView;
            int idCliente = int.Parse(Request["ClienteId"]);

            Pedido pedido = new Pedido()
            {
                ClienteId = idCliente,
                PedidoFecha = DateTime.Now
            };
            db.Pedidoes.Add(pedido);

            db.SaveChanges();

            int ultimoPedidoId = db.Pedidoes.ToList()
                .Select(p => p.PedidoId).Max();

            foreach (ProductoPedido item in pedidoView.ProductosPedidos)
            {
                var detalle = new PedidoDetalle()
                {
                    PedidoId = ultimoPedidoId,
                    ProductoId = item.ProductoId,
                    Cantidad = item.Cantidad
                };
                db.PedidoDetalles.Add(detalle);
            }

            db.SaveChanges();

            pedidoView = Session["PedidoView"] as PedidoView;

            var listaClientes = db.Clientes.ToList();
            ViewBag.ClienteId = new SelectList(listaClientes, "ClienteId", "ClienteRazonSocial");

            return View(pedidoView);
        }

        [Authorize(Roles = "Administrador,Asociado")]
        public ActionResult agregarProducto()
        {
            var listaProductos = db.Productoes.ToList();
            ViewBag.ProductoId = new SelectList(listaProductos, "ProductoId", "ProductoDescripcion");
            return View();


        }

        [HttpPost]
        public ActionResult agregarProducto(ProductoPedido productopedido)
        {
            var pedidoview = Session["PedidoView"] as PedidoView;
            var productoid = int.Parse(Request["ProductoId"]);
            var producto = db.Productoes.Find(productoid);

            productopedido = new ProductoPedido()
            {
                ProductoId = producto.ProductoId,
                ProductoDescripcion = producto.ProductoDescripcion,
                ProductoPrecio = producto.ProductoPrecio,
                Cantidad = int.Parse(Request["Cantidad"]),

            };

            pedidoview.ProductosPedidos.Add(productopedido);


            var listaClientes = db.Clientes.ToList();
            ViewBag.ClienteId = new SelectList(listaClientes, "ClienteId", "ClienteRazonSocial");


            var listaProductos = db.Productoes.ToList();
            ViewBag.ProductoId = new SelectList(listaProductos, "ProductoId", "ProductoDescripcion");

            return View("NuevoPedido", pedidoview);


        }
    }
}