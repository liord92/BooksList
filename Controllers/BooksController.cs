using BooksList.Interfaces;
using BooksList.Models;
using BooksList.Services;
using Microsoft.AspNetCore.Mvc;

namespace BooksList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet("GetBooks")]
        public async Task<IActionResult> GetBooksAsync()
        {
            try
            {
                var books = await _bookService.GetBooksAsync();
                return Ok(books);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("AddBook")]
        public async Task<IActionResult> AddBookAsync([FromBody] Book book)
        {
            try
            {
                if (book == null)
                {
                    return BadRequest("Book is null.");
                }

                await _bookService.AddBookAsync(book);
                return Ok("Book added successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("UpdateBook/{isbn}")]
        public async Task<IActionResult> UpdateBookAsync(string isbn, [FromBody] Book updatedBook)
        {
            try
            {
                await _bookService.UpdateBookAsync(isbn, updatedBook);
                return Ok("Book updated successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("DeleteBook/{isbn}")]
        public async Task<IActionResult> DeleteBookAsync(string isbn)
        {
            try
            {
                await _bookService.DeleteBookAsync(isbn);
                return Ok("Book deleted successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
