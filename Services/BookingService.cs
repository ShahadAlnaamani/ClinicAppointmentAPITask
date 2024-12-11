using ClinicAppointmentTask.Models;
using ClinicAppointmentTask.Repositories;

namespace ClinicAppointmentTask.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IPatientRepository _patientRepository;
        private readonly IClinicRepository _clinicRepository;

        public BookingService(IBookingRepository bookingRepository, IPatientRepository patientRepository, IClinicRepository clinicRepository)
        {
            _bookingRepository = bookingRepository;
            _patientRepository = patientRepository;
            _clinicRepository = clinicRepository;
        }


        //Adds new booking [returns clinic ID]
        public int AddBooking(DateTime date, string patientName, string clinicSpecialization)
        {
            int PatientID = _patientRepository.GetPatientID(patientName);

            //Calculating slot number 
            int clinicID = _clinicRepository.GetClinicID(clinicSpecialization);
            int TakenSlots = _bookingRepository.GetTakenSlots(date, clinicID);

            int SlotNumber = TakenSlots + 1;

            var booking = new Booking
            {
                Date = date,
                SlotNumber = SlotNumber,
                PID = PatientID,
                CID = clinicID,
            };

            return _bookingRepository.Add(booking);

        }


        //Validates if patient exists using PatientName [returns bool]
        public bool PatientExists(string patientName)
        {
            bool patientExists = _patientRepository.PatientExists(patientName);
            if (patientExists)
            {
                return true; //patient found
            }
            else return false; //not found
        }


        //Conducts validation to all info before adding booking 
        public int Validation(DateTime date, string patientName, string clinicSpecialization)
        {
            if (date > DateTime.Now) //checks that date is in the future
            {
                //Checking if patient exists 
                bool patientExists = PatientExists(patientName);
                if (patientExists) //found patient
                {
                    int PatientID = _patientRepository.GetPatientID(patientName);
                    int TotalSlots = _clinicRepository.GetNextSlot(clinicSpecialization);

                    //Checking slot availability
                    if (TotalSlots != 0) //found clinic
                    {
                        //Calculating slot number 
                        int clinicID = _clinicRepository.GetClinicID(clinicSpecialization);

                        int TakenSlots = _bookingRepository.GetTakenSlots(date, clinicID);


                        if (TakenSlots < TotalSlots)
                        {
                            return 0; //no issues 

                        }
                        else { return 3; } //no slots available 

                    }
                    else { return 3; } //no slots availabe 
                }

                else { return 2; } //patient does not exist
            }
            else { return 1; };//bad date 

        }


        //Gets all bookings from booking repo and orders the results by date 
        public List<Booking> GetAllBookings()
        {
            var bookings = _bookingRepository.GetAllBookings() //gets all bookings
                .OrderBy(b => b.Date) //orders by date 
                .ToList(); // converts to list

            //returns list of bookings 
            return bookings;
        }
    }
}
