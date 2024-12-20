using BookClubProj.Server.Data;
using BookClubProj.Server.Models;
using BookClubProj.Server.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookClubProj.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;
        private readonly BookClubContext _context;

        public AuthController(AuthService authService, BookClubContext context)
        {
            _authService = authService;
            _context = context;
        }


        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] RegisterUser user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Name == user.Name);

            if (existingUser != null)
            {
                ModelState.AddModelError("name", "пользователь с таким именем уже существует");
                return BadRequest(ModelState);
            }
            try
            {
                var token = await _authService.RegisterAsync(user.Name, user.Password);
                return Ok(
                    
                    new RegisterResponse
                    {
                        Token = token,
                        Message = "регистрация прошла успешно"
             
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Внутренняя ошибка сервера", Details = ex.Message });
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] RegisterUser _user)
        {
            if (!ModelState.IsValid)
                { return BadRequest(ModelState); }
            try
            {
                var isValidPassword = await _authService.ValidateUserPassword(_user.Name, _user.Password);
                if (!isValidPassword)
                {
                    ModelState.AddModelError("password_or_name", "Неправильный логин или пароль");
                    return BadRequest(ModelState);
                }

                var token = await _authService.LoginAsync(_user.Name, _user.Password);
                return Ok(new { Token = token, Message = "Вход прошла успешно" });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }




    }
}
