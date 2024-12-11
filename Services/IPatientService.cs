using ClinicAppointmentTask.Models;

namespace ClinicAppointmentTask.Services
{
    public interface IPatientService
    {
        int AddPatient(Patient patient);
        List<Patient> GetAllPatients();
    }
}