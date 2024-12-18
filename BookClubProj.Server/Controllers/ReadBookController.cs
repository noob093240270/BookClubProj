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


        

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserBooks(int userId)
        {
            var books = await _context.ReadBooks.Where(rb => rb.UserId == userId).ToListAsync();
            return Ok(books);
        }


    }
}
