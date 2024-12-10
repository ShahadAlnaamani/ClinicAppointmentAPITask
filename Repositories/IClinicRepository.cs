using ClinicAppointmentTask.Models;

namespace ClinicAppointmentTask.Repositories
{
    public interface IClinicRepository
    {
        int AddClinc(Clinic clinic);
        bool ClinicExists(string Specialization);
        List<Clinic> GetAllClinics();
        Clinic GetClinicByID(int ID);
        int GetClinicID(string Specialization);
        int GetNextSlot(string Specialization);
    }
}