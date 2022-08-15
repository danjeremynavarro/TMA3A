using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TMA3A.Models;

namespace TMA3A.Data
{
    public class TMA3AContext : IdentityDbContext<User, IdentityRole, string>
    {
        public TMA3AContext (DbContextOptions<TMA3AContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder
            .Entity<TMA3A.Models.Order>().Property(e => e.Status)
            .HasConversion(
            v => v.ToString(),
            v => (TMA3A.Models.Order.OrderStatus)Enum.Parse(typeof(TMA3A.Models.Order.OrderStatus), v));

        }
    

        public DbSet<TMA3A.Models.User> User { get; set; } = default!;

        public DbSet<TMA3A.Models.Product> Product { get; set; } = default!;

        public DbSet<TMA3A.Models.Order> Order { get; set; } = default!;

        public DbSet<TMA3A.Models.OrderLine> OrderLine { get; set; } = default!;

        public DbSet<TMA3A.Models.Image> Image { get; set; } = default!;

        public DbSet<TMA3A.Models.ItemCategory> ItemCategory { get; set; } = default!;
    }
}
