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

        /// <summary>
        /// Get list of appointments based on filter criteria.
        /// </summary>
        /// <remarks>
        /// This endpoint retrieves appointment data based on optional filters:
        /// - appointment_date
        /// - customer_name
        /// - status
        ///
        /// This endpoint always returns HTTP 200.
        /// Business outcome is indicated by the 'status' field in the response body.
        /// </remarks>
        /// <param name="dto">Filter parameters for listing appointments</param>
        /// <response code="200">
        /// Returns appointment list when data exists, otherwise returns status false with message.
        /// </response>
        /// <response code="400">
        /// Returns general error message when unexpected error occurs.
        /// </response>
        [Produces("application/json")]
        [ProducesResponseType(typeof(GeneralResponse<List<AppointmentResponseDTO>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(GeneralResponse<string>), StatusCodes.Status400BadRequest)]
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

        /// <summary>
        /// Create a new appointment.
        /// </summary>
        /// <remarks>
        /// This endpoint creates a new appointment for a customer.
        /// 
        /// Business rules:
        /// - Appointment time must be within working hours
        /// - Appointment slot must be available
        /// - If the selected date is full, the appointment may be shifted to the next available date
        /// 
        /// This endpoint always returns HTTP 200.
        /// Business outcome is indicated by the 'status' field in the response body.
        /// </remarks>
        /// <param name="dto">Appointment creation request data</param>
        /// <response code="200">
        /// Appointment created successfully or business validation failed.
        /// </response>
        /// <response code="400">
        /// Unexpected system error.
        /// </response>
        [HttpPost("create-appointment")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(GeneralResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(GeneralResponse<string>), StatusCodes.Status400BadRequest)]
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


        /// <summary>
        /// Get available appointment time slots for a specific date
        /// </summary>
        /// <remarks>
        /// This endpoint always returns HTTP 200.
        /// Business outcome is indicated by the 'status' field in the response body.
        ///
        /// Possible outcomes:
        /// - status = true : Available slots are returned
        /// - status = false : Request failed due to business validation
        ///
        /// Failure scenarios:
        /// - Invalid date or non-working day
        /// - No available appointment slots
        /// - Unexpected system error
        /// </remarks>
        /// <response code="200">
        /// Returns operation result wrapped in ApiResponse object.
        /// </response>
        /// <response code="400">
        /// Returns general error message when unexpected error occurs.
        /// </response>
        [HttpPost("schedule-appointment")]
        [ProducesResponseType(typeof(List<GeneralResponse<AvailableTimeResponseDTO>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(GeneralResponse<string>), StatusCodes.Status400BadRequest)]
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
