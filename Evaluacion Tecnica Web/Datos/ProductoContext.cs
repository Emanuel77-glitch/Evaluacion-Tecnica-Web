using Microsoft.EntityFrameworkCore;

namespace Evaluacion_Tecnica_Web.Datos
{
    public class ProductoContext : DbContext
    {
        public ProductoContext(DbContextOptions<ProductoContext> options) : base(options)
        {
        }

       public DbSet<Evaluacion_Tecnica_Web.Models.Producto> Productos { get; set; }
    }
}
