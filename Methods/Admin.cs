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
        }
    }
}
