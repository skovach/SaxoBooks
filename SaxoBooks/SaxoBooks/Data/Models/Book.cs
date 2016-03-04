using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace SaxoBooks.Models
{
    public class Book
    {
        public string Isbn { get; set; }
        public byte[] Image { get; set; }
    }
}