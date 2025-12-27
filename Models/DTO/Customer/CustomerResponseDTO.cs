using System.ComponentModel.DataAnnotations.Schema;

namespace QwiikTechnicalTest.Models.DTO.Customer
{
    public class CustomerResponseDTO
    {
        public int id { get; set; }
        public string name { get; set; }
        public string phone_number { get; set; }
        public string email { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
    }
}
