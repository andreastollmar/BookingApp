using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Models
{
    public class BookingAppContext : DbContext
    {
        public DbSet<ConferenceRoom> ConferenceRooms { get; set; }
        public DbSet<WeekDayBooking> WeekDayBookings { get; set; }
        public DbSet<Day> Days { get; set; }
        public DbSet<Week> Weeks { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.\\SQLExpress;Database=BookingAppDb;Trusted_Connection=True;");
        }
    }


}
