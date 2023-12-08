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
    }
}
