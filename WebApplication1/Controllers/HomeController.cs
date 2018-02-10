using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        List<Book> allbooks = new List<Book>();
        private static NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();

        public HomeController()
        {
            allbooks.Add(new Book { Author = "Aut1", Id = 1, Name = "N`1", Price = 11 });
            allbooks.Add(new Book { Author = "Aut2", Id = 2, Name = "N`2", Price = 21 });
            allbooks.Add(new Book { Author = "Aut3", Id = 3, Name = "N`3", Price = 31 });
        }

        public ActionResult Index()
        {
            _logger.Debug("test");

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        public ActionResult BookSearch(string name)
        {
            var books = allbooks.Where(a => a.Author.Contains(name)).ToList();

            if (books.Count <= 0)
            {
                return HttpNotFound();
            }

            return PartialView(books);
        }
    }
}