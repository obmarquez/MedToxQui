using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedToxQui.Models.Consultas
{
    public class BhModel
    {
        public string FOLIO { get; set; }
        public decimal wbc { get; set; }
        public decimal Limph2 { get; set; }
        public decimal Limph { get; set; }
        public decimal Mid2 { get; set; }
        public decimal Mid { get; set; }
        public decimal Neu2 { get; set; }
        public decimal Neu { get; set; }
        public decimal Eos2 { get; set; }
        public decimal Eos { get; set; }
        public decimal Bas2 { get; set; }
        public decimal Bas { get; set; }
        public decimal HGB { get; set; }
        public decimal RBC { get; set; }
        public decimal HTC { get; set; }
        public decimal MCv { get; set; }
        public decimal MCH { get; set; }
        public decimal MCHC { get; set; }
        public decimal PLT { get; set; }
        public decimal PCT { get; set; }
        public decimal RDWCV { get; set; }
        public decimal RDWSD { get; set; }
        public decimal MPV { get; set; }
        public decimal PDW { get; set; }
        public string realizo { get; set; }
        public string ced_rea { get; set; }
        public string superviso { get; set; }
        public string ced_sup { get; set; }
        public string Observacion { get; set; }
        //public string fr_hgb { get; set; }
        public int fr_hgb { get; set; }
        //public string fr_rbc { get; set; }
        public int fr_rbc { get; set; }
        //public string fr_htc { get; set; }
        public int fr_htc { get; set; }
        //public string fr_mcv { get; set; }
        public int fr_mcv { get; set; }
        //public string fr_mch { get; set; }
        public int fr_mch { get; set; }
        //public string fr_mchc { get; set; }
        public int fr_mchc { get; set; }
        //public string fr_plt { get; set; }
        public int fr_plt { get; set; }
        //public string fr_pct { get; set; }
        public int fr_pct { get; set; }
        //public string fr_rdwcv { get; set; }
        public int fr_rdwcv { get; set; }
        //public string fr_rdwsd { get; set; }
        public int fr_rdwsd { get; set; }
        //public string fr_mpv { get; set; }
        public int fr_mpv { get; set; }
        //public string fr_pdw { get; set; }
        public int fr_pdw { get; set; }
        public string Tiposangre { get; set; }
        public string F_procesahematica { get; set; }
        public string Usu_rea_bh { get; set; }
        public string Usu_superviso { get; set; }
        public int fr_limph { get; set; }
        public int fr_mid { get; set; }
        public int fr_neu { get; set; }
        public int fr_eos { get; set; }
        public int fr_bas { get; set; }
        public int fr_wbc { get; set; }
        public decimal Banda2 { get; set; }
        public decimal Banda { get; set; }
        public int fr_banda { get; set; }
    }
}
