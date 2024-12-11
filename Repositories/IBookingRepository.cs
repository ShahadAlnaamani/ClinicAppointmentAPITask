using ClinicAppointmentTask.Models;

namespace ClinicAppointmentTask.Repositories
{
    public interface IBookingRepository
    {
        int Add(Booking booking);
        List<Booking> GetAllBookings();
        List<Booking> GetBookingsByClinicID(int ClinicID);
        List<Booking> GetBookingsByDate(DateTime Date);
        List<Booking> GetBookingsByPatientID(int PatientID);
        int GetTakenSlots(DateTime date, int clinicID);
    }
}