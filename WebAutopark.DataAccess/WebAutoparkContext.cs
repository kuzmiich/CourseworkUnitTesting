using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebAutopark.Core.Entities;
using WebAutopark.Core.Entities.Base;
using WebAutopark.Core.Entities.Identity;

namespace WebAutopark.DataAccess
{
    public class WebAutoparkContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public WebAutoparkContext()
        {
            
        }
        public WebAutoparkContext(DbContextOptions<WebAutoparkContext> options)
            : base(options)
        {
            
        }

        public DbSet<Detail> Details { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<VehicleType> VehicleTypes { get; set; }
        
        public override DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (!options.IsConfigured)
            {
                options.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Initial Catalog=UnitTesting;Integrated Security=True;Connect Timeout=30;Encrypt=False;");
            }
        }
    }
}