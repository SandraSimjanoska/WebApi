using BooksApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BooksApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<Book>> GetAllBooks()
        {
            try
            {
                return Ok(StaticDb.Books);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occured! Contact the admin {e.Message}");
            }
        }

        [HttpGet("queryString")]
        public ActionResult<Book> GetIndexByQueryString(int index)
        {
            try
            {
                if (index < 0)
                {
                    return BadRequest("The index cannot be negative");
                }

                if (index >= StaticDb.Books.Count)
                {
                    return NotFound($"There is no resourse on index {index}");
                }
                return Ok(StaticDb.Books[index]);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occured! Contact the admin {e.Message}");
            }
        }

        [HttpGet("filter")]
        public ActionResult<List<Book>> GetBookByFilter(string title, string author)
        {
            try
            {

                var book = StaticDb.Books.FirstOrDefault(x => x.Author.ToLower() == author.ToLower() && x.Title.ToLower() == title.ToLower());

                if (book == null)
                {
                    return NotFound($"No book found with author '{author}' and title '{title}'.");
                }

                if (string.IsNullOrEmpty(title) && string.IsNullOrEmpty(author))
                {
                    return BadRequest("Filter parameters are required!");
                }
                return Ok(book);

            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occured! Contact the admin {e.Message}");
            }
        }

        [HttpPost]
        public IActionResult PostBook([FromBody] Book book)
        {
            try
            {
                if (string.IsNullOrEmpty(book.Author) && string.IsNullOrEmpty(book.Title))
                {
                    return BadRequest("Author and Title required");
                }
                StaticDb.Books.Add(book);
                return StatusCode(StatusCodes.Status201Created, "New Book created");
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occured! Contact the admin {e.Message}");
            }
        }

        [HttpPost("bonus")]
        public ActionResult<List<string>> PostBooks([FromBody] List<Book> books)
        {
            try
            {
                if (books == null || books.Count == 0)
                {
                    return BadRequest("You must provide at least one book.");
                }

                var titles = books
                    .Where(x => !string.IsNullOrEmpty(x.Title)) 
                    .Select(y => y.Title.Trim())
                    .ToList();

                if (titles.Count == 0)
                {
                    return BadRequest("All provided books have empty or invalid titles.");
                }

                return Ok(titles);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occured! Contact the admin {e.Message}");
            }
        }
    }
}
