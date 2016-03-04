using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using SaxoBooks.Data.Configurations;
using SaxoBooks.Models;

namespace SaxoBooks.Data
{
    public class SaxoBooksContext : DbContext
    {
        public SaxoBooksContext() : base("SaxoBookDb")
        {
            
        }

        public DbSet<Book> Books { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new BookConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}