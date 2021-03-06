using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using WebAutopark.Core.Entities;
using WebAutopark.Core.Entities.Base;
using WebAutopark.Core.Entities.Identity;

namespace WebAutopark.DataAccess
{
    public class WebAutoparkContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public WebAutoparkContext(DbContextOptions<WebAutoparkContext> options)
            : base(options)
        {
            
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<VehicleType> VehicleTypes { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
        
        public override DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ShoppingCartItem>(b =>
            {
                b.HasOne("WebAutopark.Core.Entities.Order", null)
                    .WithMany("CartItems")
                    .HasForeignKey("OrderId")
                    .OnDelete(DeleteBehavior.Cascade);
            });
            
            base.OnModelCreating(builder);
        }
    }
}