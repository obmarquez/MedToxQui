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
    }
}
