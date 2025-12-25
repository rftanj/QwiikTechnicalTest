namespace QwiikTechnicalTest.Models.DTO.Appointment
{
    public class AppointmentResponseDTO
    {
        public int Id { get; set; }
        public string customer_name { get; set; }
        public string appointment_time { get; set; }
        public string appointment_date { get; set; }
        public string token { get; set; }
        public string status { get; set; }
        public string CreatedAt { get; set; }
    }
}
