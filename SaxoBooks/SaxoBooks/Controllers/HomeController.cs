using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using SaxoBooks.Data.Repository;
using SaxoBooks.Infrastructure;
using SaxoBooks.Models;
using WebGrease.Css.Extensions;

namespace SaxoBooks.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRepository<Book> _booksRepository;
        private readonly ISaxoBooksService _saxoService;

        public HomeController(IRepository<Book> repository, ISaxoBooksService saxoService)
        {
            _booksRepository = repository;
            _saxoService = saxoService;

        }

        public ActionResult Index()
        {
            return View();
        }


        public PartialViewResult GetBooks(string isbnNumbers)
        {
            var isbns = GetListOfIsbns(isbnNumbers);
            var dbBooks = GetBooksFromDb(isbns);

            var isbnsToRequest = isbns.Except(dbBooks.Select(x => x.Isbn)).ToList();

            var booksFromService = _saxoService.GetBooksFromService(isbnsToRequest).Result;

            booksFromService.ForEach(_booksRepository.AddOrUpdate);
            _booksRepository.SaveChanges();

            return PartialView("_GetBooks", booksFromService);
        }

        private List<Book> GetBooksFromDb(List<string> isbns)
        {
            var result = _booksRepository.Query().Where(x => isbns.Contains(x.Isbn)).ToList();
            return result;
        }

        private List<string> GetListOfIsbns(string isbnNumbers)
        {
            return isbnNumbers.Replace("\r", "").Split('\n').ToList();
        }
    }
}