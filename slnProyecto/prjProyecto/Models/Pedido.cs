using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace prjProyecto.Models
{
    public class Pedido
    {//Pedido
        [Key]
        public int PedidoId { get; set; }
        [Display(Name = "Fecha")]
        [Required(ErrorMessage = "Se requiere ingresar la Fecha del Pedido")]
        [DataType(DataType.Date)]
        [CustomDateRange]
        public DateTime PedidoFecha { get; set; }
        public int ClienteId { get; set; }
        public virtual Cliente  Cliente { get; set; }
        public virtual List<PedidoDetalle> PedidoDetalles { get; set; }
    }
}