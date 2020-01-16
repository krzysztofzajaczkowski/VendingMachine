using System;
using System.Collections.Generic;
using System.Text;

namespace VendingMachine
{
    public class Point
    {
        public bool IsViable { 
            get
            {
                if (X >= 0 && X < 10 && Y >= 0 && Y < 10)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public int X { get; set; }
        public int Y { get; set; }
        public Point()
        {
            X = 0;
            Y = 0;
        }
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }
        public bool Equals(Point point)
        {
            if (X == point.X && Y == point.Y)
            {
                return true;
            }
            return false;
        }
    }
}
