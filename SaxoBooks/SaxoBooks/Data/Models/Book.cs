using Newtonsoft.Json;

namespace SaxoBooks.Data.Models
{
    public class Book
    {
        [JsonProperty("isbn13")]
        public string Isbn { get; set; }
        [JsonProperty("imageurl")]
        public string ImageUrl { get; set; }
    }
}