using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace prjProyecto.Models
{
    public class Proveedor
    {
        [Key]
        public int ProveedorId { get; set; }
        [Display(Name = "RUC")]
        [Required(ErrorMessage ="Se requiere ingresar RUC")]
        [StringLength(13,ErrorMessage ="Los RUC son solo númericos y de 13 caracteres",MinimumLength =13)]
        [Range(1, 9999999999999)]
        public string ProveedorRUC { get; set; }
        [Display(Name = "Razón Social")]
        [Required(ErrorMessage = "Se requiere ingresar la Razón Social")]
        [StringLength(50, ErrorMessage = "Máximo 50 caracteres y mínimo 3", MinimumLength = 3)]
        public string ProveedorRazonSocial { get; set; }
        [Display(Name = "Nombre Contacto")]
        [Required(ErrorMessage = "Se requiere ingresar el Nombre de Contacto")]
        [StringLength(50, ErrorMessage = "Máximo 50 caracteres y mínimo 3", MinimumLength = 3)]
        public string ProveedorNombreContacto { get; set; }
        [Display(Name = "Dirección")]
        [Required(ErrorMessage = "Se requiere ingresar la Dirección")]
        [StringLength(50, ErrorMessage = "Máximo 50 caracteres y mínimo 3", MinimumLength = 3)]
        public string ProveedorDireccion { get; set; }
        [Display(Name = "Teléfono")]
        [Required(ErrorMessage = "Se requiere ingresar el Teléfono")]
        [StringLength(10, ErrorMessage = "Máximo 10 caracteres", MinimumLength = 10)]
        public string ProveedorTelefono { get; set; }
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Se requiere ingresar el Email")]
        [EmailAddress(ErrorMessage ="Ingrese una dirección de correo electrónico válida")]
        public string ProveedorEmail { get; set; }
        public virtual List<Pedido> Pedidos { get; set; }
    }
}