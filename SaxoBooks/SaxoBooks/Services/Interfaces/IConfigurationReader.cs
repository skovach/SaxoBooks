namespace SaxoBooks.Services.Interfaces
{
    public interface IConfigurationReader
    {
        string ApiKey { get; }
        string ApiUrl { get; }
        string DateTimeFormat { get; }
        int BooksPerRequest { get; }
        string GetApiKey();
        string GetApiUrl();
        string GetDateTimeFormat();
        int GetBooksPerRequest();
    }
}
