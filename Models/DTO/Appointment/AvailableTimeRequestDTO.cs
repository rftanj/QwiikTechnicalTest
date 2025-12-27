namespace QwiikTechnicalTest.Models.DTO.Appointment
{
    /// <summary>
    /// Request payload for retrieving available appointment time slots
    /// </summary>
    public class AvailableTimeRequestDTO
    {
        /// <summary>
        /// The date for which available appointment slots are requested
        /// </summary>
        /// <example>2025-01-10</example>
        public string appointment_date { get; set; }
    }
}
