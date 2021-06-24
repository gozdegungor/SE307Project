namespace project_307
{
    public class SummerHotel : Hotel
    {
        public SummerHotel(int id, string name, int star) : 
            base(id, name, star){}

        override public string getType()
        {
            return "summer";
        }
    }
}