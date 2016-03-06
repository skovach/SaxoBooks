using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration.Conventions;
using Newtonsoft.Json;

namespace SaxoBooks.Models
{
    public class Book
    {
        [JsonProperty("isbn13")]
        public string Isbn { get; set; }
        [JsonProperty("imageurl")]
        public string ImageUrl { get; set; }
    }
}