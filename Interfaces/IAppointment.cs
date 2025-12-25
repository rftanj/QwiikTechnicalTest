using QwiikTechnicalTest.Models.DTO.Appointment;

namespace QwiikTechnicalTest.Interfaces
{
    public interface IAppointment
    {
        Task<(string message, bool isSuccess)> CreateAppointment(AppointmentRequestDTO appointment);
        Task<List<AppointmentResponseDTO>> GetListDataAppointments(ListAppointmentRequestDTO dto);
        Task<(List<AvailableTimeResponseDTO> datas, string message)> GetAvailableTime(AvailableTimeRequestDTO appointment_date);
    }
}
