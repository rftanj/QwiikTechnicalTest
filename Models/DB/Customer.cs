using System.ComponentModel.DataAnnotations.Schema;

namespace QwiikTechnicalTest.Models.DB
{
    public class Customer
    {
        public int Id { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string Name { get; set; }
        [Column(TypeName = "varchar(20)")]
        public string PhoneNumber { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string Email { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public List<Appointment> Appointments { get; set; }
    }
}
