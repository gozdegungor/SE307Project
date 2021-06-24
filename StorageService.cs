using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace project_307
{
    public class StorageService
    {
        private static StorageService instance = null;
        
        private Dictionary<int, Admin> admins;
        private Dictionary<int, Resident> residents;
        private Dictionary<int, Hotel> hotels;
        private Dictionary<int, Reservation> reservations;

        private FileService fs;
        private HelperService helper;
        
        private StorageService()
        {
            this.admins = new Dictionary<int, Admin>();
            this.residents = new Dictionary<int, Resident>();
            this.hotels = new Dictionary<int, Hotel>();
            this.reservations = new Dictionary<int, Reservation>();
            
            this.fs = FileService.getInstance();
            helper = HelperService.getInstance();

            this.updateHashes();
        }

        public static StorageService getInstance()
        {
            if (instance == null)
            {
                instance = new StorageService();
            }

            return instance;
        }

        public Dictionary<int, Admin> getAdmins()
        {
            return admins;
        }

        public Dictionary<int, Resident> getResidents()
        {
            return residents;
        }

        public Hotel getHotelById(int hotelId)
        {
            return this.hotels[hotelId];
        }

        public void updateHashes()
        {
            this.admins = this.fs.readAdminsFile();
            this.residents = this.fs.readResidentsFile();
            
            this.hotels = this.fs.readHotelsFile();
            Dictionary<int, ArrayList> hotelRooms = this.fs.readRoomsFile();
            foreach(KeyValuePair<int, ArrayList> hrEntry in hotelRooms)
            {
                Hotel hotel = this.hotels[hrEntry.Key];
                foreach(Room room in hrEntry.Value )
                {
                    hotel.addRoom(room);
                }
            }
            this.reservations = this.fs.readReservartionsFile();
        }

        public void displayHotels()
        {
            Console.WriteLine("Displaying hotels");
            foreach(KeyValuePair<int, Hotel> hEntry in this.hotels)
            {
                hEntry.Value.display();
            }
        }

        public void displayReservationsInHotel(int hotelId)
        {
            Hotel hotel;
            if (!hotels.TryGetValue(hotelId, out hotel))
            {
                Console.WriteLine("Hotel not found");
                return;
            }

            int reservationCount = 0;
            foreach(KeyValuePair<int, Reservation> rEntry in reservations)
            {
                Reservation reservation = rEntry.Value;
                if (reservation.getHotelId() == hotel.getId())
                {
                    reservationCount++;
                    reservation.display();
                }
            }

            if (reservationCount == 0)
            {
                Console.WriteLine("No reservations in this hotel");
            }
        }

        public void displayAllCustomersInfo()
        {
            foreach(KeyValuePair<int, Resident> rEntry in residents)
            {
                Resident resident = rEntry.Value;
                resident.display();
                displayReservationsOfResident(resident.getId());
            }
        }

        public void displayReservationsOfResident(int residentId)
        {
            Resident resident;
            if (!residents.TryGetValue(residentId, out resident))
            {
                Console.WriteLine("Resident not found");
                return;
            }

            int reservationCount = 0;
            foreach(KeyValuePair<int, Reservation> rEntry in reservations)
            {
                Reservation reservation = rEntry.Value;
                if (reservation.getResidentId() == resident.getId())
                {
                    reservationCount++;
                    reservation.display();
                }
            }

            if (reservationCount == 0)
            {
                Console.WriteLine("No reservations in this hotel");
            }
        }

        public void addHotel()
        {
            int latestHotelId = 0;
            foreach(KeyValuePair<int, Hotel> hEntry in this.hotels)
            {
                if (latestHotelId < hEntry.Value.getId())
                {
                    latestHotelId = hEntry.Value.getId();
                }
            }
            
            Hotel newHotel = helper.gatherHotelInformation(latestHotelId + 1);
            hotels.Add(newHotel.getId(), newHotel);
            
            fs.updateHotelsFile(hotels);
        }

        public void updateHotel()
        {
            do
            {
                int hotelId = helper.getIntFromUser("Enter hotel id");

                if (hotelId > 0)
                {
                    Hotel oldHotel = this.hotels[hotelId];
                    Hotel newHotel = helper.gatherHotelInformation(hotelId);
                    foreach(KeyValuePair<int, Room> rEntry in oldHotel.getRooms())
                    {
                        newHotel.addRoom(rEntry.Value);
                    }
            
                    hotels[hotelId] = newHotel;
            
                    fs.updateHotelsFile(hotels);
                    break;
                }
            } while (true);
        }

        public void addRoom()
        {
            do
            {
                int hotelId = helper.getIntFromUser("Enter hotel id");

                if (hotelId > 0)
                {
                    int latestRoomId = 0;
                    foreach(KeyValuePair<int, Hotel> hEntry in this.hotels)
                    {
                        Dictionary<int, Room> rooms = hEntry.Value.getRooms();
                        foreach (KeyValuePair<int, Room> rEntry in rooms)
                        {
                            if (latestRoomId < rEntry.Value.getId())
                            {
                                latestRoomId = rEntry.Value.getId();
                            }
                        }
                    }

                    Room room = helper.gatherRoomInformation(latestRoomId + 1);
                    this.hotels[hotelId].addRoom(room);
            
                    fs.updateRoomsFile(hotels);
                    break;
                }
            } while (true);
        }

        public void deleteHotel()
        {
            do
            {
                int hotelId = helper.getIntFromUser("Enter hotel id");

                if (hotelId > 0)
                {
                    this.hotels.Remove(hotelId);
            
                    fs.updateHotelsFile(hotels);
                    fs.updateRoomsFile(hotels);
                    break;
                }
            } while (true);
        }

        public void searchHotel()
        {
            do
            {
                int sType = helper.getIntFromUser("Select one (1-SearchByType o-o 2-SearchByName)");
                switch (sType)
                {
                    case 1:
                        do
                        {
                            int hType = helper.getIntFromUser("Select one (1-WinterHotel o-o 2-SummerHotel)");
                            switch (hType)
                            {
                                case 1:
                                    foreach(KeyValuePair<int, Hotel> hEntry in this.hotels)
                                    {
                                        if (hEntry.Value.getType() == "winter")
                                        {
                                            hEntry.Value.display();
                                        }
                                    }
                                    return;
                                case 2:
                                    foreach(KeyValuePair<int, Hotel> hEntry in this.hotels)
                                    {
                                        if (hEntry.Value.getType() == "summer")
                                        {
                                            hEntry.Value.display();
                                        }
                                    }
                                    return;
                                default:
                                    Console.WriteLine("Invalid hotel type");
                                    break;
                            }
                        } while (true);
                    case 2:
                        Console.WriteLine("Enter hotel name to search");
                        string name = Console.ReadLine();
                        foreach(KeyValuePair<int, Hotel> hEntry in this.hotels)
                        {
                            if (hEntry.Value.getName().Contains(name))
                            {
                                hEntry.Value.display();
                            }
                        }
                        return;
                }
            } while (true);
        }

        public void addReservation(int residentId)
        {
            Reservation reservation = helper.gatherReservationInformation(this.reservations.Count + 1, residentId);
            this.reservations.Add(reservation.getId(), reservation);
            updateReservation(reservation.getId());
        }

        public void updateReservation(int reservationId = -1)
        {
            do
            {
                if (reservationId == -1)
                {
                    reservationId = helper.getIntFromUser("Enter reservation id");    
                }

                if (reservationId > 0)
                {
                    Reservation reservation = this.reservations[reservationId];
            
                    Console.WriteLine("Enter start date dd/mm/yyy");
                    string startDate = Console.ReadLine();
                    reservation.setStartDate(startDate);

                    int dayCount;
                    do
                    {
                        dayCount = helper.getIntFromUser("Enter day count");
                    } while (dayCount < 0);
                    reservation.setDayCount(dayCount);
            
                    fs.updateReservationsFile(reservations);
                    break;
                }
            } while (true);
        }

        public void searchRoomsToReserve()
        {
            int minSize;
            do
            {
                minSize = helper.getIntFromUser("Enter min size");
            } while (minSize < 0);
            
            int maxPrice;
            do
            {
                maxPrice = helper.getIntFromUser("Enter max price");
            } while (maxPrice < 0);

            foreach(KeyValuePair<int, Hotel> hEntry in this.hotels)
            {
                Hotel hotel = hEntry.Value;
                hotel.display(false);

                int roomFoundCount = 0;
                foreach (KeyValuePair<int, Room> rEntry in hotel.getRooms())
                {
                    Room room = rEntry.Value;

                    if (room.getSize() >= minSize && room.getPrice() <= maxPrice)
                    {
                        roomFoundCount++;
                        room.display();
                    }
                }

                if (roomFoundCount == 0)
                {
                    Console.WriteLine("No rooms found");
                }
            }
        }

    }
}