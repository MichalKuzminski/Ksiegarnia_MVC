using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Ksiegarnia_MVC.Models;

namespace Ksiegarnia_MVC.Data
{
    public class Ksiegarnia_MVCContext : DbContext
    {
        public Ksiegarnia_MVCContext (DbContextOptions<Ksiegarnia_MVCContext> options)
            : base(options)
        {
        }

        public DbSet<Ksiegarnia_MVC.Models.Book> Book { get; set; } = default!;
        public DbSet<Ksiegarnia_MVC.Models.Customer> Customer { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Book>()
                .HasOne(b => b.Customer)
                .WithMany(c => c.Books)
                .HasForeignKey(b => b.CustomerId)
                .OnDelete(DeleteBehavior.SetNull);
        }


    }
}
