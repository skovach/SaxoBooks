using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using SaxoBooks.Models;

namespace SaxoBooks.Data.Models
{
    public class ResponseRootObjectModel
    {
        public List<Book> Products { get; set; }
    }
}