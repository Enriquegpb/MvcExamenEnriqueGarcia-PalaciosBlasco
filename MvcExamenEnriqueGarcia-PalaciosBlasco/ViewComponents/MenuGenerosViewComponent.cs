using Microsoft.AspNetCore.Mvc;
using MvcExamenEnriqueGarcia_PalaciosBlasco.Models;
using MvcExamenEnriqueGarcia_PalaciosBlasco.Repositories;
using NuGet.Protocol.Core.Types;

namespace MvcExamenEnriqueGarcia_PalaciosBlasco.ViewComponents
{
    public class MenuGenerosViewComponent: ViewComponent
    {
        private RepositoryLibros repo;
        public MenuGenerosViewComponent(RepositoryLibros repo)
        {
            this.repo = repo;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<Genero> generos = await repo.GetGeneros();
            return View(generos);
        }
    }
}
