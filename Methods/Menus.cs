using BookingApp.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Methods
{
    public class Menus
    {
        enum MainMenu
        {
            Browse_Conferencerooms = 1,
            Admin,
            Exit = 0
        }

        enum Admin
        {
            Manage_Rooms = 1,
            See_Queries,
            Return = 0
        }
        enum AdminRooms
        {
            Add_Room = 1,
            Edit_Room,
            Return = 0
        }
        enum SeeQueries
        {
            Most_Popular_Room = 1,
            Nr_Of_Unbooked_Rooms,
            Most_Popular_Week,
            Return = 0
        }
        
        enum BrowseRooms
        {
            List_Rooms = 1,
            Book_Room,
            Return = 0
        }

        public static void ShowMenu(string value)
        {
            bool goMain = true;
            bool adminStart = true;
            bool adminRoom = true;
            bool adminQueries = true;
            if (value == "Main")
            {
                while (goMain)
                {
                    foreach (int i in Enum.GetValues(typeof(MainMenu)))
                    {
                        Console.WriteLine($"{i}. {Enum.GetName(typeof(MainMenu), i).Replace("_", " ")}");
                    }

                    int nr;
                    MainMenu menu = (MainMenu)99; //Default
                    if (int.TryParse(Console.ReadKey(true).KeyChar.ToString(), out nr))
                    {
                        menu = (MainMenu)nr;
                        Console.Clear();
                    }
                    switch (menu)
                    {
                        case MainMenu.Browse_Conferencerooms:
                            Helpers.BookConferenceRoom();
                            goMain = false;
                            break;
                        case MainMenu.Admin:
                            ShowMenu("Admin");
                            goMain = false;
                            break;
                        case MainMenu.Exit:
                            Console.WriteLine("Thank you come again"); ;
                            goMain = false;
                            break;

                    }
                }
            }

            if (value == "Admin")
            {
                while (adminStart)
                {
                    foreach (int i in Enum.GetValues(typeof(Admin)))
                    {
                        Console.WriteLine($"{i}. {Enum.GetName(typeof(Admin), i).Replace("_", " ")}");
                    }

                    int nr;
                    Admin menu = (Admin)99; //Default
                    if (int.TryParse(Console.ReadKey(true).KeyChar.ToString(), out nr))
                    {
                        menu = (Admin)nr;
                        Console.Clear();
                    }
                    switch (menu)
                    {
                        case Admin.Manage_Rooms:
                            ShowMenu("AdminRooms");
                            adminStart = false;
                            break;
                        case Admin.See_Queries:
                            ShowMenu("AdminQueries");
                            adminStart = false;
                            break;
                        case Admin.Return:
                            ShowMenu("Main");
                            adminStart = false;
                            break;

                    }
                }
            }
            if (value == "AdminRooms")
            {
                while (adminRoom)
                {
                    foreach (int i in Enum.GetValues(typeof(AdminRooms)))
                    {
                        Console.WriteLine($"{i}. {Enum.GetName(typeof(AdminRooms), i).Replace("_", " ")}");
                    }

                    int nr;
                    AdminRooms menu = (AdminRooms)99; //Default
                    if (int.TryParse(Console.ReadKey(true).KeyChar.ToString(), out nr))
                    {
                        menu = (AdminRooms)nr;
                        Console.Clear();
                    }
                    switch (menu)
                    {
                        case AdminRooms.Add_Room:
                            Methods.Admin.AddConferenceRoom();
                            adminRoom = false;
                            break;
                        case AdminRooms.Edit_Room:
                            Methods.Admin.EditRoom();
                            Console.Clear();
                            adminRoom = false;
                            break;
                        case AdminRooms.Return:
                            ShowMenu("Main");
                            adminRoom = false;
                            break;

                    }
                }
            }
            if(value == "AdminQueries")
            {
                while (adminQueries)
                {
                    foreach (int i in Enum.GetValues(typeof(SeeQueries)))
                    {
                        Console.WriteLine($"{i}. {Enum.GetName(typeof(SeeQueries), i).Replace("_", " ")}");
                    }

                    int nr;
                    SeeQueries menu = (SeeQueries)99; //Default
                    if (int.TryParse(Console.ReadKey(true).KeyChar.ToString(), out nr))
                    {
                        menu = (SeeQueries)nr;
                        Console.Clear();
                    }
                    switch (menu)
                    {
                        case SeeQueries.Most_Popular_Room:
                            Helpers.GetMostPopularRoom();                            
                            break;
                        case SeeQueries.Most_Popular_Week:
                            Helpers.GetMostPopularWeek();                            
                            break;
                        case SeeQueries.Nr_Of_Unbooked_Rooms:
                            Helpers.GetNrOfUnbookedRooms();
                            break;                            
                        case SeeQueries.Return:
                            adminQueries = false;
                            ShowMenu("Main");
                            break;

                    }
                }

            }
            
        }

    }
}
