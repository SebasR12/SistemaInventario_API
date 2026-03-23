using Microsoft.EntityFrameworkCore;

namespace SistemaInventario_API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> option) : base(option)
        { }
    }
}
