using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using SaxoBooks.Data.Repository;
using SaxoBooks.Infrastructure;
using SaxoBooks.Models;
using SaxoBooks.ViewModels;
using WebGrease.Css.Extensions;

namespace SaxoBooks.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRepository<Book> _booksRepository;
        private readonly ISaxoBooksService _saxoService;

        public int BooksPerRequest { get; } = 5;

        public HomeController(IRepository<Book> repository, ISaxoBooksService saxoService)
        {
            _booksRepository = repository;
            _saxoService = saxoService;

        }

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult GetBooks(string isbnNumbers, int blockNumber = 0)
        {
            var isbnsToShow = GetIsbnNumbersFromCurrentBlock(blockNumber, isbnNumbers);
            if (isbnsToShow == null)
            {
                return Json(new BooksPartialJsonModel()
                {
                    HasBooks = true,
                    HtmlString = ""
                });
            }

            var dbBooks = GetBooksFromDb(isbnsToShow);

            var isbnsToRequest = isbnsToShow.Except(dbBooks.Select(x => x.Isbn)).ToList();
            var booksFromService = _saxoService.GetBooksFromService(isbnsToRequest).Result.ToList();
            SaveNewBooksToDb(booksFromService);

            var result = dbBooks.Concat(booksFromService);
            var jsonResult = BuildJsonViewModel(result);
            return Json(jsonResult);
        }

        private void SaveNewBooksToDb(IEnumerable<Book> books)
        {
            books.ForEach(_booksRepository.AddOrUpdate);
            _booksRepository.SaveChanges();
        }


        private BooksPartialJsonModel BuildJsonViewModel(IEnumerable<Book> books)
        {
            var booksModel = new BooksPartialJsonModel
            {
                HasBooks = books.Count() >= BooksPerRequest,
                HtmlString = RenderPartialViewToString("_GetBooks", books)
            };
            return booksModel;
        }

        private List<string> GetIsbnNumbersFromCurrentBlock(int blockNumber, string isbnNumbers)
        {
            if (!RequestDataIsValid(blockNumber, isbnNumbers)) return null;
            int startIndex = (blockNumber - 1) * BooksPerRequest;
            var isbns = GetListOfIsbns(isbnNumbers)
                .Skip(startIndex)
                .Take(BooksPerRequest).ToList();
            return isbns;
        }

        private bool RequestDataIsValid(int blockNumber, string isbnNumbers)
        {
            Regex digitsOnly = new Regex(@"^\d$");
            return blockNumber == 0 || string.IsNullOrEmpty(isbnNumbers) || !digitsOnly.IsMatch(isbnNumbers);
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

        protected string RenderPartialViewToString(string viewName, object model)
        {
            if (string.IsNullOrEmpty(viewName))
                viewName = ControllerContext.RouteData.GetRequiredString("action");

            ViewData.Model = model;

            using (StringWriter sw = new StringWriter())
            {
                ViewEngineResult viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
                ViewContext viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);

                return sw.GetStringBuilder().ToString();
            }
        }
    }
}