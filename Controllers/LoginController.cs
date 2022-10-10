using MedToxQui.Helper;
using MedToxQui.Models;
using MedToxQui.Models.ViewModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MedToxQui.Controllers
{
    public class LoginController : Controller
    {
        CodeStackCTX ctx;

        public LoginController(CodeStackCTX _ctx)
        {
            ctx = _ctx;
        }

        public IActionResult Index()
        {
            return View();
        }

        [BindProperty]
        public UsuarioVM Usuario { get; set; }

        public async Task<IActionResult> Login()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new JObject() {
                    { "Statuscode",  400 },
                    { "Message", "El usuario ya existe seleccione otro."  }
                });
            }
            else
            {
                var result = await ctx.Usuarios.Include("Roles.Rol").Where(x => x.Nombre == Usuario.Nombre).SingleOrDefaultAsync();
                if (result == null)
                {
                    return NotFound(new JObject() {
                        { "Statuscode",  404 },
                        { "Message", "Usuario no encontrado."  }
                    });
                }
                else
                {
                    //Usuario econtrado e identificado y generando el usaurio de sesion
                    if (HashHelper.CheckHash(Usuario.Clave, result.Clave, result.Sal))
                    {
                        if (result.Roles.Count == 0)
                        {
                            var response = new JObject() {
                            { "Statuscode",  403 },
                            { "Message", "No tiene acceso al sistema."  }
                        };
                            return StatusCode(403, response);
                        }

                        var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Name, ClaimTypes.Role);
                        identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, result.IdUsuario.ToString()));
                        identity.AddClaim(new Claim(ClaimTypes.Name, result.Nombre));
                        identity.AddClaim(new Claim(ClaimTypes.Email, "correopersonal@corre.com"));
                        identity.AddClaim(new Claim("Dato", "Valor"));
                        identity.AddClaim(new Claim(ClaimTypes.Actor, result.NombreUsuario));  //Mostrar el Nombre completo

                        foreach (var Rol in result.Roles)
                        {
                            identity.AddClaim(new Claim(ClaimTypes.Role, Rol.Rol.Descripcion));
                        }

                        var principal = new ClaimsPrincipal(identity);
                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties { ExpiresUtc = DateTime.Now.AddDays(1), IsPersistent = true });
                        //return Ok(result);
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        var response = new JObject() {
                            { "Statuscode",  403 },
                            { "Message", "Usuario o contraseña no Válido."  }
                        };
                        return StatusCode(403, response);
                    }
                }
            }
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Login");
        }
    }
}
