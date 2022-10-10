using Microsoft.AspNetCore.Mvc;
using MedToxQui.Models.QuimicaModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using MedToxQui.Data;
using MedToxQui.Models.Consultas;
using MedToxQui.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using MedToxQui.Helper;

namespace MedToxQui.Controllers
{
    [Authorize]

    public class QuimicaController : Controller
    {
        private DBOperaciones repo;

        public QuimicaController()
        {
            repo = new DBOperaciones();
        }

        [Authorize(Roles = "Administrador, Quimica")]
        public IActionResult Index(string fecha = "")
        {
            if (fecha == "")
            {
                return View();
            }
            else
            {
                ViewBag.fechapasarTox = fecha;

                List<SelectListItem> metodoQuimica = new List<SelectListItem>();
                metodoQuimica.Add(new SelectListItem { Text = "Análisis cuantitativo / Dimensión RLX MAX", Value = "Análisis cuantitativo / Dimensión RLX MAX" });
                metodoQuimica.Add(new SelectListItem { Text = "Análisis cuantitativo / Mindray BS120", Value = "Análisis cuantitativo / Mindray BS120" });
                ViewBag.metodologiaQS = metodoQuimica;

                List<SelectListItem> toxMuestra = new List<SelectListItem>();
                toxMuestra.Add(new SelectListItem { Text = "Orina", Value = "Orina" });
                toxMuestra.Add(new SelectListItem { Text = "Sangre", Value = "Sangre" });
                toxMuestra.Add(new SelectListItem { Text = "Cabello", Value = "Cabello" });
                toxMuestra.Add(new SelectListItem { Text = "Saliva", Value = "Saliva" });
                ViewBag.toxMuestras = toxMuestra;

                List<SelectListItem> toxResultados_1 = new List<SelectListItem>();
                toxResultados_1.Add(new SelectListItem { Text = "Negativo", Value = "Negativo" });
                toxResultados_1.Add(new SelectListItem { Text = "Positivo", Value = "Positivo" });
                toxResultados_1.Add(new SelectListItem { Text = "Invalido", Value = "Invalido" });
                ViewBag.toxRes_1 = toxResultados_1;

                List<SelectListItem> toxResultados_2 = new List<SelectListItem>();
                toxResultados_2.Add(new SelectListItem { Text = "Negativo", Value = "Negativo" });
                toxResultados_2.Add(new SelectListItem { Text = "Positivo", Value = "Positivo" });
                ViewBag.toxRes_2 = toxResultados_2;

                List<SelectListItem> comboEgo = new List<SelectListItem>();
                comboEgo.Add(new SelectListItem { Text = "Negativo", Value = "Negativo" });
                comboEgo.Add(new SelectListItem { Text = "Escasas", Value = "Escasas" });
                comboEgo.Add(new SelectListItem { Text = "Moderadas", Value = "Moderadas" });
                comboEgo.Add(new SelectListItem { Text = "Abundantes", Value = "Abundantes" });
                ViewBag.cmbEgo = comboEgo;

                List<SelectListItem> metodoTox = new List<SelectListItem>();
                metodoTox.Add(new SelectListItem { Text = "Inmunoensayo cromatográfico", Value = "Inmunoensayo cromatográfico" });
                metodoTox.Add(new SelectListItem { Text = "Inmunoensayo enzimático", Value = "Inmunoensayo enzimático" });
                ViewBag.metodologiaTOX = metodoTox;

                ViewBag.losQuimicos = repo.Getdosparam1<Usuarios>("sp_medicos_obtener_usuarios", new { @opcion = 1 }).ToList();
                return View(repo.Getdosparam1<ListaExamenesQuimicosModel>("sp_medicos_lista_estudios_quimicos_a_realizar", new { @fecha = fecha }).ToList());
            }
        }

        public IActionResult AgregaActualizaEGO(int idhistorico, string FOLIO, string Aspecto, string Color, decimal PH, decimal Densidad, string Glucosa, string Bilirrubina, string Cetona, string Proteinas, decimal Urobilinogeno, string Sangre, string Nitritos, string Leucocitos, string Eritrocitos, string Bacterias, string Celulas, string Cilindros, string Cristales, string Observaciones, string usu_rea_ego, string AcidoAscorbico, string usu_superviso, DateTime f_procesamiento, int accion)
        {
            ExGeOrModel ExGeOr = new ExGeOrModel();
            ExGeOr.idhistorico = idhistorico;
            ExGeOr.FOLIO = FOLIO;
            ExGeOr.Aspecto = Aspecto;
            ExGeOr.Color = Color;
            ExGeOr.PH = PH;
            ExGeOr.Densidad = Densidad;
            ExGeOr.Glucosa = Glucosa;
            ExGeOr.Bilirrubina = Bilirrubina;
            ExGeOr.Cetona = Cetona;
            ExGeOr.Proteinas = Proteinas;
            ExGeOr.Urobilinogeno = Urobilinogeno;
            ExGeOr.Sangre = Sangre;
            ExGeOr.Nitritos = Nitritos;
            ExGeOr.Leucocitos = Leucocitos;
            ExGeOr.Eritrocitos = Eritrocitos;
            ExGeOr.Bacterias = Bacterias;
            ExGeOr.Celulas = Celulas;
            ExGeOr.Cilindros = Cilindros;
            ExGeOr.Cristales = Cristales;
            ExGeOr.Observaciones = Observaciones;
            ExGeOr.usu_rea_ego = usu_rea_ego;
            ExGeOr.usu_gra_ego = SessionHelper.GetName(User);
            ExGeOr.AcidoAscorbico = AcidoAscorbico;
            ExGeOr.usu_superviso = usu_superviso;
            ExGeOr.f_procesamiento = f_procesamiento;
            ExGeOr.accion = accion;

            string resultado = "Ok";

            repo.Getdosparam2("sp_medicos_quimica_agrega_actualiza_ego", ExGeOr);

            return Json(resultado);
        }

        public IActionResult AgregaActualizaBH(int Idhistorico, string FOLIO, decimal WBC, decimal Limph, decimal Mid, decimal Neu, decimal Eos, decimal Bas, decimal Limph2, decimal Mid2, decimal Neu2, decimal Eos2, decimal Bas2, decimal HGB, decimal RBC, decimal HTC, decimal MCv, decimal MCH, decimal MCHC, decimal RDWCV, decimal RDWSD, decimal PLT, decimal MPV, decimal PDW, decimal PCT, string Observacion, string Tiposangre, string Usu_rea_bh, string Usu_superviso, DateTime F_procesahematica, int Accion)
        {
            BiometriaModel BHModel = new BiometriaModel();
            BHModel.Idhistorico = Idhistorico;
            BHModel.FOLIO = FOLIO;
            BHModel.WBC = WBC;
            BHModel.Limph = Limph;
            BHModel.Mid = Mid;
            BHModel.Neu = Neu;
            BHModel.Eos = Eos;
            BHModel.Bas = Bas;
            BHModel.Limph2 = Limph2;
            BHModel.Mid2 = Mid2;
            BHModel.Neu2 = Neu2;
            BHModel.Eos2 = Eos2;
            BHModel.Bas2 = Bas2;
            BHModel.HGB = HGB;
            BHModel.RBC = RBC;
            BHModel.HTC = HTC;
            BHModel.MCv = MCv;
            BHModel.MCH = MCH;
            BHModel.MCHC = MCHC;
            BHModel.RDWCV = RDWCV;
            BHModel.RDWSD = RDWSD;
            BHModel.PLT = PLT;
            BHModel.MPV = MPV;
            BHModel.PDW = PDW;
            BHModel.PCT = PCT;
            BHModel.Observacion = Observacion;
            BHModel.Tiposangre = Tiposangre;
            BHModel.Usu_rea_bh = Usu_rea_bh;
            BHModel.Usu_gra_bh = SessionHelper.GetName(User);
            BHModel.Usu_superviso = Usu_superviso;
            BHModel.F_procesahematica = F_procesahematica;
            BHModel.Accion = Accion;

            string resBH = "Ok";

            repo.Getdosparam2("sp_medicos_quimica_agrega_actualiza_bh", BHModel);

            return Json(resBH);
        }

        public IActionResult AgregaActualizaQS(int p_idhistorico, string p_folio, decimal p_glucosa, decimal p_acido, decimal p_colesterol, decimal p_trigliceridos, decimal p_urea, decimal p_creatinina, decimal p_colesterolAlta, decimal p_colesterolBaja, string p_observacion, string p_usu_rea_qs, string p_usu_superviso, string p_metodologia, DateTime p_f_procesamientosangre, int p_accion)
        {
            string resQS = "Ok";

            QuimicaSanModel QSanModel = new QuimicaSanModel();
            QSanModel.idhistorico = p_idhistorico;
            QSanModel.folio = p_folio;
            QSanModel.glucosa = p_glucosa;
            QSanModel.acido = p_acido;
            QSanModel.colesterol = p_colesterol;
            QSanModel.trigliceridos = p_trigliceridos;
            QSanModel.urea = p_urea;
            QSanModel.creatinina = p_creatinina;
            QSanModel.colesterolAlta = p_colesterolAlta;
            QSanModel.colesterolBaja = p_colesterolBaja;
            QSanModel.observacion = p_observacion;
            QSanModel.usu_rea_qs = p_usu_rea_qs;
            QSanModel.usu_gra_qs = SessionHelper.GetName(User);
            QSanModel.usu_superviso = p_usu_superviso;
            QSanModel.metodologia = p_metodologia;
            QSanModel.f_procesamientosangre = p_f_procesamientosangre;
            QSanModel.accion = p_accion;

            repo.Getdosparam2("sp_medicos_quimica_agrega_actualiza_qs", QSanModel);

            return Json(resQS);
        }

        public IActionResult AgregaActualizaTOX(int p_idhistorico, string p_RS_FOLIO, DateTime p_RS_F_RESULT, string p_RS_TMUESTRA, bool p_RS_NEGAT, bool p_RS_POSIT, bool p_RS_INVALIDO, bool p_RS_MARI, bool p_RS_COCA, bool p_RS_ANFE, bool p_RS_BENZO, bool p_RS_BARBI, string p_RS_METODO, string p_RS_OBSERVA, DateTime p_RS_H_RESULT, bool p_RS_META, int p_accion, string p_usuariosuperviso)
        {
            string resTox = "Ok";

            ToxicologiaModel QuimTox = new ToxicologiaModel();
            QuimTox.idhistorico = p_idhistorico;
            QuimTox.RS_FOLIO = p_RS_FOLIO;
            QuimTox.RS_F_RESULT = p_RS_F_RESULT;
            QuimTox.RS_TMUESTRA = p_RS_TMUESTRA;
            QuimTox.RS_NEGAT = p_RS_NEGAT;
            QuimTox.RS_POSIT = p_RS_POSIT;
            QuimTox.RS_INVALIDO = p_RS_INVALIDO;
            QuimTox.RS_MARI = p_RS_MARI;
            QuimTox.RS_COCA = p_RS_COCA;
            QuimTox.RS_ANFE = p_RS_ANFE;
            QuimTox.RS_BENZO = p_RS_BENZO;
            QuimTox.RS_BARBI = p_RS_BARBI;
            QuimTox.RS_METODO = p_RS_METODO;
            QuimTox.RS_OBSERVA = p_RS_OBSERVA;
            //QuimTox.RS_H_RESULT = p_RS_H_RESULT;
            QuimTox.RS_H_RESULT = Convert.ToDateTime(p_RS_H_RESULT);
            QuimTox.RS_META = p_RS_META;
            QuimTox.accion = p_accion;
            QuimTox.usuariosuperviso = p_usuariosuperviso;
            QuimTox.idUsuario_frm = SessionHelper.GetName(User);

            repo.Getdosparam2("sp_medicos_quimica_agrega_actualiza_tox", QuimTox);

            return Json(resTox);
        }

        public JsonResult ObtenerDatosAnalisisQuimicos(int idhistorico, int estudio)
        {
            var x = repo.Getdosparam1<EgoModel>("sp_medicos_quimica_obtener_analisisquimicos", new { @idhistorico = idhistorico, @idEstudio = estudio }).FirstOrDefault();

            return Json(x);
        }

        public JsonResult ObtenerDatosAnalisisQuimicoBH(int idhistorico, int estudio)
        {
            var x = repo.Getdosparam1<BhModel>("sp_medicos_quimica_obtener_analisisquimicos", new { @idhistorico = idhistorico, @idEstudio = estudio }).FirstOrDefault();
            return Json(x);
        }

        public JsonResult ObtenerDatosAnalisisQuimicosQuimisaSanguinea(int idhistorico, int estudio)
        {
            var x = repo.Getdosparam1<QSModel>("sp_medicos_quimica_obtener_analisisquimicos", new { @idhistorico = idhistorico, @idEstudio = estudio }).FirstOrDefault();
            return Json(x);
        }

        public JsonResult ObtenerDatosAnalisisQuimicosToxicologia(int idhistorico, int estudio)
        {
            var x = repo.Getdosparam1<ToxModel>("sp_medicos_quimica_obtener_analisisquimicos", new { @idhistorico = idhistorico, @idEstudio = estudio }).FirstOrDefault();
            return Json(x);
        }

        public JsonResult ObtenerObservacionesPublicas(int idhistorico)
        {
            var x = repo.Getdosparam1<ConsultasModel>("sp_general_observacionpublica_area", new { @idHistorico = idhistorico, @idArea = 2, @accion = 1, @ido = 0 }).ToList();
            return Json(x);
        }

        public JsonResult ObtenerObservacionesCustodia(int idhistorico)
        {
            var x = repo.Getdosparam1<ConsultasModel>("sp_general_observacionCustodia", new { @idHistorico = idhistorico }).ToList();
            return Json(x);
        }

        public IActionResult IndexAsociar()
        {
            ViewBag.losQuimicos = repo.Getdosparam1<Usuarios>("sp_medicos_obtener_usuarios", new { @opcion = 1 }).ToList();
            return View(repo.Getdosparam1<ConsultasModel>("sp_medicos_quimica_asociacion_lista", null).ToList());
        }

        public IActionResult AsociacionMultiple(string super, string[] input, string procesa)
        {

            foreach(var x in input)
            {
                //ViewBag.idh = x;
                repo.Getdosparam1<ConsultasModel>("sp_medicos_quimica_asociacion", new { @idhistorico = x, @userquim = super });
            }

            repo.Getdosparam1<ConsultasModel>("sp_medicos_quimica_actualizar_procesamuestra", new { @usertox = procesa });

            return RedirectToAction("IndexAsociar", "Quimica");
        }
   
    }
}
