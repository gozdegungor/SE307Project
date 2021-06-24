using System;

namespace project_307
{
    public class Resident : User
    {
        public Resident(int id, string username, string password) : 
            base(id, username, password){}

        override public string getType()
        {
            return "resident";
        }

        public void display()
        {
            Console.WriteLine("Resident id: " + id + " username: " + username);
        }
    }
}