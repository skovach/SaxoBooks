using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SaxoBooks.Data.Models;
using SaxoBooks.Infrastructure;
using SaxoBooks.Services.Interfaces;

namespace SaxoBooks.Services
{
    public class SaxoBooksService : ISaxoBooksService
    {
        private readonly string startRequest = "?";
        private readonly string addParam = "&isbn=";

        private readonly IConfigurationReader _configReader;
        public SaxoBooksService(IConfigurationReader configurationReader)
        {
            _configReader = configurationReader;
        }
        
        public async Task<IEnumerable<Book>> GetBooksFromService(IEnumerable<string> isbnsToRequest)
        {
            if (!isbnsToRequest.Any()) return new List<Book>();
            using (HttpClient httpClient = new HttpClient())
            {
                var query = BuildQueryString(isbnsToRequest.ToList());
                var result = httpClient.GetStringAsync(query).Result;

                var settings = new JsonSerializerSettings();
                settings.DateFormatString = _configReader.DateTimeFormat;
                settings.ContractResolver = new SaxoBooksContractResolver();
                return JsonConvert.DeserializeObject<ApiResponseRootObjectModel>(result, settings).Products;
            }
        }

        private string BuildQueryString(List<string> isbnsToRequest)
        {
            var builder = new StringBuilder();
            builder.Append(_configReader.ApiUrl);
            builder.Append(startRequest);
            builder.Append(_configReader.ApiKey);
            builder.Append(addParam);
            builder.Append(string.Join(addParam, isbnsToRequest));
            return builder.ToString();
        }
    }
}