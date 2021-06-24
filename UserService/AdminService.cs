using System;
using System.Collections.Generic;

namespace project_307
{
    public class AdminService : UserService
    {
        private StorageService ss;
        private HelperService helper;
        private Admin loggedUser = null;

        public AdminService()
        {
            this.ss = StorageService.getInstance();
            this.helper = HelperService.getInstance();
        }

        public void login(string username, string password)
        {
            foreach (KeyValuePair<int, Admin> entry in ss.getAdmins())
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
                Console.WriteLine("o-o-o-o ADMIN MENU o-o-o-o");
                Console.WriteLine("1- Display All Hotels");
                Console.WriteLine("2- Add New Hotel");
                Console.WriteLine("3- Remove Hotel");
                Console.WriteLine("4- Search Hotel");
                Console.WriteLine("5- Update Hotel");
                Console.WriteLine("6- Add New Room");
                Console.WriteLine("7- Display All Reservations");
                Console.WriteLine("99- Logout");
                Console.WriteLine("o-o-o-o-o-o-o-o-o-o-o-o-o");

                int choice = helper.getIntFromUser("Enter your choice");
                switch (choice)
                {
                    case 1:
                        ss.displayHotels();
                        break;
                    case 2:
                        ss.addHotel();
                        break;
                    case 3:
                        ss.deleteHotel();
                        break;
                    case 4:
                        ss.searchHotel();
                        break;
                    case 5:
                        ss.updateHotel();
                        break;
                    case 6:
                        ss.addRoom();
                        break;
                    case 7:
                        ss.displayAllCustomersInfo();
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