using System.ComponentModel.DataAnnotations.Schema;

namespace QwiikTechnicalTest.Models.DB
{
    public class Appointment
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        [Column(TypeName = "varchar(20)")]
        public string Token { get; set; }
        public TimeOnly AppointmentTime { get; set; }
        public DateTime AppointmentDate { get; set; }
        [Column(TypeName = "enum('Scheduled','Completed','Cancelled')")]
        public AppointmentStatus Status { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public Customer Customer { get; set; }

    }

    public enum AppointmentStatus
    {
        Scheduled,
        Completed,
        Cancelled
    }
}
