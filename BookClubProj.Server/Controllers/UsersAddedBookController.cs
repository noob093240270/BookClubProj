using BookClubProj.Server.Data;
using BookClubProj.Server.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookClubProj.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersAddedBookController : ControllerBase
    {
        private readonly BookClubContext _context;

        public UsersAddedBookController(BookClubContext context)
        {
            _context = context;
        }


        [HttpGet("get_users_added_book")]
        [Authorize]
        public async Task<ActionResult> GetUsersAddedBooks()
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "id");
            if (userIdClaim == null)
            {
                return Unauthorized("Не удалось получить идентификатор пользователя");
            }
            var userId = int.Parse(userIdClaim.Value);
            
            var useraddedbooks = await _context.UsersAddedBooks.Where(x => x.UserId == userId).ToListAsync();
            return Ok(useraddedbooks);

        }


        [HttpPost("add_users_book")]
        [Authorize]
        public async Task<ActionResult> AddUsersBook([FromBody] Book book)
        {
            try
            {
                
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (book == null)
                {
                    return BadRequest("Неверные данные");
                }


                var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "id");
                if (userIdClaim == null)
                {
                    return Unauthorized("Не удалось получить идентификатор пользователя");
                }
                var userId = int.Parse(userIdClaim.Value);



                var existingBook = await _context.Books.FirstOrDefaultAsync(b => b.Title == book.Title && b.Author == book.Author);
                if (existingBook == null)
                {
                    existingBook = new Book
                    {
                        Title = book.Title,
                        Author = book.Author
                    };

                    _context.Books.Add(existingBook);
                    await _context.SaveChangesAsync();

                }

                var userAddedBook = new UsersAddedBook
                {
                    UserId = userId,
                    BookId = existingBook.Id
                };

                _context.UsersAddedBooks.Add(userAddedBook);
                await _context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при добавлении книги: {ex.Message}");
                return StatusCode(500, $"Произошла ошибка: {ex.Message}");
            }


        }

        
        [HttpPost("delete")]
        [Authorize]
        public async Task<IActionResult> DeleteUsersAddedBook([FromBody] int bookId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "id");
            if (userIdClaim == null)
            {
                return Unauthorized("Не удалось получить идентификатор пользователя");
            }
            var userId = int.Parse(userIdClaim.Value);

            var deletebook = await _context.UsersAddedBooks.FirstOrDefaultAsync(c => c.Id == bookId && c.UserId == userId);
            if (deletebook == null)
            {
                return NotFound("Книга не найдена или не принадлежит пользователю.");
            }

            var book = await _context.Books.FirstOrDefaultAsync(c => c.Id == deletebook.BookId);
            if (book == null)
            {
                return NotFound();
            }

            if (book.CountAddings > 0)
            {
                ModelState.AddModelError("countaddings", "эта книга добавлена в коллекцию у других пользователей, поэтому книгу нельзя удалить из общей библиотеки");
                
                return BadRequest(ModelState);
            }
            _context.UsersAddedBooks.Remove(deletebook);
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

            return Ok("Книга успешно удалена.");


        }

    
    }
}
