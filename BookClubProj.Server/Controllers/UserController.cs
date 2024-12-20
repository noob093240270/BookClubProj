using BookClubProj.Server.Data;
using BookClubProj.Server.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace BookClubProj.Server.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly BookClubContext _context;

        public UserController(BookClubContext context)
        {
            _context = context;
        }


        [HttpGet("name")]
        public ActionResult GetUsername()
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "id");
            if (userIdClaim == null)
            {
                return Unauthorized("Не удалось получить идентификатор пользователя");
            }
            var userId = int.Parse(userIdClaim.Value);

            var user = _context.Users.FirstOrDefault(u =>  u.Id == userId);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(new { user.Name } );

        }


    }
}
