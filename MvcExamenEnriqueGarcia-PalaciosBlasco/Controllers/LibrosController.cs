using Microsoft.AspNetCore.Mvc;
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
    }
}
