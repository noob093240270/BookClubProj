using System.ComponentModel.DataAnnotations;

namespace BookClubProj.Server.Models
{
    public class RegisterUser
    {
        [Required(ErrorMessage = "Имя пользователя обязательно.")]
        [StringLength(15, MinimumLength = 3, ErrorMessage = "Логин должен быть от 3 до 15 символов.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Пароль обязателен.")]
        [StringLength(15, MinimumLength = 6, ErrorMessage = "Пароль должен быть от 6 до 15 символов.")]
        public string Password { get; set; }
    }

    public class RegisterResponse
    {
        public string Token { get; set; }
        public string Message { get; set; }
    }

}
