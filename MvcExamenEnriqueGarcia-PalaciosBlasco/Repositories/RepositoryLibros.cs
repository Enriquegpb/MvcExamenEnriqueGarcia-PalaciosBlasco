using Microsoft.EntityFrameworkCore;
using MvcExamenEnriqueGarcia_PalaciosBlasco.Data;
using MvcExamenEnriqueGarcia_PalaciosBlasco.Models;

namespace MvcExamenEnriqueGarcia_PalaciosBlasco.Repositories
{
    public class RepositoryLibros
    {
        private LibrosContext context;
        public RepositoryLibros (LibrosContext context)
        {
            this.context = context;
        }

        public async Task<List<Libro>> GetLibros()
        {
            return await this.context.Libros.ToListAsync();
        }

        public async Task<List<Genero>> GetGeneros()
        {
            return await this.context.Generos.ToListAsync();
        }
    }
}
