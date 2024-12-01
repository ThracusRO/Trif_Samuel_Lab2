using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Trif_Samuel_Lab2.Models;

namespace Trif_Samuel_Lab2.Data
{
    public class Trif_Samuel_Lab2Context : DbContext
    {
        public Trif_Samuel_Lab2Context (DbContextOptions<Trif_Samuel_Lab2Context> options)
            : base(options)
        {
        }

        public DbSet<Trif_Samuel_Lab2.Models.Book> Book { get; set; } = default!;
        public DbSet<Trif_Samuel_Lab2.Models.Publisher> Publisher { get; set; } = default!;
        public DbSet<Trif_Samuel_Lab2.Models.Author> Author { get; set; } = default!;
        public DbSet<Trif_Samuel_Lab2.Models.Category> Category { get; set; } = default!;
    }
}
