namespace project_307
{
    public class RegularRoom : Room
    {
        public RegularRoom(int id, bool isReservated, int size, double price) :
            base(id, isReservated, size, price){}

        public override string getType()
        {
            return "regular";
        }
    }
}