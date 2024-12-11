using ClinicAppointmentTask.Models;

namespace ClinicAppointmentTask.Repositories
{
    public class PatientRepository : IPatientRepository
    {
        private readonly ApplicationDbContext _context;
        public PatientRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        //Gets all patients [returns list of patients]
        public List<Patient> GetAllPatients()
        {
            return _context.Patients.ToList();
        }


        //Checks if patient exists using patient name to search [returns bool]
        public bool PatientExists(string name) //checks if patient exists
        {
            var patient = _context.Patients.Where(c => c.Name == name).FirstOrDefault();

            if (patient == null) return false; //Patient does not exist
            else return true; //Patient exists 

        }


        //Adds new patient and returns patient ID 
        public int AddPatient(Patient patient)
        {
            _context.Patients.Add(patient);
            _context.SaveChanges();
            return patient.PID;
        }


        //Gets the patient ID [or 0 if not found] using the name 
        public int GetPatientID(string name)
        {
            //searching for patient using name 
            Patient patient = _context.Patients.FirstOrDefault(c => c.Name == name);

            if (name == null) return 0; //Patient not found 
            else return patient.PID; //Patient found

        }


        //Gets the whole patient object by name [null if not found]
        public Patient GetPatientByName(string name)
        {
            return _context.Patients.FirstOrDefault(p => p.Name == name);
        }
    }
}
