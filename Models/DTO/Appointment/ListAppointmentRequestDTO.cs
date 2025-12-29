namespace QwiikTechnicalTest.Models.DTO.Appointment
{
    /// <summary>
    /// Request filter for listing appointments.
    /// </summary>
    public class ListAppointmentRequestDTO
    {
        /// <summary>
        /// Appointment date in yyyy-MM-dd format.
        /// Optional.
        /// </summary>
        /// <example>2025-01-01</example>
        public string? appointment_date { get; set; }

        /// <summary>
        /// Customer name to search.
        /// Optional.
        /// </summary>
        /// <example>John Doe</example>
        public string? customer_name { get; set; }

        /// <summary>
        /// Appointment status.
        /// Optional.
        /// </summary>
        /// <example>Scheduled</example>
        public string? status { get; set; }
    }

}
