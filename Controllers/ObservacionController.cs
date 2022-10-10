using MedToxQui.Data;
using MedToxQui.Helper;
using MedToxQui.Models.Observacion;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedToxQui.Controllers
{
    public class ObservacionController : Controller
    {
        private DBOperaciones repo;

        public ObservacionController()
        {
            repo = new DBOperaciones();
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult js_agregar_observacion(int p_idHistorico, string p_observacionPublica)
        {
            Observaciones nuevaOBs = new Observaciones();
            nuevaOBs.idHistorico = p_idHistorico;
            nuevaOBs.idusuario = SessionHelper.GetName(User);
            nuevaOBs.observacionpublica = p_observacionPublica;

            string resultado = "Ok";

            repo.Getdosparam2("sp_general_inserta_observacion_sin_Result", nuevaOBs);

            return Json(resultado);
        }
    }
}
