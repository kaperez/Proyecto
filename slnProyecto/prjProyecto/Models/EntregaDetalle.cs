using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace prjProyecto.Models
{
    public class EntregaDetalle
    {
        [Key]
        public int EntregaDetalleId { get; set; }
        public int EntregaId { get; set; }
        public int ProductoId { get; set; }
        public decimal Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public virtual Entrega Entregas { get; set; }
        public virtual Producto Productos { get; set; }
    }
}