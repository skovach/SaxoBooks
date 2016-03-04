using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using SaxoBooks.Data;
using SaxoBooks.Data.Repository;
using SaxoBooks.Models;

namespace SaxoBooks.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRepository<Book> _booksRepository;
        public HomeController(IRepository<Book> repository)
        {
            _booksRepository = repository;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public PartialViewResult GetBooks(string isbnNumbers = "")
        {
            var isbns = isbnNumbers.Split('\n');
            var dbBooks = GetBooksFromDb(isbns);

            var isbnsToRequest = isbns.Except(dbBooks.Select(x => x.Isbn));

            var booksFromService = GetBooksFromService(isbnsToRequest);


            Random rand = new Random();
            var books = new List<Book>();
            for (int i = 0; i < 13; i++)
            {
                books.Add(new Book()
                {
                    Isbn = rand.Next().ToString()
                });
            }
            return PartialView("_GetBooks", books);
        }

        private async Task<IEnumerable<Book>> GetBooksFromService(IEnumerable<string> isbnsToRequest)
        {
            string uri = " http://api.saxo.com/v1";

            using (HttpClient httpClient = new HttpClient())
            {
                var result = await httpClient.GetStringAsync(uri);
                return JsonConvert.DeserializeObject<List<Book>>(result);
            }
        }


        private List<Book> GetBooksFromDb(string[] isbns)
        {
            //var result = _booksRepository.Query().Where(x => isbns.Contains(x.Isbn)).ToList();
            //return result;
            return new List<Book>();
        }
    }
}