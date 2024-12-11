using ClinicAppointmentTask.Models;

namespace ClinicAppointmentTask.Services
{
    public interface IClinicService
    {
        int AddClinic(Clinic clinic);
        List<Clinic> GetAllClinics();
        public int GetClinicID(string ClinicName);
        public int GetNextClinicSlot(string ClinicSpecialization);
        public bool ClinicSlotNumberOk(int slots);
        public int ClinicSpecializationOk(string specialization);
        public int NewClinicValidation(int slots, string specialization);
        public bool ClinicExists(string specialization);

    }
}