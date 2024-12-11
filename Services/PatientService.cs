using ClinicAppointmentTask.Models;
using ClinicAppointmentTask.Repositories;
using System.Globalization;
using System.Reflection;
using System.Text.RegularExpressions;

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


        //Validates patient information 
        public int PatientValidation(string name, int age, Gender gender)
        {
            //Validates that age is between 1-100
            if (age == 0 || age == null || age >100)
                return 1; 

            //Validates that gender is
            //not null 
            if (gender == null)
                return 2;

            //Validates that name is not null and does not contain numbers or special characters 
            //Also checks that full name is given 
            if (name == null || (!Regex.IsMatch(name, @"^[a-zA-Z]")))
                return 3;

            //Everything is good to go ahead
            return 0;
        }
    }
}
