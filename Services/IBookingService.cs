using ClinicAppointmentTask.Models;

namespace ClinicAppointmentTask.Services
{
    public interface IBookingService
    {
        int AddBooking(DateTime date, string patientName, string clinicSpecialization);
        List<Booking> GetAllBookings();
        bool PatientExists(string patientName);
        int Validation(DateTime date, string patientName, string clinicSpecialization);
    }
}