using System;
using System.Drawing;

namespace RentCenter.Window
{
    public class Rent : IComparable<Rent>
    {
        public string Renter { get; set; }
        public DateTime RentStart { get; set; }
        public DateTime RentEnd { get; set; }
        public Color Color { get; set; }

        public Rent(string renter, DateTime start, DateTime end, Color color)
        {
            Renter = renter;
            RentStart = start;
            RentEnd = end;
            Color = color;
        }

        public int CompareTo(Rent other)
        {
            if (RentEnd.CompareTo(other.RentStart) == -1) return -1;
            if (RentStart.CompareTo(other.RentEnd) == 1) return 1;
            return 0;
        }
    }
}
