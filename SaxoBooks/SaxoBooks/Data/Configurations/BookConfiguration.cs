using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using SaxoBooks.Models;

namespace SaxoBooks.Data.Configurations
{
    public class BookConfiguration : EntityTypeConfiguration<Book>
    {
        public BookConfiguration()
        {
            ToTable("Books");

            HasKey(x => x.Isbn);
            Property(x => x.Isbn).IsRequired();
            Property(x => x.Isbn).HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute()));
        }
    }
}