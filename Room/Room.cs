using System;

namespace project_307
{
    public abstract class Room
    {
        private int id;
        private bool isReservated;
        private int size;
        private double price;

        public Room(int id, bool isReservated, int size, double price)
        {
            this.id = id;
            this.isReservated = isReservated;
            this.size = size;
            this.price = price;
        }

        public int getId()
        {
            return this.id;
        }

        public int getSize()
        {
            return this.size;
        }

        public double getPrice()
        {
            return this.price;
        }

        abstract public string getType();

        public void display()
        {
            Console.WriteLine("\t Room id: " + id + " isReservated:" + isReservated + " size: " + size + " price: $" + price + " type: " + getType());
        }
        
        public string format(int hotelId)
        {
            return id + "_" + (isReservated ? "true" : "false") + "_" + size + "_" + price + "_" +
                   hotelId + "_" + getType();
        }
    }
}