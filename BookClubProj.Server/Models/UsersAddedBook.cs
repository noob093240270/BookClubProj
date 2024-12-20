using System.ComponentModel.DataAnnotations;

namespace BookClubProj.Server.Models
{
    public class UsersAddedBook
    {
        [Key]
        public int Id { get; set; }

        public int BookId { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }

    }
}
