using BookingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Methods;

namespace BookingApp.Methods
{
    internal class Admin
    {
        internal static void AddConferenceRoom()
        {
            using (var database = new BookingAppContext())
            {
                Console.WriteLine("Enter name of conference Room: ");
                string newName = Helpers.CheckStringInput();
                Console.WriteLine("Enter number of seats in " + newName);
                int nrOfSeats = Helpers.TryNumber(20, 6);

                var newConferenceRoom = new ConferenceRoom
                {
                    Name = newName,
                    NrOfSeatings = nrOfSeats
                };
                database.Add(newConferenceRoom);
                database.SaveChanges();
            }
            AddDayWeeksToConferenceRoom();
        }
        public static void AddDayWeeksToConferenceRoom()
        {
            using (var db = new BookingAppContext())
            {
                var conferenceRoomList = db.ConferenceRooms.OrderBy(x => x.Id).LastOrDefault();
                var conferenceRoom = db.ConferenceRooms.SingleOrDefault(x => x.Id == conferenceRoomList.Id);
                var weekList = db.Weeks.ToList();
                var dayList = db.Days.ToList();

                for(int i = 0; i < weekList.Count; i++)
                {
                    int weekNr = i +1;
                    for (int j = 0; j < dayList.Count(); j++)
                    {
                        var weekDayBooking = new WeekDayBooking
                        {
                            ConferenceRoomId = conferenceRoom.Id,
                            WeekId = weekNr,
                            DayId = j + 1
                        };
                        db.Add(weekDayBooking);
                        db.SaveChanges();
                    }

                }
            }
        }

    }
}
