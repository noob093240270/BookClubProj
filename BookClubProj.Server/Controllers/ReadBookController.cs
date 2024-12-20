using System.Linq;
using BookClubProj.Server.Data;
using BookClubProj.Server.Models;
using BookClubProj.Server.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookClubProj.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReadBookController : ControllerBase
    {
        private readonly BookClubContext _context;

        public ReadBookController(BookClubContext context)
        {
            _context = context;
        }


        

        [HttpGet("userpage/userbooks")]
        public async Task<ActionResult> GetUserBooks()
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "id");
            if (userIdClaim == null)
            {
                return Unauthorized("Не удалось получить идентификатор пользователя");
            }
            var userId = int.Parse(userIdClaim.Value);

            var readbooks = await _context.ReadBooks.Where(rb => rb.UserId == userId).ToListAsync();
            var booksid = readbooks.Select(b => b.BookId).ToList();
            var books = await _context.Books.Where(b => booksid.Contains(b.Id)).ToListAsync();
            

            
            return Ok(books);
        }

        [HttpPost("delete")]
        public async Task<IActionResult> DeleteUserBook([FromBody] AddedBook addedBook)
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "id");
            if (userIdClaim == null)
            {
                return Unauthorized("Не удалось получить идентификатор пользователя");
            }
            var userId = int.Parse(userIdClaim.Value);

            var book = await _context.ReadBooks.FirstOrDefaultAsync(b => b.UserId == userId && b.BookId == addedBook.BookId);
            if (book == null)
            {
                return NotFound("Книга не найдена или не принадлежит пользователю");
            }

            _context.ReadBooks.Remove(book);
            await _context.SaveChangesAsync();
            return Ok();


        }


    }
}
