using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedToxQui.Models.Consultas
{
    public class ConsultasModel
    {
        //generales
        public int idhistorico { get; set; }

        //evaluado
        public string evaluado { get; set; }
        public int edad { get; set; }
        public string sexo { get; set; }
        public string rfc { get; set; }
        public string codigoevaluado { get; set; }
        public string curp { get; set; }
        public string adscripcion { get; set; }

        //evaluacion
        public string fecha { get; set; }
        public string puesto { get; set; }
        public string evaluacion { get; set; }
        public string dependencia { get; set; }

        //Aceptacion
        public string folio { get; set; }

        //Test
        public int idTest { get; set; }

        //Test Fagerstrom
        public int hayNicotina { get; set; }
        public int p1 { get; set; }
        public int p2 { get; set; }
        public int p3 { get; set; }
        public int p4 { get; set; }
        public int p5 { get; set; }
        public int p6 { get; set; }
        public int p7 { get; set; }

        //Test Audit
        public int hayAudit { get; set; }
        public int pregunta1 { get; set; }
        public int pregunta2 { get; set; }
        public int pregunta3 { get; set; }
        public int pregunta4 { get; set; }
        public int pregunta5 { get; set; }
        public int pregunta6 { get; set; }
        public int pregunta7 { get; set; }
        public int pregunta8 { get; set; }
        public int pregunta9 { get; set; }
        public int pregunta10 { get; set; }

        //Test Medicamenteso
        public int hayMed { get; set; }
        public string padeceenfermedad { get; set; }
        public string enfermedad { get; set; }
        public string tomamedicamento { get; set; }
        public string medicamento { get; set; }
        public string cantidad { get; set; }
        public string tiempo { get; set; }
        public string consumiodroga { get; set; }
        public string droga { get; set; }
        public string frecuenciadroga { get; set; }
        public string cantidaddroga { get; set; }
        public string cReceta { get; set; }

        //C3
        public string certifica { get; set; }
        public string acredita { get; set; }

        public string area { get; set; }
        public string observacion { get; set; }

        //custodia
        public string fechaCus { get; set; }
        public string observacionCus { get; set; }
        public byte[] laFoto { get; set; }
        public string usertox { get; set; }
        public string userodo { get; set; }
        public string userquim { get; set; }
        public int gafete { get; set; }
        public int grupo { get; set; }
        public string evaluador { get; set; }
        public string estado { get; set; }
        public int id { get; set; }
        public string idm { get; set; }
        public string municipio { get; set; }
        public string Nombre { get; set; }
        public string NombreUsuario { get; set; }
        public string clave { get; set; }
        public string dxCie10 { get; set; }

    }
}
