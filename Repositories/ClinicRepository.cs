using ClinicAppointmentTask.Models;

namespace ClinicAppointmentTask.Repositories
{
    public class ClinicRepository : IClinicRepository
    {
        private readonly ApplicationDbContext _context;
        public ClinicRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        //Returns a list of all the clinics 
        public List<Clinic> GetAllClinics()
        {
            return _context.Clinics.ToList();
        }


        //Checks if a clinic exists or not using specialization [returns bool]
        public bool ClinicExists(string Specialization)
        {
            var clinic = _context.Clinics.Where(c => c.Specialization == Specialization).FirstOrDefault();

            if (clinic == null) return false; //Clinic not found 
            else return true; //Clinic found 

        }


        //Adds clinic and returns the new clinic ID 
        public int AddClinc(Clinic clinic)
        {
            _context.Clinics.Add(clinic);
            _context.SaveChanges();
            return clinic.CID;
        }


        //Checks the total capacity of slots for a clinic using specialization (both booked and not),
        //calculation for available slots done in BookingService
        public int GetNextSlot(string Specialization)
        {
            Clinic clinic = _context.Clinics.FirstOrDefault(c => c.Specialization == Specialization);

            if (clinic == null) return 0; //means none available 
            else
            {
                return clinic.NumberofSlots;
            };
        }


        //Gets the clinic ID using specialization
        public int GetClinicID(string Specialization)
        {
            Clinic clinic = _context.Clinics.FirstOrDefault(c => c.Specialization == Specialization);

            if (clinic == null) return 0;
            else
            {
                int ID = clinic.CID;
                return ID;
            };

        }


        //Gets the clinic object using the ID 
        public Clinic GetClinicByID(int ID)
        {
            try
            {
                Clinic clinic = _context.Clinics.FirstOrDefault(c => c.CID == ID);

                return clinic; //if not found will return null 

            }catch(Exception ex) { return null; } //incase of issues 

        }
    }
}
