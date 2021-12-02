using BulkyBookWebCore.Models;
using Microsoft.EntityFrameworkCore;

namespace BulkyBookWebCore.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options):base(options)
        {
        }
        public DbSet<Category> Categories { get; set; }
    }
}
