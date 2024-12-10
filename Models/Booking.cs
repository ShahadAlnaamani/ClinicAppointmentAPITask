using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClinicAppointmentTask.Models
{
    [PrimaryKey(nameof(Date), nameof(SlotNumber), nameof(CID))]

    public class Booking
    {
        public DateTime Date { get; set; }

        public int SlotNumber { get; set; }


        [ForeignKey(nameof(Patient))]
        public int PID { get; set; }
        public virtual Patient Patient { get; set; }


        [ForeignKey(nameof(CID))]
        public int CID { get; set; }
        public virtual Clinic Clinic { get; set; }
    }
}
