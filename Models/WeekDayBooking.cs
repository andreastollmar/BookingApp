using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Models
{
    public class WeekDayBooking
    {
        public int Id { get; set; }
        public int WeekId { get; set; }
        public int DayId { get; set; }
        public int ConferenceRoomId { get; set; }
        public string? BookerName { get; set; }
        public string? CompanyBookerName { get; set; }
        public bool? Booked { get; set; }
        public virtual Day Day { get; set; }
        public virtual Week Week { get; set; }
        public virtual ConferenceRoom ConferenceRoom { get; set; }
    }
}
