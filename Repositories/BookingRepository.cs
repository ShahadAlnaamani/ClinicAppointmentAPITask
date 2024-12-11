using ClinicAppointmentTask.Models;

namespace ClinicAppointmentTask.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly ApplicationDbContext _context;
        public BookingRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        //Gets all bookings [returns list]
        public List<Booking> GetAllBookings()
        {
            return _context.Bookings.ToList();
        }


        //Adds new booking [returns clinicID]
        public int Add(Booking booking)
        {
            _context.Bookings.Add(booking);
            _context.SaveChanges();
            return booking.CID;
        }

        //Gets bookings using date to search [returns list]
        public List<Booking> GetBookingsByDate(DateTime Date)
        {
            return _context.Bookings
                .Where(b => b.Date == Date) //Searches by date 
                .ToList(); //converts to list 
            //will return null if none found 
        }


        //Gets Bookings by clinic ID [returns list]
        public List<Booking> GetBookingsByClinicID(int ClinicID)
        {
            return _context.Bookings.Where(b => b.CID == ClinicID).ToList();
        }


        //Gets Bookings by patient ID [returns list]
        public List<Booking> GetBookingsByPatientID(int PatientID)
        {
            return _context.Bookings.Where(b => b.PID == PatientID).ToList();
        }


        //Gets number of taken slots using date and clinic ID 
        public int GetTakenSlots(DateTime date, int clinicID)
        {
            //Counts how many have the same date and clinic ID 
            //This will get last slot number given to a patient for that clinic on that date 
            return _context.Bookings.Count(b => b.CID == clinicID && b.Date == date);
            //returns 0 if none 
        }
    }
}
