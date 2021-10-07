using Microsoft.EntityFrameworkCore;
using ShopsRUs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopsRUs.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserType> UserTypes { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Invoice> Invoices {  get;}
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<Item> Items { get; set; }

    }
}
