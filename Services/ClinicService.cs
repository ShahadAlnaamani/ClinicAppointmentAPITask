using ClinicAppointmentTask.Models;
using ClinicAppointmentTask.Repositories;

namespace ClinicAppointmentTask.Services
{
    public class ClinicService : IClinicService
    {
        private readonly IClinicRepository _clinicRepository;

        public ClinicService(IClinicRepository clinicRepository)
        {
            _clinicRepository = clinicRepository;
        }


        //Adds new clinic and validates user input 
        public int AddClinic(Clinic clinic)
        {
            //If specialization is left empty will it return an error message 
            if (string.IsNullOrWhiteSpace(clinic.Specialization))
            {
                throw new ArgumentException("Clinic specialization required.");
            }

            //If number of slots is negative will it return an error message 
            if (int.IsNegative(clinic.NumberofSlots))
            {
                throw new ArgumentException("Number of slots must be a whole number.");
            }

            //If specialization is already in use it will return an error message 
            bool exists = _clinicRepository.ClinicExists(clinic.Specialization);
            if (exists)
            {
                throw new ArgumentException("<!>This clinic already exists<!>");
            }

            //After all checks it will add the clinic using the ClinicRepo
            return _clinicRepository.AddClinc(clinic);
        }


        //Gets all clinics and returns a list of them ordered by specialization
        public List<Clinic> GetAllClinics()
        {
            var clinics = _clinicRepository.GetAllClinics() //Gets list of all clinics
                .OrderBy(c => c.Specialization) //Orders clinics alphabetically by specialization
                .ToList(); //converts output to list

            //checks if no clinics in list and returns an error if none found
            if (clinics == null || clinics.Count == 0) 
            {
                throw new InvalidOperationException("No clinics found.");
            }

            //Otherwise returns list of clinics 
            return clinics;
        }


        //Gets clinic ID using name [0 if not found]
        public int GetClinicID(string ClinicSpecialization)
        {
            return _clinicRepository.GetClinicID(ClinicSpecialization);
        }


        //Gets next available slot 
        public int GetNextClinicSlot(string ClinicSpecialization)
        {
            return _clinicRepository.GetNextSlot(ClinicSpecialization);
        }
    }
}
