using MedToxQui.Data;
using MedToxQui.Helper;
using MedToxQui.Models;
using MedToxQui.Models.Consultas;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MedToxQui.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private DBOperaciones repo;
        CodeStackCTX ctx;

        public HomeController(CodeStackCTX _ctx)
        {
            repo = new DBOperaciones();
            ctx = _ctx;
        }

        public IActionResult Index()
        {
            if(SessionHelper.GetNameRol(User) == "Administrador" || SessionHelper.GetNameRol(User) == "Quimica")
            {
                ViewBag.totalGeneral = repo.Getdosparam1<Datos_Index>("sp_medicos_datos_index", new { @opcion = 1 }).FirstOrDefault();
                ViewBag.totalHombre = repo.Getdosparam1<Datos_Index>("sp_medicos_datos_index", new { @opcion = 2 }).FirstOrDefault();
                ViewBag.totalMujer = repo.Getdosparam1<Datos_Index>("sp_medicos_datos_index", new { @opcion = 3 }).FirstOrDefault();
                ViewBag.totalConfirmatorio = repo.Getdosparam1<Datos_Index>("sp_medicos_datos_index", new { @opcion = 6 }).FirstOrDefault();
                ViewBag.avanceQuimica = repo.Getdosparam1<Datos_Index>("sp_medicos_datos_index", new { @opcion = 7 }).ToList();
            }            

            return View();
        }

        [Authorize(Roles = "Administrador")]
        public IActionResult Registro()
        {
            return View();
        }

        [BindProperty]
        public Usuarios Usuario { get; set; }
        public async Task<IActionResult> Registrar()
        {
            var result = await ctx.Usuarios.Where(x => x.Nombre == Usuario.Nombre).SingleOrDefaultAsync();
            if (result != null)
            {
                return BadRequest(new JObject() {
                    { "Statuscode",  400 },
                    { "Message", "El usuario ya existe seleccione otro."  }
                });
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState.SelectMany(x => x.Value.Errors.Select(y => y.ErrorMessage)).ToList());
                }
                else
                {
                    var hash = HashHelper.Hash(Usuario.Clave);
                    Usuario.Clave = hash.Password;
                    Usuario.Sal = hash.Salt;
                    Usuario.Activo = true;
                    ctx.Usuarios.Add(Usuario);
                    await ctx.SaveChangesAsync();
                    Usuario.Clave = "";
                    Usuario.Sal = "";
                    return Created($"/Usuarios/{Usuario.IdUsuario}", Usuario);
                }
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
