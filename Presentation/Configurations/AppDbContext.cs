using eMix.ConsultaCEP.Models;
using Microsoft.EntityFrameworkCore;

namespace eMix.ConsultaCEP.Configurations
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Address> Addresses { get; set; }
    }
}
