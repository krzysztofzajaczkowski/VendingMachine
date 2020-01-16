using System;
using System.Collections.Generic;
using System.Text;

namespace VendingMachine
{
    public abstract class BoardObject
    {
        public Point Position { get; set; }
        public char Symbol { get; set; }

        public BoardObject(int x, int y, char symbol)
        {
            Position = new Point(x, y);
            Symbol = symbol;
        }

        public abstract void Interact(BoardObject boardObject);

    }
}
