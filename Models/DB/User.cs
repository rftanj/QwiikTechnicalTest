using System.ComponentModel.DataAnnotations.Schema;

namespace QwiikTechnicalTest.Models.DB
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Salt { get; set; }
        [Column(TypeName = "enum('Admin','Customer')")]
        public UserRole Role { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

    public enum UserRole
    {
        Admin,
        Customer
    }
}
