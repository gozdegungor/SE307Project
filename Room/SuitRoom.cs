namespace project_307
{
    public class SuitRoom : Room
    {
        public SuitRoom(int id, bool isReservated, int size, double price) :
            base(id, isReservated, size, price){}

        public override string getType()
        {
            return "suit";
        }
    }
}