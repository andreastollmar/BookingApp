using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Models
{
    public class Day
    {
        public int Id { get; set; }
        public string WeekDay { get; set; }

        public virtual ICollection<WeekDayBooking> WeekDayBooking { get; set; }
    }
}
