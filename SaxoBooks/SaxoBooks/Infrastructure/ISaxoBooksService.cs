using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SaxoBooks.Models;

namespace SaxoBooks.Infrastructure
{
    public interface ISaxoBooksService
    {
        Task<IEnumerable<Book>> GetBooksFromService(IEnumerable<string> isbnsToRequest);
    }
}
