using System;
using BookManagement.DataAccess.Model;
using BookManagement.DataAccess.Repository;
using Microsoft.AspNetCore.Mvc;

namespace BookManagement.WebApp.Controllers
{

    [Route("api/books")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookRepository bookRepository;

        public BookController(IBookRepository bookRepository)
        {
            this.bookRepository = bookRepository;
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            var result = bookRepository.GetAllBooks();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public ActionResult GetById(int id)
        {
            var result = bookRepository.GetBookById(id);
            if (result is null)
            {
                return NotFound();
            }

            return Ok(result);
        }


        [HttpPost]
        public ActionResult Create([FromBody] CreateBookRequestModel bookRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var book = new Book()
            {
                Title = bookRequest.Title,
                Author = bookRequest.Author,
                Genre = bookRequest.Genre,
                ISBN = bookRequest.ISBN,
                PublicationYear = bookRequest.PublicationYear
            };

            try
            {
                bookRepository.AddBook(book);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        [HttpPut("{id}")]
        public ActionResult Edit(int id, Book book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != book.Id)
            {
                return BadRequest("ID in URL does not match ID in body.");
            }

            var result = bookRepository.GetBookById(id);
            if (result is null)
            {
                return NotFound();
            }

            try
            {
                bookRepository.UpdateBook(book);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var book = bookRepository.GetBookById(id);
            if (book == null)
            {
                return NotFound();
            }
            bookRepository.DeleteBook(id);
            return NoContent();
        }
    }
}
