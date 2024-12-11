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
                //checks that number of slots not over 20 and that specialization length is not more than 20
                int Validated = _clinicService.NewClinicValidation(noSlots, specialization);


                switch(Validated)
                {
                    case 0:
                        int newClinicId = _clinicService.AddClinic(new Clinic
                        {
                            NumberofSlots = noSlots,
                            Specialization = specialization,
                        });
                        return Created(string.Empty, newClinicId);

                    case 1:
                        return BadRequest("<!>Number of slots must not exceed 20<!>");

                    case 2:
                        return BadRequest("<!>Specialization too long<!>");

                    default: return BadRequest("<!>Error occured in creating clinic try again<!>");
                }

                
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
