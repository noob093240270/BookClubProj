using BookClubProj.Server.Data;
using BookClubProj.Server.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

    
        [HttpGet("books")]
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

            


            if (await _context.ReadBooks.AnyAsync(rb => rb.UserId == addedBook.UserId && rb.BookId == addedBook.BookId))
            {
                return BadRequest("Книга уже добавлена");
            }
            if (!int.TryParse(userId, out int parsedUserId))
            {
                return Unauthorized("Неверный идентификатор пользователя");
            }

            addedBook.UserId = parsedUserId;


            _context.ReadBooks.Add(
                new ReadBook
                {
                    UserId = addedBook.UserId,
                    BookId = addedBook.BookId,
                }
                );

            await _context.SaveChangesAsync();
            return Ok(new { message = "книга добавлена" });

        }

        /*
        [HttpGet("get-user-id")]
        public async Task<IActionResult> GetUserIdByUsername([FromQuery] string username)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Name == username);

            if (user == null)
            {
                return NotFound("Пользователь не найден");
            }

            return Ok(user.Id);
        }*/


        /*
        [HttpGet("user-books/{Id}")]
        public async Task<IActionResult> GetUserBooks(int id)
        {
            var user = await _context.Users.Include(u => u.ReadBooks).FirstOrDefaultAsync(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user.ReadBooks);
        }

       


        [HttpPost("delete")]
        public async Task<IActionResult> DeleteBookFromUser(int userId, int bookId)
        {
            var user = await _context.Users.Include(u => u.ReadBooks).FirstOrDefaultAsync(u => userId == u.Id);
            var book = user.ReadBooks.FirstOrDefault(u => user.Id == bookId);
            if (book == null || user == null)
            {
                return NotFound();
            }
            user.ReadBooks.Remove(book);

            await _context.SaveChangesAsync();
            return Ok();
        }*/
    }
}
