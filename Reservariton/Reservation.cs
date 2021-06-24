using System;

namespace project_307
{
    public class Reservation
    {
        private int id;
        private int residentId;
        private int hotelId;
        private int roomId;
        private string startDate;
        private int dayCount;
        private bool isPaid;

        public Reservation(int id, int residentId, int hotelId, int roomId)
        {
            this.id = id;
            this.residentId = residentId;
            this.hotelId = hotelId;
            this.roomId = roomId;
            this.startDate = "";
            this.dayCount = 0;
            this.isPaid = false;
        }

        public int getId()
        {
            return this.id;
        }

        public int getResidentId()
        {
            return this.residentId;
        }

        public int getHotelId()
        {
            return this.hotelId;
        }

        public void setStartDate(string startDate)
        {
            this.startDate = startDate;
        }
        
        public void setDayCount(int dayCount)
        {
            this.dayCount = dayCount;
        }
        
        public void setPaid(bool isPaid)
        {
            this.isPaid = isPaid;
        }

        public double calculateCharge()
        {
            Hotel hotel = StorageService.getInstance().getHotelById(hotelId);
            Room room = hotel.getRoomById(roomId);

            return dayCount * room.getPrice();
        }

        public void display()
        {
            Console.WriteLine("Reservation");
            Console.WriteLine("\t id: " + id);
            Console.WriteLine("\t residentId: " + residentId);
            Console.WriteLine("\t hotelId: " + hotelId);
            Console.WriteLine("\t roomId: " + roomId);
            Console.WriteLine("\t startDate: " + startDate);
            Console.WriteLine("\t dayCount: " + dayCount);
            Console.WriteLine("\t calculatedCharge: $" + calculateCharge());
            Console.WriteLine("\t isPaid: " + isPaid);
        }
        
        public string format()
        {
            return id + "_" + residentId + "_" + hotelId + "_" + roomId + "_" + startDate + "_" +
                   dayCount + "_" + (isPaid ? "true" : "false");
        }
    }
}