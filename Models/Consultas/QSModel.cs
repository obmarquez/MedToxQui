using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedToxQui.Models.Consultas
{
    public class QSModel
    {
        public string FOLIO { get; set; }
        public decimal Glucosa { get; set; }
        public decimal Acido { get; set; }
        public decimal Colesterol { get; set; }
        public decimal Trigliceridos { get; set; }
        public decimal Urea { get; set; }
        public decimal Creatinina { get; set; }
        public decimal colesterolAlta { get; set; }
        public decimal colesterolBaja { get; set; }
        public string realizo { get; set; }
        public string ced_rea { get; set; }
        public string superviso { get; set; }
        public string ced_sup { get; set; }
        public string metodologia { get; set; }
        public string Observacion { get; set; }
        public string resGlu { get; set; }
        public string resAci { get; set; }
        public string resCol { get; set; }
        public string resTri { get; set; }
        public string resUre { get; set; }
        public string resCre { get; set; }
        public string resColAlt { get; set; }
        public string resColBaj { get; set; }
        public string f_procesamientoqs { get; set; }
    }
}
