namespace project_307
{
    public class Admin : User
    {
        public Admin(int id, string username, string password) : 
            base(id, username, password){}

        override public string getType()
        {
            return "admin";
        }
    }
}