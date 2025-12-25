using System.ComponentModel.DataAnnotations.Schema;

namespace QwiikTechnicalTest.Models.DTO.Customer
{
    public class CustomerResponseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string CreatedAt { get; set; }
        public string UpdatedAt { get; set; }
    }
}
