using System.ComponentModel.DataAnnotations;

namespace JobSearch.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string UserName { get; set; } = "";
        [EmailAddress]
        public string Email { get; set; } = "";
        public string HashedPassword { get; set; } = "";
        public byte[]? SaltPassword { get; set; } 
    }
}
