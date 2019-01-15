using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace prjProyecto.Models
{
    public class Producto
    {
        [Key]
        public int ProductoId { get; set; }
        [Display(Name = "Descripción")]
        [Required(ErrorMessage = "Se requiere ingresar la Descripción del Producto")]
        [StringLength(50, ErrorMessage = "Máximo 50 caracteres y mínimo 3", MinimumLength = 3)]
        public string ProductoDescripcion { get; set; }
        [Display(Name = "Cantidad")]
        [Required(ErrorMessage = "Se requiere ingresar la Cantidad del Producto")]
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = false)]
        [Range(1,9999999999999999999,ErrorMessage ="Solo números mayores que 0")]
        public decimal ProductoCantidad { get; set; }
        [Display(Name = "Precio")]
        [Required(ErrorMessage = "Se requiere ingresar el Precio del Producto")]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        [Range(1, 9999999999999999999, ErrorMessage = "Solo números mayores que 0")]
        public decimal ProductoPrecio { get; set; }
        public int CategoriaId { get; set; }
        public virtual Categoria Categoria { get; set; }
    }
}