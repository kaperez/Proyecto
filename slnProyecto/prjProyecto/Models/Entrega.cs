using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace prjProyecto.Models
{
    public class Entrega
    {
        [Key]
        public int EntregaId { get; set; }
        [Display(Name = "Fecha")]
        [Required(ErrorMessage = "Se requiere ingresar la Fecha")]
        [DataType(DataType.Date)]
        [CustomDateRange]
        public DateTime EntregaFecha { get; set; }
        public int ProveedorId { get; set; }
        public virtual Proveedor Proveedor { get; set; }
        public virtual List<EntregaDetalle> EntregaDetalles { get; set; }
    }
}