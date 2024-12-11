using ClinicAppointmentTask.Services;
using Microsoft.AspNetCore.Mvc;

namespace ClinicAppointmentTask.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class BookingController : ControllerBase 
    {
        private readonly IBookingService _bookingService;

        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }


        [HttpPost("AddBooking")]
        public IActionResult AddBooking(DateTime date, string patientName, string clinicSpecialization)
        {
            try
            {
                int result = _bookingService.Validation(date, patientName, clinicSpecialization);

                switch (result)
                {
                    case 0:
                        return Ok(_bookingService.AddBooking(date, patientName, clinicSpecialization));

                    case 1:
                        return BadRequest("<!>Date must be in the future<!>");

                    case 2:
                        return BadRequest("<!>Invalid patient<!>");

                    case 3:
                        return BadRequest("<!>No available slots for this date<!>");

                    default:
                        return StatusCode(500, "<!>Error occured in creating booking, booking did not go through<!>");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetAllBookings")]
        public IActionResult GetAllBookings()
        {
            try
            {
                //gets bookings 
                var admins = _bookingService.GetAllBookings();

                //checks if none found and throws error message 
                if (admins == null || admins.Count == 0)
                {
                    throw new InvalidOperationException("No bookings found.");
                }

                return Ok(admins);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
