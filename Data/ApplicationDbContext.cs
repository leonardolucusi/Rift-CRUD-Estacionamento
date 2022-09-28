using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Rift.Models;

namespace Rift.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Rift.Models.Person> Person { get; set; }
        public DbSet<Rift.Models.Business> Business { get; set; }
    }
}