using ClinicAppointmentTask.Models;
using ClinicAppointmentTask.Repositories;
using System.Text.RegularExpressions;

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

        //Validation of user input for adding new clinic
        public int NewClinicValidation(int slots, string specialization)
        {
            bool SlotNos = ClinicSlotNumberOk(slots);
            int Specialization = ClinicSpecializationOk(specialization);

            if (SlotNos)
            {
                //Specialization length and slot number acceptable 
                if (Specialization == 0)
                    return 0; 

                //Specialization length is too short 
                else if (Specialization == 2)
                    return 3;

                //Specialization contains charachters other than letters  
                else if (Specialization == 3)
                    return 4;

                else return 2; //specialization length too long
            }

            else return 1; //slots no over 20 
        }


        //Checks that the number of slots does not exceed the maximum (20)
        public bool ClinicSlotNumberOk(int slots)
        {
            if (slots > 20) return false; //checks slot number less than 20
            else return true;
        }

        //Checks the format of specialization
        public int ClinicSpecializationOk(string specialization)
        {
            //checks length of specialization more than 20
            if (specialization.Length > 20)
                return 1; 
            
            //checks that specialization length is more than 1 
            else if (specialization.Length < 2 || specialization == null)
                return 2;

            //checks that specialization contains letters 
            if (!Regex.IsMatch(specialization, @"^[a-zA-Z]"))
                return 3;

            //All good
            else return 0;
        }

    }
}
