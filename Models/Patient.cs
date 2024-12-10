using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace ClinicAppointmentTask.Models
{
    public enum Gender { Male, Female }

    public class Patient
    {
        [Key]
        public int PID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int Age { get; set; }

        [Required]
        public Gender Gender { get; set; }

        public virtual ICollection<Booking>? Bookings { get; set; }
    }
}
