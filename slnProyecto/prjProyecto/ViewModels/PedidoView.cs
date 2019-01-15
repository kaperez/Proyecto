using prjProyecto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace prjProyecto.ViewModels
{
    public class PedidoView
    {
        public Cliente Cliente { get; set; }

        public ProductoPedido ProductoPedidos { get; set; }

        public List<ProductoPedido> ProductosPedidos { get; set; }
    }
}