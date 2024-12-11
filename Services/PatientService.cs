using ClinicAppointmentTask.Models;
using ClinicAppointmentTask.Repositories;
using System.Globalization;

namespace ClinicAppointmentTask.Services
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _patientRepository;

        public PatientService(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }

        //Validates new patient to added be added before sending request to add
        public int AddPatient(Patient patient)
        {
            //Patient name left blank will return error message 
            if (string.IsNullOrWhiteSpace(patient.Name))
            {
                throw new ArgumentException("<!>Patient name required<!>");
            }

            //Patient age negative blank will return error message
            if (int.IsNegative(patient.Age))
            {
                throw new ArgumentException("<!>Age must be a whole number<!>");
            }

            //Patient already exists will return error message 
            bool exists = _patientRepository.PatientExists(patient.Name);
            if (exists)
            {
                throw new ArgumentException("<!>This patient already exists<!>");
            }

            //Sends patient object to repo to be added to DB 
            return _patientRepository.AddPatient(patient);
        }


        //Gets all patients from repo then orders them by PatientName alphabetically 
        public List<Patient> GetAllPatients()
        {
            var patients = _patientRepository.GetAllPatients() //gets all patients
                .OrderBy(p => p.Name) //Orders patient by name 
                .ToList(); //converts to list 

            //Checks if no patients found if none will return error message 
            if (patients == null || patients.Count == 0)
            {
                throw new InvalidOperationException("No patients found.");
            }

            //returns patients list 
            return patients;
        }


        //Returns patient ID using patient name [0 if not found]
        public int GetPatientID(string patientName)
        {
            return  _patientRepository.GetPatientID(patientName);
        }


        //Checks to see if patient exists or not [returns bool]
        public bool PatientExists(string patientName)
        {
            return _patientRepository.PatientExists(patientName);
        }
    }
}
