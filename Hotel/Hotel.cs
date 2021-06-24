using System;
using System.Collections.Generic;

namespace project_307
{
    public abstract class Hotel
    {
        private int id;
        private string name;
        private int star;
        private Dictionary<int, Room> rooms;

        public Hotel(int id, string name, int star)
        {
            this.id = id;
            this.name = name;
            this.star = star;
            this.rooms = new Dictionary<int, Room>();
        }

        public int getId()
        {
            return this.id;
        }

        public Room getRoomById(int roomId)
        {
            return this.rooms[roomId];
        }

        public Dictionary<int, Room> getRooms()
        {
            return this.rooms;
        }

        public string getName()
        {
            return this.name;
        }

        public void addRoom(Room room)
        {
            rooms.Add(room.getId(), room);
        }

        abstract public string getType();

        public void display(bool displayRooms = true)
        {
            Console.WriteLine("Hotel id:" + id + " name: " + name + " star:" + star + " type: " + getType());

            if (displayRooms)
            {
                foreach(KeyValuePair<int, Room> roomEntry in this.rooms)
                {
                    roomEntry.Value.display();
                }    
            }
        }
        
        public string format()
        {
            return id + "_" + name + "_" + star + "_" + getType();
        }
    }
}