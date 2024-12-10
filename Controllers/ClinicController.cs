using ClinicAppointmentTask.Models;
using ClinicAppointmentTask.Services;
using Microsoft.AspNetCore.Mvc;

namespace ClinicAppointmentTask.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class ClinicController :ControllerBase
    {
        private readonly IClinicService _clinicService;

        public ClinicController(IClinicService clinicService)
        {
            _clinicService = clinicService;
        }

        //Adds new clinic
        [HttpPost("AddClinic")]
        public IActionResult AddClinic(string specialization, int noSlots)
        {
            try
            {
                int newClinicId = _clinicService.AddClinic(new Clinic
                {
                    NumberofSlots = noSlots,
                    Specialization = specialization,
                });
                return Created(string.Empty, newClinicId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        //Gets all clinics 
        [HttpGet("GetAllClinics")]
        public IActionResult GetAllClinics()
        {
            try
            {
                var clinics = _clinicService.GetAllClinics();
                return Ok(clinics);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
