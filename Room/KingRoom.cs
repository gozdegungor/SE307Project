namespace project_307
{
    public class KingRoom : Room
    {
        public KingRoom(int id, bool isReservated, int size, double price) :
            base(id, isReservated, size, price){}

        public override string getType()
        {
            return "king";
        }
    }
}