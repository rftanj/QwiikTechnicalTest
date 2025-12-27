using Microsoft.EntityFrameworkCore;
using QwiikTechnicalTest.Models;
using QwiikTechnicalTest.Models.DB;
using QwiikTechnicalTest.Models.DTO.Appointment;
using QwiikTechnicalTest.Utilities;

namespace QwiikTechnicalTest.Repositories
{
    public class AppointmentRepository
    {
        private readonly ApplicationContext _context;
        public AppointmentRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<List<Appointment>> GetListDataAppointments(ListAppointmentRequestDTO dto)
        {
            var datas =  await _context.Appointments
                .Include(x => x.Customer)
                .Where(x => (string.IsNullOrEmpty(dto.status) || x.Status.ToString() == dto.status) &&
                      (string.IsNullOrEmpty(dto.customer_name) || x.Customer.Name.ToLower().Contains(dto.customer_name)) &&
                      (string.IsNullOrEmpty(dto.appointment_date) || x.AppointmentDate == DateTime.Parse(dto.appointment_date))
                )
                .OrderBy(x => x.AppointmentDate).ThenBy(x => x.AppointmentTime)
                .ToListAsync();

            return datas;
        }

        public async Task<bool> CreateAppointment(Appointment appointment)
        {
            await _context.Appointments.AddAsync(appointment);
            var isAffected = await _context.SaveChangesAsync();
            return isAffected > 0;
        }

        public async Task<string> GetLastToken(DateTime appointmentDate)
        {
            var lastAppointment = await _context.Appointments
                .Where(a => a.AppointmentDate == appointmentDate.Date)
                .OrderByDescending(a => a.CreatedAt)
                .FirstOrDefaultAsync();
            return lastAppointment?.Token ?? string.Empty;
        }

        public async Task<bool> IsAppointmentAvailable(TimeOnly appointment_time, DateTime appointment_date)
        {
            var isExist = await _context.Appointments.Where(x => x.AppointmentDate == appointment_date.Date && 
                        x.AppointmentTime == appointment_time).FirstOrDefaultAsync();

            if (isExist == null)
                return true;

            return false;
        }

        public async Task<int> GetAppointmentCountByDate(DateTime appointment_date)
        {
            return await _context.Appointments.CountAsync(x => x.AppointmentDate == appointment_date.Date && x.Status != AppointmentStatus.Cancelled);
        }

        public async Task<bool> CheckExistingAppointment(DateTime date, TimeOnly time)
        {
            return await _context.Appointments.AnyAsync(a =>
                         a.AppointmentDate.Date == date.Date &&
                         a.AppointmentTime == time
                    );
        }

        public async Task<List<TimeOnly>> GetAvailableSlots(DateTime date)
        {
            var bookedTimes = await _context.Appointments.Where(x => x.AppointmentDate.Date == date)
                                .Select(x => x.AppointmentTime)
                                .ToListAsync();

            var slots = new List<TimeOnly>();
            var current = AppointmentSlots.Start;

            while (current < AppointmentSlots.End)
            {
                if (!bookedTimes.Contains(current))
                    slots.Add(current);

                current = current.AddHours(1);
            }

            return slots;
        }

    }
}
