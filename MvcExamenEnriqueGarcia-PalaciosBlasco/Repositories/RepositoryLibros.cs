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

        public async Task<Usuario> ExisteUsuario(string username, string password)
        {
            return await this.context.Usuarios.FirstOrDefaultAsync(u => u.Nombre == username && u.Pass == password);
        }

        public async Task<Libro> FindLibro(int idlibro)
        {
            return await this.context.Libros.FirstOrDefaultAsync(l => l.IdLibro == idlibro);
        }
        
        public async Task<List<Libro>> GetLibrosSession(List<int> ids)
        {
            var consulta = from datos in this.context.Libros
                           where ids.Contains(datos.IdLibro)
                           select datos;
            if(consulta.Count() == 0)
            {
                return null;
            }
            return await consulta.ToListAsync();
        }

        public async Task<int> GetMaximoPedido()
        {
           if(this.context.Pedidos.Count() == 0)
            {
                return 1;
            }
            else
            {
                return this.context.Pedidos.Max(z => z.IdPedido) + 1;
            }
        }

        public async Task NewPedido(int idpedido, int idfactura, string fecha, int idlibro, int idusuario, int cantidad)
        {
            Pedido pedido = new Pedido();

            pedido.IdPedido = await this.GetMaximoPedido();
            pedido.IdFactura = idfactura;
            pedido.Fecha = fecha;
            pedido.IdLibro = idlibro;
            pedido.Cantidad = cantidad;
            pedido.IdUsuario = idusuario;
            this.context.Pedidos.Add(pedido);
            await this.context.SaveChangesAsync();
        }
       
    }
}
