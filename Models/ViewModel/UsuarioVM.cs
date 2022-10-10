using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MedToxQui.Models.ViewModel
{
    public class UsuarioVM
    {
        [Required(ErrorMessage = "Escriba su usuario.")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "Escriba su contraseña.")]
        public string Clave { get; set; }
    }
}
