using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Models
{
    public class ConferenceRoom
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int NrOfSeatings { get; set; }
        public virtual ICollection<WeekDayBooking> WeekDayBooking { get; set; }
    }
}
