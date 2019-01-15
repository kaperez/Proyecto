using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace prjProyecto.Models
{
    public class Categoria
    {
        [Key]
        public int CategoriaId { get; set; }
        [Display(Name = "Nombre Categoria")]
        [Required(ErrorMessage = "Se requiere ingresar el nombre de la Categoría")]
        [StringLength(50, ErrorMessage = "Máximo 50 caracteres y mínimo 3", MinimumLength = 3)]
        public string CategoriaNombre { get; set; }
    }
}