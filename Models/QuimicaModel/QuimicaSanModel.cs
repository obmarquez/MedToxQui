using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedToxQui.Models.QuimicaModel
{
    public class QuimicaSanModel
    {
        public int idhistorico { get; set; }
        public string folio { get; set; }
        public decimal glucosa { get; set; }
        public decimal acido { get; set; }
        public decimal colesterol { get; set; }
        public decimal trigliceridos { get; set; }
        public decimal urea { get; set; }
        public decimal creatinina { get; set; }
        public decimal colesterolAlta { get; set; }
        public decimal colesterolBaja { get; set; }
        public string observacion { get; set; }
        public string usu_rea_qs { get; set; }
        public string usu_gra_qs { get; set; }
        public string usu_superviso { get; set; }
        public string metodologia { get; set; }
        public DateTime f_procesamientosangre { get; set; }
        public int accion { get; set; }
    }
}
