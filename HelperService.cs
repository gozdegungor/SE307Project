using System;
using System.Collections;
using System.IO;

namespace project_307
{
    public class HelperService
    {
        private static HelperService instance = null;

        private HelperService()
        {
        }

        public static HelperService getInstance()
        {
            if (instance == null)
            {
                instance = new HelperService();
            }

            return instance;
        }

        public int str2int(string str)
        {
            try
            {
                return Convert.ToInt32(str);
            }
            catch (Exception e)
            {
                return -1;
            }
        }

        public double str2double(string str)
        {
            try
            {
                return Convert.ToDouble(str);
            }
            catch (Exception e)
            {
                return -1;
            }
        }

        public bool str2bool(string str)
        {
            try
            {
                return str == "true";
            }
            catch (Exception e)
            {
                return false;
            }
            
        }

        public int getIntFromUser(string message)
        {
            do
            {
                Console.WriteLine(message);
                string inp = Console.ReadLine();
                int num = str2int(inp);
                if (num != -1)
                {
                    return num;
                }

                Console.WriteLine("Invalid input");
            } while (true);

        }

        public double getDoubleFromUser(string message)
        {
            do
            {
                Console.WriteLine(message);
                string inp = Console.ReadLine();
                double num = str2double(inp);
                if (num != -1)
                {
                    return num;
                }

                Console.WriteLine("Invalid input");
            } while (true);

        }

        public Hotel gatherHotelInformation(int hotelId)
        {
            Console.WriteLine("Enter hotel name");
            string name = Console.ReadLine();

            int star;
            do
            {
                star = getIntFromUser("Enter star");
            } while (star < 0 || star > 5);
            
            int type;
            do
            {
                type = getIntFromUser("Select one (1-WinterHotel o-o 2-SummerHotel)");
            } while (type != 1 && type != 2);

            Hotel hotel = null;
            switch (type)
            {
             case 1:
                 hotel = new WinterHotel(hotelId, name, star);
                 break;
             case 2:
                 hotel = new SummerHotel(hotelId, name, star);
                 break;
            }

            return hotel;
        }

        public Room gatherRoomInformation(int roomId)
        {
            int size;
            do
            {
                size = getIntFromUser("Enter size");
            } while (size < 0);

            double price;
            do
            {
                price = getDoubleFromUser("Enter price");
            } while (price < 0);
            
            int type;
            do
            {
                type = getIntFromUser("Select one (1-KingRoom o-o 2-SuitRoom o-o 3-RegularRoom)");
            } while (type != 1 && type != 2 && type != 3);

            Room room = null;
            switch (type)
            {
                case 1:
                    room = new KingRoom(roomId, false, size, price);
                    break;
                case 2:
                    room = new SuitRoom(roomId, false, size, price);
                    break;
                case 3:
                    room = new RegularRoom(roomId, false, size, price);
                    break;
            }

            return room;
        }

        public Reservation gatherReservationInformation(int reservationId, int residentId)
        {
            int hotelId;
            do
            {
                hotelId = getIntFromUser("Enter hotel id");
            } while (hotelId < 0);
            
            int roomId;
            do
            {
                roomId = getIntFromUser("Enter room id");
            } while (roomId < 0);

            return new Reservation(reservationId, residentId, hotelId, roomId);
        }
    }
}