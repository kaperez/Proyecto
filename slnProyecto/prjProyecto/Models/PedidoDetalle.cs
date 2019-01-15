using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace prjProyecto.Models
{
    public class PedidoDetalle
    {
        [Key]
        public int PedidoDetalleId { get; set; }
        public int PedidoId { get; set; }
        public int ProductoId { get; set; }
       
        public decimal Cantidad { get; set; }
        
        public decimal PrecioUnitario { get; set; }
        public virtual Pedido Pedidos { get; set; }
        public virtual Producto Productos { get; set; }
    }
}