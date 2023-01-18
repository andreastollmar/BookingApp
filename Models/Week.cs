using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Models
{
    public class Week
    {
        public int Id { get; set; }
        public int WeekNr { get; set; }

        public virtual ICollection<WeekDayBooking> WeekDayBooking { get; set; }
    }
}
