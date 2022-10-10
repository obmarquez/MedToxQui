using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedToxQui.Models.Consultas
{
    public class ListaExamenesQuimicosModel
    {
        public string evaluado { get; set; }
        public string evaluacion { get; set; }
        public string dependencia { get; set; }
        public string idhistorico { get; set; }
        public string sexo { get; set; }
        public string FOLIO { get; set; }
        public string hayEgo { get; set; }
        public string hayBH { get; set; }
        public string hayQS { get; set; }
        public int hayTx { get; set; }
        public string fecha { get; set; }
        public int gafete { get; set; }
    }
}
