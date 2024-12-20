using BookClubProj.Server.Data;
using BookClubProj.Server.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BookClubProj.Server.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        private readonly BookClubContext _context;

        public BookController(BookClubContext context)
        {
            _context = context;
        }

    
        [HttpGet("library/books")]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
        {
            var books = await _context.Books.ToListAsync();
            if (books == null || !books.Any())
            {
                return NotFound("Books not found");
            }
            await _context.SaveChangesAsync();
            return books;
            
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddBookReadList([FromBody] AddedBook addedBook)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (addedBook == null || addedBook.BookId <= 0)
            {
                return BadRequest("Неверные данные");
            }

    
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "id");
            if (userIdClaim == null)
            {
                
                return Unauthorized("Не удалось получить идентификатор пользователя");
            }
            var userId = userIdClaim.Value;
            if (!int.TryParse(userId, out int parsedUserId))
            {
                return Unauthorized("Неверный идентификатор пользователя");
            }

            addedBook.UserId = parsedUserId;

            var existingBook = await _context.ReadBooks.FirstOrDefaultAsync(b => b.UserId == parsedUserId && b.BookId == addedBook.BookId);

            if (existingBook != null)
            {
                ModelState.AddModelError("bookadded", "Эта книга уже добавлена в ваш список");
                return BadRequest(ModelState);
            }


            _context.ReadBooks.Add(
                new ReadBook
                {
                    UserId = addedBook.UserId,
                    BookId = addedBook.BookId,
                }
                );

            
            var b = _context.Books.Where(b => b.Id == addedBook.BookId).First();
            b.CountAddings += 1;

            await _context.SaveChangesAsync();
            return Ok(new { message = "книга добавлена" });

        }
    }
}
