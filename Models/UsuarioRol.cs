using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedToxQui.Models
{
    [Table("UsuarioRol")]
    public partial class UsuarioRol
    {
        public int IdUsuario { get; set; }

        public int IdRol { get; set; }

        [ForeignKey("IdRol")]
        public virtual Roles Rol { get; set; }
    }
}
