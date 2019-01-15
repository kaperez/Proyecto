using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace prjProyecto.Models
{
    public class Cliente
    {
        [Key]
        public int ClienteId { get; set; }
        [Display(Name = "RUC")]
        [Required(ErrorMessage = "Se requiere ingresar RUC")]
        [StringLength(13, ErrorMessage = "Los RUC son solo númericos y de 13 caracteres", MinimumLength = 13)]
        [Range(1, 9999999999999999999, ErrorMessage = "Solo números mayores que 0")]
        public string ClienteRUC { get; set; }
        [Display(Name = "Razón Social")]
        [Required(ErrorMessage = "Se requiere ingresar la Razón Social")]
        [StringLength(50, ErrorMessage = "Máximo 50 caracteres y mínimo 3", MinimumLength = 3)]
        public string ClienteRazonSocial { get; set; }
        [Display(Name = "Nombre Contacto")]
        [Required(ErrorMessage = "Se requiere ingresar el Nombre de Contacto")]
        [StringLength(50, ErrorMessage = "Máximo 50 caracteres y mínimo 3", MinimumLength = 3)]
        public string ClienteNombreContacto { get; set; }
        [Display(Name = "Dirección")]
        [Required(ErrorMessage = "Se requiere ingresar la Dirección")]
        [StringLength(50, ErrorMessage = "Máximo 50 caracteres y mínimo 3", MinimumLength = 3)]
        public string ClienteDireccion { get; set; }
        [Display(Name = "Teléfono")]
        [Required(ErrorMessage = "Se requiere ingresar el Teléfono")]
        [StringLength(10, ErrorMessage = "Máximo 10 caracteres", MinimumLength = 10)]
        public string ClienteTelefono { get; set; }
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Se requiere ingresar el Email")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Ingrese una dirección de correo electrónico válida")]
        public string ClienteEmail { get; set; }
        public virtual List<Pedido> Pedidos { get; set; }
    }
}