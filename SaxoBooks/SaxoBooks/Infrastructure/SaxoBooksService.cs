using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using SaxoBooks.Data.Models;
using SaxoBooks.Models;

namespace SaxoBooks.Infrastructure
{
    public class SaxoBooksService : ISaxoBooksService
    {
        private readonly string apiKey = "key=08964e27966e4ca99eb0029ac4c4c217";
        private readonly string apiUrl = "http://api.saxo.com/v1/products/products.json";
        private readonly string startRequest = "?";
        private readonly string addParam = "&isbn=";

        public async Task<IEnumerable<Book>> GetBooksFromService(IEnumerable<string> isbnsToRequest)
        {
            if (!isbnsToRequest.Any()) return new List<Book>();
            using (HttpClient httpClient = new HttpClient())
            {
                var query = BuildQueryString(isbnsToRequest.ToList());
                var result = httpClient.GetStringAsync(query).Result;
                var settings = new JsonSerializerSettings();
                settings.DateFormatString = "MM-DD-YYYY";
                settings.ContractResolver = new SaxoBooksContractResolver();
                return JsonConvert.DeserializeObject<ResponseRootObjectModel>(result, settings).Products;
            }
        }

        private string BuildQueryString(List<string> isbnsToRequest)
        {
            var builder = new StringBuilder();
            builder.Append(apiUrl);
            builder.Append(startRequest);
            builder.Append(apiKey);
            builder.Append(addParam);
            builder.Append(string.Join(addParam, isbnsToRequest));
            return builder.ToString();
        }
    }
}