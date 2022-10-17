using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MedToxQui.Models.Consultas;
using MedToxQui.Data;
using Microsoft.AspNetCore.Authorization;

namespace MedToxQui.Controllers
{
    [Authorize]

    public class ConsultasController : Controller
    {
        private DBOperaciones repo;

        public ConsultasController()
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
                return View(repo.Getdosparam1<EntradaDiariaModel>("sp_medico_entrada_diaria_x_fecha", new { @fecha = fecha }).ToList());
            }
        }

        [Authorize(Roles = "Administrador, Quimica")]
        public IActionResult IndexTotalizadoDiario(string fecha = "")
        {
            if (fecha == "")
            {
                return View();
            }
            else
            {
                return View(repo.Getdosparam1<Datos_Index>("sp_medicos_quimica_totalizado_actual_dependencia_genero", new { @fecha = fecha }).ToList());
            }
        }

        [Authorize(Roles = "Administrador, Quimica")]
        public IActionResult IndexAnalisis(string fecha = "")
        {
            if (fecha == "")
            {
                return View();
            }
            else
            {
                return View(repo.Getdosparam1<ListaExamenesQuimicosModel>("sp_medicos_lista_estudios_quimicos", new { @fecha = fecha }).ToList());
            }
        }

        public IActionResult IndexGrafica(string fecha01 = "", string fecha02 = "")
        {
            if (fecha01 == "" || fecha02 == "")
            {
                ViewBag.totalGenero = repo.Getdosparam1<GraficasQuimicas>("sp_medicos_quimica_obtener_valores_graficas", new { @opcion = 2, @fecha01 = "01/01/1900", @fecha02 = "01/01/1900" }).ToList();
                ViewBag.totalInstitucion = repo.Getdosparam1<GraficasQuimicas>("sp_medicos_quimica_obtener_valores_graficas", new { @opcion = 4, @fecha01 = "01/01/1900", @fecha02 = "01/01/1900" }).ToList();
                ViewBag.totalTipoEvaluacion = repo.Getdosparam1<GraficasQuimicas>("sp_medicos_quimica_obtener_valores_graficas", new { @opcion = 3, @fecha01 = "01/01/1900", @fecha02 = "01/01/1900" }).ToList();
                ViewBag.totalAnalitos = repo.Getdosparam1<GraficasQuimicas>("sp_medicos_quimica_obtener_valores_graficas", new { @opcion = 1, @fecha01 = "01/01/1900", @fecha02 = "01/01/1900" }).ToList();

                return View();
            }
            else
            {
                ViewBag.totalGenero = repo.Getdosparam1<GraficasQuimicas>("sp_medicos_quimica_obtener_valores_graficas", new { @opcion = 2, @fecha01 = fecha01, @fecha02 = fecha02 }).ToList();
                ViewBag.totalInstitucion = repo.Getdosparam1<GraficasQuimicas>("sp_medicos_quimica_obtener_valores_graficas", new { @opcion = 4, @fecha01 = fecha01, @fecha02 = fecha02 }).ToList();
                ViewBag.totalTipoEvaluacion = repo.Getdosparam1<GraficasQuimicas>("sp_medicos_quimica_obtener_valores_graficas", new { @opcion = 3, @fecha01 = fecha01, @fecha02 = fecha02 }).ToList();
                ViewBag.totalAnalitos = repo.Getdosparam1<GraficasQuimicas>("sp_medicos_quimica_obtener_valores_graficas", new { @opcion = 1, @fecha01 = fecha01, @fecha02 = fecha02 }).ToList();

                return View();
            }
        }
    }
}
