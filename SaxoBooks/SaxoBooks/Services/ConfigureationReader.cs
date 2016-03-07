using System.Configuration;
using SaxoBooks.Services.Interfaces;

namespace SaxoBooks.Services
{
    public class ConfigurationReader : IConfigurationReader
    {
        public ConfigurationReader()
        {
            ApiKey = GetApiKey();
            ApiUrl = GetApiUrl();
            DateTimeFormat = GetDateTimeFormat();
            BooksPerRequest = GetBooksPerRequest();
        }

        public string ApiKey { get; private set; }
        public string ApiUrl { get; private set; }
        public string DateTimeFormat { get; private set; }
        public int BooksPerRequest { get; private set; }

        public string GetApiKey()
        {
            return ConfigurationManager.AppSettings.Get("ApiKey");
        }
        public string GetApiUrl()
        {
            return ConfigurationManager.AppSettings.Get("ApiUrl");
        }
        public string GetDateTimeFormat()
        {
            return ConfigurationManager.AppSettings.Get("DateTineFormat");
        }

        public int GetBooksPerRequest()
        {
           return int.Parse(ConfigurationManager.AppSettings.Get("BooksPerRequest"));
        }
    }
}