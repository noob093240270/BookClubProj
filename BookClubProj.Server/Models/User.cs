using System.ComponentModel.DataAnnotations.Schema;

namespace BookClubProj.Server.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public byte[] PasswordHash { get; set; }
        [InverseProperty(nameof(ReadBook.User))]
        public virtual ICollection<ReadBook> ReadBooks { get; set; }
        
    }
}
