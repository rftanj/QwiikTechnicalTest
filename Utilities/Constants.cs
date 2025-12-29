using System.Net.NetworkInformation;

namespace QwiikTechnicalTest.Utilities
{
    public static class Constants
    {
        public static string NotFoundMessage = "Data not found!";
        public static string GeneralErrorMessage = "An error occurred, please try again later.";
        public static string AppointmentUnavailableMsg = "Appointment not available on {0} at {1}, please select another date or time";
        public static string FailedCreateAppointmnetMsg = "Failed to create appointment, please try again later.";
        public static string CustomerNotFoundMsg = "Customer not found, please register customer first.";
        public static string InvalidAppointmentTime = "Invalid appointment time";
        public static string AppointmentTimeNotAvailable = "There is no appointment time available on {0}, please select another date.";
        public static string LoginFailed = "Email and Password doesnt match.";
        public static string LoginSuccess = "Login success.";
    }
}
