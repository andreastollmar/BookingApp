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
        static readonly string _connString = "data source=.\\SQLEXPRESS; initial catalog = MyWebShop; persist security info = True; Integrated Security = True;";


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
                var roomList = db.ConferenceRooms.ToList();
                int i = 0;
                int j = 0;

                foreach (var room in roomList)
                {
                    i++;
                    Console.WriteLine(i + " " + room.Name + " room for " + room.NrOfSeatings);
                }
                Console.WriteLine("Choose Room that you want to book: ");
                int roomChoiceId = TryNumber(roomList.Count(), 1);

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
                Console.WriteLine("ID\tRoom Name\tWeek Nr\tDay\t\tStatus\tBooked By\tCompany Name");
                foreach (var p in result)
                {
                    j++;
                    Console.WriteLine(j + "\t" + p.ConferenceRoom.Name + "\t\t" + p.Week.WeekNr + "\t" + p.Day.WeekDay + "\t\t" + (p.WeekDayBooking.Booked == null ? "Free" : "Booked") + "\t" + (p.WeekDayBooking.BookerName == null ? "Unbooked" : p.WeekDayBooking.BookerName) + "\t" + (p.WeekDayBooking.CompanyBookerName == null ? "Unbooked" : p.WeekDayBooking.CompanyBookerName));
                }
                Console.WriteLine("----------------------------------------------------------------------------------");
                Console.WriteLine("Choose id of the day you want to book or go back with [0] ");
                int bookNrId = TryNumber(conferenceRoomForWeek.Count(), 0);
                if (bookNrId == 0)
                {
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

                }

            }
        }


    }
}
