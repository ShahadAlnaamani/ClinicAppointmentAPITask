using ClinicAppointmentTask.Models;

namespace ClinicAppointmentTask.Services
{
    public interface IPatientService
    {
        int AddPatient(Patient patient);
        List<Patient> GetAllPatients();
        public int GetPatientID(string patientName);
        public bool PatientExists(string patientName);
        public int PatientValidation(string name, int age, Gender gender);
    }
}