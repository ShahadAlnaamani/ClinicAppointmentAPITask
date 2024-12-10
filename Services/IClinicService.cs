using ClinicAppointmentTask.Models;

namespace ClinicAppointmentTask.Services
{
    public interface IClinicService
    {
        int AddClinic(Clinic clinic);
        List<Clinic> GetAllClinics();
    }
}