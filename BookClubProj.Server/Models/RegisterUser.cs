namespace BookClubProj.Server.Models
{
    public class RegisterUser
    {
        public string Name { get; set; }
        public string Password { get; set; }
    }

    public class RegisterResponse
    {
        public string Token { get; set; }
        public string Message { get; set; }
    }

}
