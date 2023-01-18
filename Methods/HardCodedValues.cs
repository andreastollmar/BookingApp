using BookingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Methods
{
    internal class HardCodedValues
    {
        private static void InsertWeekDays()
        {
            using (var db = new BookingAppContext())
            {
                string[] values = "Monday, Tuesday, Wednesday, Thursday, Friday".Split(", ");
                var resultList = new List<Day>();

                for (int i = 0; i < values.Length; i++)
                {
                    resultList.Add(new Day() { WeekDay = values[i] });
                }
                db.AddRange(resultList);
                db.SaveChanges();
            }
        }
        private static void InsertWeek()
        {
            using (var db = new BookingAppContext())
            {
                int[] values = {1, 2, 3, 4};
                var resultList = new List<Week>();

                for (int i = 0; i < values.Length; i++)
                {
                    resultList.Add(new Week() { WeekNr = values[i] });
                }
                db.AddRange(resultList);
                db.SaveChanges();
            }
        }
        private static void InsertConferenceRoom()
        {
            using (var db = new BookingAppContext())
            {
                string roomName = "Apollo";
                int roomSize = 8;
                var newRoom = new ConferenceRoom
                {
                    Name = roomName,
                    NrOfSeatings = roomSize

                };

                
                db.Add(newRoom);
                db.SaveChanges();
            }
        }
        public static void InsertWeekDayConferenceRoom()
        {
            using (var db = new BookingAppContext())
            {
                var dayList = db.Days.ToList();
                for(int i = 1; i < dayList.Count(); i++ )
                {
                    var newBooking = new WeekDayBooking
                    {
                        WeekId = 1,
                        DayId = (i + 1),
                        ConferenceRoomId = 1
                    };

                    db.Add(newBooking);
                    db.SaveChanges();
                }
               
            }
        }
        public static void AllInserts()
        {
            InsertWeekDays();
            InsertWeek();
            InsertConferenceRoom();
        }

    }
}
