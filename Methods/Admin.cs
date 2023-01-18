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

        internal static void EditRoom()
        {
            using (var db = new BookingAppContext())
            {
                var conferenceRoomList = db.ConferenceRooms.ToList();
                int i = 0;

                foreach(var room in conferenceRoomList)
                {
                    i++;
                    Console.WriteLine(i + ". Name: " + room.Name.PadRight(20) + " Seatings: " + room.NrOfSeatings);
                }
                Console.WriteLine("Enter Index of room you want to edit, [0] to return");
                int roomToEdit = Helpers.TryNumber(conferenceRoomList.Count(), 0);
                if(roomToEdit == 0)
                {
                    Console.Clear();
                    Menus.ShowMenu("Main");
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("What do you want to change? \n[1] = Name \n[2] = Seatings");
                    int thingToChange = Helpers.TryNumber(2, 1);

                    if(thingToChange == 1)
                    {
                        string input = "";
                        Console.WriteLine("Enter new name of Conference Room");
                        input = Helpers.CheckStringInput();
                        var conferenceRoomToEdit = db.ConferenceRooms.Where(x => x.Id == conferenceRoomList[roomToEdit - 1].Id).SingleOrDefault();
                        conferenceRoomToEdit.Name = input;
                        db.SaveChanges();
                        Console.Clear();
                        Console.WriteLine("Name is updated");
                        Thread.Sleep(1000);
                        Menus.ShowMenu("Main");
                        
                    }
                    else
                    {
                        int input = 0;
                        Console.Write("Enter new number of seatings for conferenceroom: ");
                        input = Helpers.TryNumber(24, 6);
                        var conferenceRoomToEdit = db.ConferenceRooms.Where(x => x.Id == conferenceRoomList[roomToEdit - 1].Id).SingleOrDefault();
                        conferenceRoomToEdit.NrOfSeatings = input;
                        db.SaveChanges();
                        Console.Clear();
                        Console.WriteLine("Seatings are updated");
                        Thread.Sleep(1000);
                        Menus.ShowMenu("Main");
                    }
                }
                    
            }
        }
    }
}
