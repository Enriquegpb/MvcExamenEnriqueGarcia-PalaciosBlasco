using Microsoft.AspNetCore.Mvc;
using MvcExamenEnriqueGarcia_PalaciosBlasco.Extensions;
using MvcExamenEnriqueGarcia_PalaciosBlasco.Filters;
using MvcExamenEnriqueGarcia_PalaciosBlasco.Models;
using MvcExamenEnriqueGarcia_PalaciosBlasco.Repositories;

namespace MvcExamenEnriqueGarcia_PalaciosBlasco.Controllers
{
    public class LibrosController : Controller
    {
        private RepositoryLibros repo;
        public LibrosController(RepositoryLibros repo)
        {
            this.repo = repo;
        }

        public async Task<IActionResult> Index()
        {
            List<Libro> libros = await this.repo.GetLibros();   
            return View(libros);
        } 
        public async Task<IActionResult> Details(int idlibro)
        {
            Libro libro = await this.repo.FindLibro(idlibro);   
            return View(libro);
        }
        [AuthorizeUsuarios]
        public async Task<IActionResult> MiCarrito(int idlibro)
        {
            Libro libro = await this.repo.FindLibro(idlibro);

            List<Libro> libros = HttpContext.Session.GetObject<List<Libro>>("LISTALIBROS");
            if (libros == null)
            {
                libros = new List<Libro>();
            }
            libros.Add(libro);
            HttpContext.Session.SetObject("LISTALIBROS", libros);
            return RedirectToAction("PedidoFinal");


        }

        [AuthorizeUsuarios]
        public IActionResult PedidoFinal()
        {
            return View();
        }

        public async Task<IActionResult> Login()
        {
            return View();
        }
        [AuthorizeUsuarios]
        public IActionResult PerfilUsuario()
        {
            return View();
        }
    }
}
