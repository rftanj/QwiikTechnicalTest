namespace QwiikTechnicalTest.Models.DTO.Appointment
{
    /// <summary>
    /// Request data for creating a new appointment.
    /// </summary>
    public class AppointmentRequestDTO
    {
        /// <summary>
        /// Customer full name.
        /// </summary>
        /// <example>John Doe</example>
        public string customer_name { get; set; }

        /// <summary>
        /// Customer phone number.
        /// Must start with 62 or 08.
        /// </summary>
        /// <example>081234567890</example>
        public string phone_number { get; set; }

        /// <summary>
        /// Customer email address.
        /// </summary>
        /// <example>john.doe@gmail.com</example>
        public string email { get; set; }

        /// <summary>
        /// Appointment time in HH:mm format.
        /// </summary>
        /// <example>10:00</example>
        public string appointment_time { get; set; }

        /// <summary>
        /// Appointment date.
        /// </summary>
        /// <example>2025-01-01</example>
        public DateTime appointment_date { get; set; }
    }
}
