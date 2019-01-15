using prjProyecto.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace prjProyecto.ViewModels
{
    public class ProductoPedido:Producto
    {
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = false)]
        [Range(1, 999999, ErrorMessage = "Solo números mayores que 0")]
        public int Cantidad { get; set; }

        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        [Range(1, 999999, ErrorMessage = "Solo números mayores que 0")]
        public decimal Subtotal { get { return ProductoPrecio * Cantidad; } }
    }
}