using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedToxQui.Models.QuimicaModel
{
    public class ToxicologiaModel
    {
        public int idhistorico { get; set; }
        public string RS_FOLIO { get; set; }
        public DateTime RS_F_RESULT { get; set; }   //Guarda la fecha del resultado
        public DateTime RS_H_RESULT { get; set; }   //Guarda la fecha y la hora del resultado
        public string RS_TMUESTRA { get; set; }
        public bool RS_NEGAT { get; set; }
        public bool RS_POSIT { get; set; }
        public bool RS_INVALIDO { get; set; }
        public bool RS_MARI { get; set; }
        public bool RS_COCA { get; set; }
        public bool RS_ANFE { get; set; }
        public bool RS_BENZO { get; set; }
        public bool RS_BARBI { get; set; }
        public bool RS_META { get; set; }
        public string RS_METODO { get; set; }
        public string RS_OBSERVA { get; set; }
        public string idUsuario_frm { get; set; }   //Usuario que se firma
        public string @usuariosuperviso { get; set; }
        public int accion { get; set; }
    }
}
