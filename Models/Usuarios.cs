using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MedToxQui.Models
{

    [Table("Usuarios")]
    public partial class Usuarios
    {
        [Key]
        public int IdUsuario { get; set; }
        [Required(ErrorMessage = "Escriba el nombre del usuario.")]
        [MinLength(3, ErrorMessage = "Escriba al menos 3 caracteres.")]
        [MaxLength(7, ErrorMessage = "Escriba máximo 15 caracteres.")]
        public string Nombre { get; set; }
        public string Sal { get; set; }

        [Required(ErrorMessage = "Escriba la clave del usuario.")]
        [MinLength(7, ErrorMessage = "Escriba al menos 5 caracteres.")]
        [MaxLength(50, ErrorMessage = "Escriba máximo 50 caracteres.")]
        public string Clave { get; set; }

        #region Nuevos campos

        [Required(ErrorMessage = "Escriba el nombre completo del usuario.")]
        [MinLength(15, ErrorMessage = "Escriba al menos 15 caracteres.")]
        [MaxLength(70, ErrorMessage = "Escriba máximo 70 caracteres.")]
        public string NombreUsuario { get; set; }

        [Required(ErrorMessage = "Escriba la Cedula del usuario.")]
        [MinLength(7, ErrorMessage = "Escriba al menos 10 caracteres.")]
        [MaxLength(10, ErrorMessage = "Escriba máximo 10 caracteres.")]
        public string Cedula { get; set; }

        public bool Activo { get; set; }
        #endregion

        [ForeignKey("IdUsuario")]
        public virtual List<UsuarioRol> Roles { get; set; }

    }

}
