using Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class EcomDbContext : IdentityDbContext<ApplicationUser>
    {
        public EcomDbContext(DbContextOptions<EcomDbContext> options) : base(options)
        {
        }
        public DbSet<ApplicationUser> ApplicationUsers { set; get; }
        public DbSet<Product> Products { set; get; }
        public DbSet<Category> Categories { get; set; }

    }
}
