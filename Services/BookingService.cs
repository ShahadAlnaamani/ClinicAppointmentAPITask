using ClinicAppointmentTask.Models;
using ClinicAppointmentTask.Repositories;

namespace ClinicAppointmentTask.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IPatientService _patientService;
        private readonly IClinicService _clinicService;

        public BookingService(IBookingRepository bookingRepository, IPatientService patientService, IClinicService clinicService)
        {
            _bookingRepository = bookingRepository;
            _patientService = patientService;
            _clinicService = clinicService;
        }


        //Adds new booking [returns clinic ID]
        public int AddBooking(DateTime date, string patientName, string clinicSpecialization)
        {
            int PatientID = _patientService.GetPatientID(patientName);

            //Calculating slot number 
            int clinicID = _clinicService.GetClinicID(clinicSpecialization);
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
            bool patientExists = _patientService.PatientExists(patientName);
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
                    int PatientID = _patientService.GetPatientID(patientName);
                    int TotalSlots = _clinicService.GetNextClinicSlot(clinicSpecialization);

                    //Checking slot availability
                    if (TotalSlots != 0) //found clinic
                    {
                        //Calculating slot number 
                        int clinicID = _clinicService.GetClinicID(clinicSpecialization);

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
