using BookingApp.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Methods
{

    internal class Helpers
    {
        static readonly string _connString = "Server=tcp:gameshopstopdb.database.windows.net,1433;Initial Catalog=bookingapAndreas;Persist Security Info=False;User ID=andreastollmar;Password=Hejsanmicke91;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";


        public static string CheckStringInput()
        {
            bool tryAgain = true;
            string? outPut = "";
            while (tryAgain)
            {
                outPut = Console.ReadLine();
                if (outPut == null)
                {
                    Console.WriteLine("Your input must contain atleast one character");
                }
                else if (outPut.Length > 0)
                {
                    tryAgain = false;
                }
                else
                {
                    Console.WriteLine("Your input must contain atleast one character");
                }

            }
            return outPut;
        }

        public static int TryNumber(int maxValue, int minValue)
        {
            int number = 0;
            bool correctInput = false;
            while (!correctInput)
            {
                if (!int.TryParse(Console.ReadLine(), out number) || number > maxValue || number < minValue)
                {
                    Console.Write("Wrong input, try again: ");
                }
                else
                {
                    correctInput = true;
                }
            }
            return number;
        }
        internal static void ShowConferenceRooms()
        {
            using (var database = new BookingAppContext())
            {
                var conferenceRoomList = database.ConferenceRooms.ToList();
                int i = 1;

                foreach (var d in conferenceRoomList)
                {
                    Console.WriteLine(i + "\t" + d.Name);
                    i++;
                }
                Console.WriteLine("Choose one room to manage: ");

                int chosenRoom = (TryNumber(conferenceRoomList.Count(), 1)) - 1;

            }
        }

        public static void BookConferenceRoom()
        {
            using (var db = new BookingAppContext())
            {
                int padSmall = 12;
                int padLarge = 17;
                var roomList = db.ConferenceRooms.ToList();
                int i = 0;
                int j = 0;
                var weekList = db.Weeks.ToList();
                foreach (var room in roomList)
                {
                    i++;
                    Console.WriteLine(i + " " + room.Name + " room for " + room.NrOfSeatings);
                }
                Console.WriteLine("Choose Room that you want to book: ");
                int roomChoiceId = TryNumber(roomList.Count(), 1);
                Console.Clear();
                foreach (var week in weekList)
                {
                    Console.WriteLine("Week " + week.Id);
                }
                Console.WriteLine("For what week do you want to check availabilty? ");
                int weekChoice = TryNumber(4, 1);                
                var result = (
                from weekDayBookings in db.WeekDayBookings
                join conferenceRoom in db.ConferenceRooms on weekDayBookings.ConferenceRoomId equals conferenceRoom.Id
                join day in db.Days on weekDayBookings.DayId equals day.Id
                join week in db.Weeks on weekDayBookings.WeekId equals week.Id

                where week.Id == weekChoice && weekDayBookings.ConferenceRoomId== roomChoiceId
                select new { WeekDayBooking = weekDayBookings, ConferenceRoom = conferenceRoom, Day = day, Week = week,  }
                );
                Console.Clear();
                var conferenceRoomForWeek = db.WeekDayBookings.Where(x => x.WeekId == weekChoice && x.ConferenceRoomId == roomList[roomChoiceId - 1].Id);
                var conferenceRoomForWeekToBook = db.WeekDayBookings.Where(x => x.WeekId == weekChoice && x.ConferenceRoomId == roomList[roomChoiceId - 1].Id).ToList();
                Console.WriteLine("ID".PadRight(padSmall) + "Room Name".PadRight(padLarge) + "Week Nr".PadRight(padSmall) + "Day".PadRight(padLarge) + "Status".PadRight(padSmall) + "Booked By".PadRight(padSmall) + "Company Name");
                foreach (var p in result)
                {
                    j++;
                    Console.WriteLine((j).ToString().PadRight(padSmall) + p.ConferenceRoom.Name.PadRight(padLarge) + p.Week.WeekNr.ToString().PadRight(padSmall) + p.Day.WeekDay.PadRight(padLarge) + (p.WeekDayBooking.Booked == null ? "Free" : "Booked").PadRight(padSmall) + (p.WeekDayBooking.BookerName == null ? "Unbooked" : p.WeekDayBooking.BookerName).PadRight(padSmall) + (p.WeekDayBooking.CompanyBookerName == null ? "Unbooked" : p.WeekDayBooking.CompanyBookerName));
                }
                Console.WriteLine("----------------------------------------------------------------------------------");
                Console.WriteLine("Choose id of the day you want to book or go back with [0] ");
                int bookNrId = TryNumber(conferenceRoomForWeek.Count(), 0);
                if (bookNrId == 0)
                {
                    Console.Clear();
                    Menus.ShowMenu("Main");
                }
                else if (conferenceRoomForWeekToBook[bookNrId - 1].Booked == true)
                {
                    Console.Clear();
                    Console.WriteLine("That day is already booked. Try again");
                    Thread.Sleep(2000);
                    Menus.ShowMenu("Main");
                }
                else
                {
                    Console.WriteLine("Input your name for booking: ");
                    string inputName = CheckStringInput();
                    Console.WriteLine("Input your companyname for booking: ");
                    string inputCompanyName = CheckStringInput();

                    conferenceRoomForWeekToBook[bookNrId - 1].BookerName = inputName;
                    conferenceRoomForWeekToBook[bookNrId - 1].CompanyBookerName = inputCompanyName;
                    conferenceRoomForWeekToBook[bookNrId - 1].Booked = true;
                    db.SaveChanges();
                    Console.Clear();
                }

            }
        }

        public static void GetMostPopularRoom()
        {      
            using (var db = new BookingAppContext())
            {
                var result = (
                from weekDayBookings in db.WeekDayBookings
                join conferenceRoom in db.ConferenceRooms on weekDayBookings.ConferenceRoomId equals conferenceRoom.Id                

                where weekDayBookings.Booked == true group conferenceRoom by conferenceRoom.Name into g
                select new
                {
                    ConferenceRoom = g.First().Name,
                    TotalBookings = g.Count(),
                }
                ).OrderByDescending(x => x.TotalBookings);

                foreach(var key in result)
                {
                    Console.WriteLine(key.ConferenceRoom + " is the most popular room with " + key.TotalBookings + " bookings");
                    break;
                }
            }
        }

        public static void GetMostPopularWeek()
        {
            using (var db = new BookingAppContext())
            {
                var result = (
                from weekDayBookings in db.WeekDayBookings 
                join week in db.Weeks on weekDayBookings.WeekId equals week.Id

                where weekDayBookings.Booked == true
                group weekDayBookings by weekDayBookings.WeekId into g
                select new
                {
                    WeekNr = g.First().Week.Id,
                    TotalBookings = g.Count(),
                }
                ).OrderByDescending(x => x.TotalBookings);

                foreach (var key in result)
                {
                    Console.WriteLine("Week nr: " + key.WeekNr + " is the most popular week at the moment with " + key.TotalBookings + " bookings");
                    break;
                }
            }
        }
        public static void GetNrOfUnbookedRooms()
        {
            using (var db = new BookingAppContext())
            {
                var result = (
                from weekDayBookings in db.WeekDayBookings
                join week in db.Weeks on weekDayBookings.WeekId equals week.Id

                where weekDayBookings.Booked == null                
                group weekDayBookings by weekDayBookings.Booked into g
                select new
                {   
                    TotalUnbooked = g.Count(),
                }
                );

                foreach (var key in result)
                {
                    Console.WriteLine("Total unbooked Days are : " + key.TotalUnbooked);
                    
                }
            }
        }
    }
}
