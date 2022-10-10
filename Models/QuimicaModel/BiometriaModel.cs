using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedToxQui.Models.QuimicaModel
{
    public class BiometriaModel
    {
        public int Idhistorico { get; set; }
        public string FOLIO { get; set; }
        public decimal WBC { get; set; }
        public decimal Limph { get; set; }
        public decimal Mid { get; set; }
        public decimal Neu { get; set; }
        public decimal Eos { get; set; }
        public decimal Bas { get; set; }
        public decimal Limph2 { get; set; }
        public decimal Mid2 { get; set; }
        public decimal Neu2 { get; set; }
        public decimal Eos2 { get; set; }
        public decimal Bas2 { get; set; }
        public decimal HGB { get; set; }
        public decimal RBC { get; set; }
        public decimal HTC { get; set; }
        public decimal MCv { get; set; }
        public decimal MCH { get; set; }
        public decimal MCHC { get; set; }
        public decimal RDWCV { get; set; }
        public decimal RDWSD { get; set; }
        public decimal PLT { get; set; }
        public decimal MPV { get; set; }
        public decimal PDW { get; set; }
        public decimal PCT { get; set; }
        public string Observacion { get; set; }
        public string Tiposangre { get; set; }
        public string Usu_rea_bh { get; set; }
        public string Usu_gra_bh { get; set; }
        public string Usu_superviso { get; set; }
        public DateTime F_procesahematica { get; set; }
        public int Accion { get; set; }
    }
}
