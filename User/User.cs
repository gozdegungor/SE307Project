using System;

namespace project_307
{
    public abstract class User
    {
        protected int id;
        protected string username;
        private string passsword;

        public User(int id, string username, string passsword)
        {
            this.id = id;
            this.username = username;
            this.passsword = passsword;
        }

        public int getId()
        {
            return this.id;
        }

        public bool tryLogin(string username, string password)
        {
            return (this.username == username) && (this.passsword == password);
        }
        
        abstract public string getType();

        public string format()
        {
            return id + "_" + username + "_" + passsword;
        }
    }
}