using System;
using System.Threading.Channels;

namespace project_307
{
    public class Start
    {
        private static AdminService aS;
        private static ResidentService rS;
        private static HelperService helper;
        
        
        static void Main()
        {
            aS = new AdminService();
            rS = new ResidentService();

            helper = HelperService.getInstance();
            
            displayMenu();
        }

        static void displayMenu()
        {
            do
            {
                Console.WriteLine("o-o-o-o MAIN MENU o-o-o-o");
                Console.WriteLine("1- Admin login");
                Console.WriteLine("2- Resident login");
                Console.WriteLine("99- Exit");
                Console.WriteLine("o-o-o-o-o-o-o-o-o-o-o-o-o");

                int choice = helper.getIntFromUser("Enter your choice");
                switch (choice)
                {
                    case 1:
                    case 2:
                        Console.WriteLine("Enter username");
                        string username = Console.ReadLine();
                        
                        Console.WriteLine("Enter password");
                        string password = Console.ReadLine();

                        if (choice == 1)
                        {
                            aS.login(username, password);
                        }

                        if (choice == 2)
                        {
                            rS.login(username, password);
                        }

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