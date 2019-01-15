using prjProyecto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace prjProyecto.ViewModels
{
    public class EntregaView
    {
        public Proveedor Proveedor { get; set; }

        public ProductoEntregado ProductoEntregado { get; set; }

        public List<ProductoEntregado> ProductosEntregados { get; set; }
    }
}