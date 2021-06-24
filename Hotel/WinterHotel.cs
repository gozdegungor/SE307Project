namespace project_307
{
    public class WinterHotel : Hotel
    {
        public WinterHotel(int id, string name, int star) : 
            base(id, name, star){}

        override public string getType()
        {
            return "winter";
        }
    }
}