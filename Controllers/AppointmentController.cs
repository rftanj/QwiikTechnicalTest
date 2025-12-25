using Microsoft.AspNetCore.Mvc;
using QwiikTechnicalTest.Interfaces;
using QwiikTechnicalTest.Models;
using QwiikTechnicalTest.Models.DTO.Appointment;
using QwiikTechnicalTest.Models.DTO.Customer;
using QwiikTechnicalTest.Services;
using QwiikTechnicalTest.Utilities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace QwiikTechnicalTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointment _appointmentService;

        public AppointmentController(IAppointment appointment)
        {
            _appointmentService = appointment;
        }


        [HttpPost("list-appointment")]
        public async Task<IActionResult> GetDataAppointment([FromBody] ListAppointmentRequestDTO dto)
        {
            try
            {
                var dataAppointments = await _appointmentService.GetListDataAppointments(dto);
                if (dataAppointments is null)
                {
                    return Ok(GeneralResponse<string>.Fail(Constants.NotFoundMessage));
                }

                return Ok(GeneralResponse<List<AppointmentResponseDTO>>.Success(dataAppointments));

            }
            catch (Exception ex)
            {
                return BadRequest(GeneralResponse<string>.Fail(Constants.GeneralErrorMessage));
            }
        }

        [HttpPost("create-appointment")]
        public async Task<IActionResult> CreateAppointment([FromBody] AppointmentRequestDTO dto)
        {
            try
            {
                var (message, isSuccess) = await _appointmentService.CreateAppointment(dto);
                if (!isSuccess)
                {
                    return Ok(GeneralResponse<string>.Fail(message));
                }

                return Ok(GeneralResponse<string>.Success(message));
            }
            catch (Exception)
            {
                return BadRequest(GeneralResponse<string>.Fail(Constants.GeneralErrorMessage));
                throw;
            }
        }

        [HttpPost("schedule-appointment")]
        public async Task<IActionResult> GetAvailableTime([FromBody] AvailableTimeRequestDTO appointment_date)
        {
            try
            {
                var (datas, message) = await _appointmentService.GetAvailableTime(appointment_date);
                if (!string.IsNullOrEmpty(message))
                {
                    return Ok(GeneralResponse<string>.Fail(message));
                }

                return Ok(GeneralResponse<List<AvailableTimeResponseDTO>>.Success(datas));
            }
            catch (Exception)
            {
                return BadRequest(GeneralResponse<string>.Fail(Constants.GeneralErrorMessage));
                throw;
            }
        }

    }
}
