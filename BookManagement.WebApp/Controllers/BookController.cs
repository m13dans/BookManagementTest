using System.Collections.Generic;
using BookManagement.DataAccess.Model;
using BookManagement.DataAccess.Repository;
using Microsoft.AspNetCore.Mvc;

namespace BookManagement.WebApp.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookRepository bookRepository;

        public BookController(IBookRepository bookRepository)
        {
            this.bookRepository = bookRepository;
        }

        public ActionResult Index()
        {
            var result = bookRepository.GetAllBooks();
            return View(result);
        }
        public ActionResult DevExtreme()
        {
            var result = bookRepository.GetAllBooks();
            return View(result);
        }

        public ActionResult Details(int id)
        {
            var book = bookRepository.GetBookById(id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Book book)
        {
            if (!ModelState.IsValid)
            {
                return View(book);
            }

            bookRepository.AddBook(book);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var book = bookRepository.GetBookById(id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        [HttpPost]
        public ActionResult Edit(Book book)
        {
            if (ModelState.IsValid)
            {
                bookRepository.UpdateBook(book);
                return RedirectToAction("Index");
            }
            return View(book);
        }

        public ActionResult Delete(int id)
        {
            var book = bookRepository.GetBookById(id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            bookRepository.DeleteBook(id);
            return RedirectToAction("Index");
        }
    }
}
