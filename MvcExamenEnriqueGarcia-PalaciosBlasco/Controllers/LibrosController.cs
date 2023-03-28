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

        public async Task<IActionResult> Index(int? posicion,int? genero)
        {
            if (posicion == null)
            {
                posicion = 1;
            }
            int numregistros = await this.repo.GetLibrosTotal();
            //ESTAMOS EN LA POSICION 1, QUE TENEMOS QUE DEVOLVER A LA VISTA?
            int siguiente = posicion.Value + 1;
            if (siguiente > numregistros)
            {
                //EFECTO OPTICO
                siguiente = numregistros;
            }
            int anterior = posicion.Value - 1;
            if (anterior < 1)
            {
                anterior = 1;
            }
            //Libros vistaLibros =
            //    await this.repo.GetLibrosSession(posicion.Value);
            ViewData["ULTIMO"] = numregistros;
            ViewData["SIGUIENTE"] = siguiente;
            ViewData["ANTERIOR"] = anterior;

            List<Libro> libros = new List<Libro>();
            if(genero == null)
            {
                 libros = await this.repo.GetLibros();

            }
            else
            {
                libros = this.repo.GetLibrosGenero(genero.Value, posicion.Value, ref numregistros);
            }
            
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
