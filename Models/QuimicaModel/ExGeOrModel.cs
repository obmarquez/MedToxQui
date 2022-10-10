using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedToxQui.Models.QuimicaModel
{
    public class ExGeOrModel
    {
        public int idhistorico { get; set; }
        public string FOLIO { get; set; }
        public string Aspecto { get; set; }
        public string Color { get; set; }
        public decimal PH { get; set; }
        public decimal Densidad { get; set; }
        public string Glucosa { get; set; }
        public string Bilirrubina { get; set; }
        public string Cetona { get; set; }
        public string Proteinas { get; set; }
        public decimal Urobilinogeno { get; set; }
        public string Sangre { get; set; }
        public string Nitritos { get; set; }
        public string Leucocitos { get; set; }
        public string Eritrocitos { get; set; }
        public string Bacterias { get; set; }
        public string Celulas { get; set; }
        public string Cilindros { get; set; }
        public string Cristales { get; set; }
        public string Observaciones { get; set; }
        public string usu_rea_ego { get; set; }
        public string usu_gra_ego { get; set; }
        public string AcidoAscorbico { get; set; }
        public string usu_superviso { get; set; }
        public DateTime f_procesamiento { get; set; }
        public int accion { get; set; }
    }
}
