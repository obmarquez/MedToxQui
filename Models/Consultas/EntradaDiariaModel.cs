using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedToxQui.Models.Consultas
{
    public class EntradaDiariaModel
    {
        public string codigoevaluado { get; set; }
        public string curp { get; set; }
        public string nombre { get; set; }
        public string folio { get; set; }
        public string gaf { get; set; }
        public string cevaluacion { get; set; }
        public string dependencia { get; set; }
        public string grupo { get; set; }
    }
}
