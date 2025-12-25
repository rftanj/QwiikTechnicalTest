namespace QwiikTechnicalTest.Models.DTO.Appointment
{
    public class AppointmentRequestDTO
    {
        public string customer_name { get; set; }
        public string phone_number { get; set; }
        public string email { get; set; }
        public string appointment_time { get; set; }
        public DateTime appointment_date { get; set; }
    }
}
