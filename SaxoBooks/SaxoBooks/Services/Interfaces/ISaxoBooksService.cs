using System.Collections.Generic;
using System.Threading.Tasks;
using SaxoBooks.Data.Models;

namespace SaxoBooks.Services.Interfaces
{
    public interface ISaxoBooksService
    {
        Task<IEnumerable<Book>> GetBooksFromService(IEnumerable<string> isbnsToRequest);
    }
}
