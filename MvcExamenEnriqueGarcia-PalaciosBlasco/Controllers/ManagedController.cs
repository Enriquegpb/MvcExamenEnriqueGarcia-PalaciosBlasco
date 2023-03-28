using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using MvcExamenEnriqueGarcia_PalaciosBlasco.Models;
using MvcExamenEnriqueGarcia_PalaciosBlasco.Repositories;
using NuGet.Protocol.Core.Types;
using System.Security.Claims;

namespace MvcExamenEnriqueGarcia_PalaciosBlasco.Controllers
{
    public class ManagedController : Controller
    {
        private RepositoryLibros repo;
        public ManagedController(RepositoryLibros repo)
        {
            this.repo = repo;
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(string usuario, string password)
        {
            Usuario user =await this.repo.ExisteUsuario(usuario, password);
            if(usuario == user.Nombre && password == user.Pass)
            {
                ClaimsIdentity identity =
                     new ClaimsIdentity(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    ClaimTypes.Name, ClaimTypes.Role);
                Claim claimUserName =
                    new Claim(ClaimTypes.Name, usuario);
                identity.AddClaim(claimUserName);
                Claim claimfoto =
                    new Claim("IMAGEN", user.Foto.ToString());
                identity.AddClaim(claimfoto); 
                Claim claimapllidos =
                    new Claim("APELLIDO", user.Apellidos.ToString());
                identity.AddClaim(claimapllidos);
                Claim claimemail =
                    new Claim("Email", user.Foto.ToString());
                identity.AddClaim(claimemail);

                ClaimsPrincipal userPrincipal =
              new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync
                    (CookieAuthenticationDefaults.AuthenticationScheme
                    , userPrincipal);
                return RedirectToAction("PerfilUsuario", "Libros");



            }
            return View();
        }
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync
                (CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Libros");
        }

    }
}
