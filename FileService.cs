using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace project_307
{
    public class FileService
    {
        private static FileService instance = null;

        private HelperService helper;
        
        private FileService()
        {
            helper = HelperService.getInstance();
        }

        public static FileService getInstance()
        {
            if (instance == null)
            {
                instance = new FileService();
            }

            return instance;
        }

        public Dictionary<int, Admin> readAdminsFile()
        {
            Dictionary<int, Admin> admins = new Dictionary<int, Admin>();
            
            string path = Path.Combine(Environment.CurrentDirectory, "datas/admins.txt");
            using (StreamReader sr = File.OpenText(path))
            {
                string s = "";
                while ((s = sr.ReadLine()) != null)
                {
                    string[] datas = s.Split('_');

                    Admin admin = new Admin(helper.str2int(datas[0]), datas[1], datas[2]);
                    admins.Add(admin.getId(), admin);
                }
            }

            return admins;
        }

        public Dictionary<int, Resident> readResidentsFile()
        {
            Dictionary<int, Resident> residents = new Dictionary<int, Resident>();
            
            string path = Path.Combine(Environment.CurrentDirectory, "datas/residents.txt");
            using (StreamReader sr = File.OpenText(path))
            {
                string s = "";
                while ((s = sr.ReadLine()) != null)
                {
                    string[] datas = s.Split('_');

                    Resident resident = new Resident(helper.str2int(datas[0]), datas[1], datas[2]);
                    residents.Add(resident.getId(), resident);
                }
            }

            return residents;
        }

        public Dictionary<int, Hotel> readHotelsFile()
        {
            Dictionary<int, Hotel> hotels = new Dictionary<int, Hotel>();
            
            string path = Path.Combine(Environment.CurrentDirectory, "datas/hotels.txt");
            using (StreamReader sr = File.OpenText(path))
            {
                string s = "";
                while ((s = sr.ReadLine()) != null)
                {
                    string[] datas = s.Split('_');

                    Hotel hotel;
                    if (datas[3] == "winter")
                    {
                        hotel = new WinterHotel(helper.str2int(datas[0]), datas[1], helper.str2int(datas[2]));    
                    }
                    else if(datas[3] == "summer")
                    {
                        hotel = new SummerHotel(helper.str2int(datas[0]), datas[1], helper.str2int(datas[2]));
                    }
                    else
                    {
                        continue;
                    }
                    
                    hotels.Add(hotel.getId(), hotel);
                }
            }

            return hotels;
        }

        public Dictionary<int, ArrayList> readRoomsFile()
        {
            Dictionary<int, ArrayList> hotelRooms = new Dictionary<int, ArrayList>();
            
            string path = Path.Combine(Environment.CurrentDirectory, "datas/rooms.txt");
            using (StreamReader sr = File.OpenText(path))
            {
                string s = "";
                while ((s = sr.ReadLine()) != null)
                {
                    string[] datas = s.Split('_');

                    ArrayList rooms;
                    if (!hotelRooms.TryGetValue(helper.str2int(datas[4]), out rooms))
                    {
                        rooms = new ArrayList();
                        hotelRooms.Add(helper.str2int(datas[4]), rooms);
                    }

                    Room room;
                    if (datas[5] == "king")
                    {
                        room = new KingRoom(helper.str2int(datas[0]), helper.str2bool(datas[1]), helper.str2int(datas[2]), helper.str2int(datas[3]));    
                    }
                    else if(datas[5] == "suit")
                    {
                        room = new SuitRoom(helper.str2int(datas[0]), helper.str2bool(datas[1]), helper.str2int(datas[2]), helper.str2int(datas[3]));
                    }
                    else if(datas[5] == "regular")
                    {
                        room = new RegularRoom(helper.str2int(datas[0]), helper.str2bool(datas[1]), helper.str2int(datas[2]), helper.str2int(datas[3]));
                    }
                    else
                    {
                        continue;
                    }

                    rooms.Add(room);
                }
            }

            return hotelRooms;
        }
        
        public Dictionary<int, Reservation> readReservartionsFile()
        {
            Dictionary<int, Reservation> reservations = new Dictionary<int, Reservation>();
            
            string path = Path.Combine(Environment.CurrentDirectory, "datas/reservations.txt");
            using (StreamReader sr = File.OpenText(path))
            {
                string s = "";
                while ((s = sr.ReadLine()) != null)
                {
                    string[] datas = s.Split('_');

                    Reservation reservation = new Reservation(helper.str2int(datas[0]), helper.str2int(datas[1]), helper.str2int(datas[2]), helper.str2int(datas[3]));
                    reservation.setStartDate(datas[4]);
                    reservation.setDayCount(helper.str2int(datas[5]));
                    reservation.setPaid(helper.str2bool(datas[6]));
                    reservations.Add(reservation.getId(), reservation);
                }
            }

            return reservations;
        }

        public void updateAdminsFile(Dictionary<int, Admin> admins)
        {
            string path = Path.Combine(Environment.CurrentDirectory, "datas/admins.txt");

            string outTxt = "";
            foreach(KeyValuePair<int, Admin> entry in admins)
            {
                outTxt += entry.Value.format() + "\n";
            }
            
            System.IO.File.WriteAllText(path,outTxt.Trim());
        }

        public void updateResidentsFile(Dictionary<int, Resident> residents)
        {
            string path = Path.Combine(Environment.CurrentDirectory, "datas/residents.txt");

            string outTxt = "";
            foreach(KeyValuePair<int, Resident> entry in residents)
            {
                outTxt += entry.Value.format() + "\n";
            }
            
            System.IO.File.WriteAllText(path,outTxt.Trim());
        }

        public void updateHotelsFile(Dictionary<int, Hotel> hotels)
        {
            string path = Path.Combine(Environment.CurrentDirectory, "datas/hotels.txt");

            string outTxt = "";
            foreach(KeyValuePair<int, Hotel> entry in hotels)
            {
                outTxt += entry.Value.format() + "\n";
            }
            
            System.IO.File.WriteAllText(path,outTxt.Trim());
        }

        public void updateRoomsFile(Dictionary<int, Hotel> hotels)
        {
            string path = Path.Combine(Environment.CurrentDirectory, "datas/rooms.txt");

            string outTxt = "";
            foreach(KeyValuePair<int, Hotel> entry in hotels)
            {
                foreach (KeyValuePair<int, Room> rEntry in entry.Value.getRooms())
                {
                    outTxt += rEntry.Value.format(entry.Value.getId()) + "\n";    
                }
            }
            
            System.IO.File.WriteAllText(path,outTxt.Trim());
        }

        public void updateReservationsFile(Dictionary<int, Reservation> reservations)
        {
            string path = Path.Combine(Environment.CurrentDirectory, "datas/reservations.txt");

            string outTxt = "";
            foreach(KeyValuePair<int, Reservation> entry in reservations)
            {
                outTxt += entry.Value.format() + "\n";
            }
            
            System.IO.File.WriteAllText(path,outTxt.Trim());
        }
    }
}