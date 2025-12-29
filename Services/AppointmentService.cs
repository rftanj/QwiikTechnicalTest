using Microsoft.EntityFrameworkCore;
using QwiikTechnicalTest.Interfaces;
using QwiikTechnicalTest.Models.DB;
using QwiikTechnicalTest.Models.DTO.Appointment;
using QwiikTechnicalTest.Repositories;
using QwiikTechnicalTest.Utilities;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace QwikkTechnicalTest.Services
{
    public class AppointmentService: IAppointment
    {
        private readonly AppointmentRepository _appointmentRepository;
        private readonly CustomerRepository _customerRepository;
        private readonly UserRepository _userRepository;
        private readonly IConfiguration _configuration;
        private readonly int _maxAppointmentsPerDay;
        private readonly string _pepper;
        private readonly string _iteration;
        public AppointmentService(AppointmentRepository appointment, UserRepository user, CustomerRepository customerRepository, IConfiguration configuration)
        {
            _appointmentRepository = appointment;
            _customerRepository = customerRepository;
            _userRepository = user;
            _configuration = configuration;
            _maxAppointmentsPerDay = int.Parse(_configuration["MaxAppointmentsPerDay"]);
            _pepper = configuration.GetSection("Security:Pepper").Value ?? "";
            _iteration = configuration.GetSection("Security:Iteration").Value ?? "";
        }

        public async Task<List<AppointmentResponseDTO>> GetListDataAppointments(ListAppointmentRequestDTO dto)
        {
            try
            {
                var appointments = await _appointmentRepository.GetListDataAppointments(dto);
                if (appointments.Count == 0)
                {
                    return null;
                }

                var appointmentsDtos = appointments.Select(x => new AppointmentResponseDTO
                {
                    id = x.Id,
                    customer_name = x.Customer.Name,
                    appointment_date = x.AppointmentDate.ToString("dd MMM yyyy"),
                    appointment_time = x.AppointmentTime.ToString("HH:mm"),
                    status = x.Status.ToString(),
                    token = x.Token,
                    created_at = x.CreatedAt.ToString("dd MMM yyyy HH:mm"),
                }).ToList();

                return appointmentsDtos;
            }
            catch (Exception)
            {

                throw;
            }
           
        }

        public async Task<(string message, bool isSuccess)> CreateAppointment(AppointmentRequestDTO appointment)
        {
            try
            {
                var time = TimeOnly.ParseExact(appointment.appointment_time, "HH:mm");
                if (!AppointmentSlots.IsValidSlot(time))
                    return(Constants.InvalidAppointmentTime, false);

                DateTime appointmentDate = appointment.appointment_date.Date;
                var isMaxReached = await isMaxAppointmentsReached(appointment.appointment_date);
                if (isMaxReached)
                {
                    appointmentDate = appointmentDate.AddDays(1);
                }

                var customer_id = await createNewCustomerOrUseExisting(appointment);

                
                var isAppointmentAvailable = await _appointmentRepository.IsAppointmentAvailable(time, appointment.appointment_date);
                if (!isAppointmentAvailable)
                {
                    return (string.Format(Constants.AppointmentUnavailableMsg, appointment.appointment_date.ToString("dd MMM yyyy"), time), false);
                }

                var lastToken = await _appointmentRepository.GetLastToken(appointment.appointment_date);
                var newToken = NewTokenGenerator.GenerateToken(lastToken);

                var appointmentData = new Appointment
                {
                    CustomerId = customer_id,
                    AppointmentTime = time,
                    AppointmentDate = appointmentDate,
                    Status = AppointmentStatus.Scheduled,
                    CreatedAt = DateTime.UtcNow,
                    Token = newToken
                };

                var isSuccess = await _appointmentRepository.CreateAppointment(appointmentData);
                if (!isSuccess)
                {
                    return (Constants.FailedCreateAppointmnetMsg, false);
                }

                return (newToken, true);
            }
            catch (Exception)
            {
                return (Constants.FailedCreateAppointmnetMsg, false);
                throw;
            }
        }

        public async Task<(List<AvailableTimeResponseDTO> datas, string message)> GetAvailableTime(AvailableTimeRequestDTO dto)
        {
            try
            {
                var date = Convert.ToDateTime(dto.appointment_date);
                var datas = await _appointmentRepository.GetAvailableSlots(date);
                if (datas.Count == 0)
                {
                    return (new List<AvailableTimeResponseDTO>(), string.Format(Constants.AppointmentTimeNotAvailable, date.ToString("dd MM yyyy")));
                }

                var times = datas.Select(x => new AvailableTimeResponseDTO
                {
                    appointment_time = x.ToString("HH:mm")
                }).ToList();

                return (times, string.Empty);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task<int> createNewCustomerOrUseExisting(AppointmentRequestDTO appointment)
        {
            int customer_id;
            var isCustomerExist = await _customerRepository.GetCustomerByPhoneNumber(appointment.phone_number);
            if (isCustomerExist is null)
            {
                customer_id = await _customerRepository.CreateCustomer(new Customer
                {
                    Name = appointment.customer_name,
                    Email = appointment.email,
                    PhoneNumber = appointment.phone_number,
                    CreatedAt = DateTime.UtcNow
                });

                //var isUserCreated = await createLoginUser(appointment);
                //if (!isUserCreated) return 0;
            }
            else
            {
                customer_id = isCustomerExist.Id;
            }

            return customer_id;
        }

        //private async Task<bool> createLoginUser(AppointmentRequestDTO dto)
        //{
        //    var user = new User
        //    {
        //        Role = UserRole.Customer,
        //        Email = dto.email,
        //        Salt = Hasher.GenerateSalt(),
        //        CreatedAt = DateTime.UtcNow
        //    };

        //    user.PasswordHash = Hasher.ComputeHash(dto.password, user.Salt, _pepper, Convert.ToInt32(_iteration));
        //    var isSuccess = await _userRepository.CreateUserLogin(user);
        //    return isSuccess;
        //}

        private async Task<bool> isMaxAppointmentsReached(DateTime appointment_date)
        {
            var appointmentCount = await _appointmentRepository.GetAppointmentCountByDate(appointment_date);
            return appointmentCount >= _maxAppointmentsPerDay;
        }

        public async Task<bool> isSlotAvailable(DateTime date, TimeOnly time)
        {
            if (!AppointmentSlots.IsValidSlot(time))
                return false;

            return !await _appointmentRepository.CheckExistingAppointment(date, time);
        }

    }
}
