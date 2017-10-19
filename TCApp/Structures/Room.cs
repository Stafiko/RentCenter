using System.Collections.Generic;

namespace RentCenter.Window
{
    public class Room
    {
        public bool IsArended { get; set; }
        public int Price { get; }
        public List<Rent> Rents { get; set; }
        public int Area { get; }
        public int Cost { get; }
        public int Index { get; }
        public int Floor { get; }

        public Room(
            int floor, int index, 
            int area, int price, 
            bool arend = false)
        {
            Rents = new List<Rent>();
            Area = area;
            Price = price;
            Cost = area * price;
            IsArended = arend;
            Floor = floor;
            Index = index;
        }
    }
}
