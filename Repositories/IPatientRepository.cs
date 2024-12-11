using ClinicAppointmentTask.Models;

namespace ClinicAppointmentTask.Repositories
{
    public interface IPatientRepository
    {
        int AddPatient(Patient patient);
        List<Patient> GetAllPatients();
        Patient GetPatientByName(string name);
        int GetPatientID(string name);
        bool PatientExists(string name);
    }
}