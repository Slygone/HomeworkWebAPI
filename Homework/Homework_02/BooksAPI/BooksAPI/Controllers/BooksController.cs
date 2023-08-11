using BooksAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BooksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        [HttpGet("allBooks")]
        public ActionResult <List<Book>> Get()
        {
            try
            {
                return Ok(StaticDb.Books);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error occurred");
            }
        }
        [HttpGet("id/{id}")]
        public ActionResult <Book> GetBookById(int id)
        {
            try
            {
                if(id < 0)
                {
                    return BadRequest("Id cannot be negative!");
                }
                Book bookDb = StaticDb.Books.FirstOrDefault(x => x.Id == id);
                if(bookDb == null)
                {
                    return NotFound($"The book with Id {id} was not found!");
                }
                return Ok(bookDb);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error occurred");
            }
        }
        [HttpGet("FilteredBooks")]
        public ActionResult <List<Book>> FilterBooks(string? author, string? title)
        {
            try
            {
                if (string.IsNullOrEmpty(author) && string.IsNullOrEmpty(title))
                {
                    return StaticDb.Books;
                }
                if (string.IsNullOrEmpty(title))
                {
                    List<Book> bookDb = StaticDb.Books.Where(x => x.Author.ToLower().Contains(author.ToLower())).ToList();
                    return bookDb;
                }
                if (string.IsNullOrEmpty(author))
                {
                    List<Book> bookDb = StaticDb.Books.Where(x => x.Title.ToLower().Contains(title.ToLower())).ToList();
                    return bookDb;
                }
                List<Book> filteredBooks = StaticDb.Books.Where(x => x.Author.ToLower().Contains(author.ToLower())
                    && x.Title.ToLower().Contains(title.ToLower())).ToList();
                return Ok(filteredBooks);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error occurred");
            }
        }
        [HttpPost]
        public IActionResult AddBook([FromBody] Book book)
        {
            try
            {
                if (string.IsNullOrEmpty(book.Author))
                {
                    return BadRequest("The book must have a Author!");
                }
                if (string.IsNullOrEmpty(book.Title))
                {
                    return BadRequest("The book must have a Title!");
                }

                StaticDb.Books.Add(book);
                return StatusCode(StatusCodes.Status201Created);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error occurred");
            }
        }
        [HttpPost("Titles")]
        public IActionResult ReturnTitlesOfBooks([FromBody] List<Book> books)
        {
            try
            {
                if (books == null || books.Count == 0)
                {
                    return BadRequest("No books were provided in the request!");
                }

                List<string> titles = new List<string>();

                foreach(Book book in books)
                {
                    if (string.IsNullOrEmpty(book.Title))
                    {
                        return BadRequest("Each book must have a title!");
                    }
                    titles.Add(book.Title);
                }

                return Ok(titles);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error occurred");
            }
        }
    }
}