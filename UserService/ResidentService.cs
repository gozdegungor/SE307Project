using System;
using System.Collections.Generic;

namespace project_307
{
    public class ResidentService : UserService
    {
        private StorageService ss;
        private HelperService helper;
        private Resident loggedUser = null;

        public ResidentService()
        {
            this.ss = StorageService.getInstance();
            this.helper = HelperService.getInstance();
        }

        public void login(string username, string password)
        {
            foreach (KeyValuePair<int, Resident> entry in ss.getResidents())
            {
                if (entry.Value.tryLogin(username, password))
                {
                    loggedUser = entry.Value;
                    break;
                }
            }
            
            if (loggedUser == null)
            {
                Console.WriteLine("Can not login");
                return;   
            }
            displayMenu();
        }

        public void displayMenu()
        {
            do
            {
                Console.WriteLine("o-o-o-o RESIDENT MENU o-o-o-o");
                Console.WriteLine("1- Display All Hotels");
                Console.WriteLine("2- New Reservation");
                Console.WriteLine("3- Display My Reservations");
                Console.WriteLine("4- Update Reservation");
                Console.WriteLine("5- Search Hotel");
                Console.WriteLine("6- Search Room");
                Console.WriteLine("99- Logout");
                Console.WriteLine("o-o-o-o-o-o-o-o-o-o-o-o-o");

                int choice = helper.getIntFromUser("Enter your choice");
                switch (choice)
                {
                    case 1:
                        ss.displayHotels();
                        break;
                    case 2:
                        ss.addReservation(loggedUser.getId());
                        break;
                    case 3:
                        ss.displayReservationsOfResident(loggedUser.getId());
                        break;
                    case 4:
                        ss.updateReservation();
                        break;
                    case 5:
                        ss.searchHotel();
                        break;
                    case 6:
                        ss.searchRoomsToReserve();
                        break;
                    case 99:
                        return;
                    default:
                        Console.WriteLine("Invalid choice");
                        break;
                }
            } while (true);
        }
    }
}