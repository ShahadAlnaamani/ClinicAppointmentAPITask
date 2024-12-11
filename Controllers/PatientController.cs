using ClinicAppointmentTask.Models;
using ClinicAppointmentTask.Services;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ClinicAppointmentTask.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class PatientController : ControllerBase
    {
        private readonly IPatientService _patientService;

        public PatientController(IPatientService patientService)
        {
            _patientService = patientService;
        }

        [HttpPost("AddPatient")]
        public IActionResult AddPatient(string name, int age, Gender gender)
        {
            try
            {
                int Validation = _patientService.PatientValidation(name, age, gender);

                switch (Validation)
                {
                    case 0:
                        int newPatientId = _patientService.AddPatient(new Patient
                        {
                            Name = name,
                            Age = age,
                            Gender = gender
                        });
                        return Created(string.Empty, newPatientId);

                    case 1:
                        return BadRequest("<!>Age must be between 1-100<!>");

                    case 2:
                        return BadRequest("<!>Gender must be added<!>");

                    case 3:
                        return BadRequest("<!>Include first and last name with no special characters or numbers<!>");

                    default: return BadRequest("<!>Error occured in adding patient try again<!>");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("GetAllPatients")]
        public IActionResult GetAllPatients()
        {
            try
            {
                var patients = _patientService.GetAllPatients();
                return Ok(patients);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
