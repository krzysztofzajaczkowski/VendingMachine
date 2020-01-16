using System;
using System.Collections.Generic;

namespace VendingMachine
{
    class Program
    {
        static void Main(string[] args)
        {
            char[,] tab = new char[10,10];
            for (int x = 0; x < 10; x++)
            {
                for (int y = 0; y < 10; y++)
                {
                    tab[x,y] = ' ';
                }
            }
            List<BoardObject> boardObjects = new List<BoardObject>();
            Player player = new Player();
            VendingMachine vendingMachine = new VendingMachine(5,5);
            boardObjects.Add(player);
            boardObjects.Add(vendingMachine);
            ConsoleKey key;
            Point playerPosition = player.Position;
            Point newPoint;
            int dX = 0, dY = 0;
            bool interact = false;
            foreach (var boardObject in boardObjects)
            {
                tab[boardObject.Position.X, boardObject.Position.Y] = boardObject.Symbol;
            } 
            while (true)
            {
                Console.Clear();
                Console.WriteLine("--------------------");
                for (int y = 0; y < 10; y++)
                {
                    Console.Write("|");
                    for (int x = 0; x < 10; x++)
                    {
                        Console.Write(tab[x,y] + " ");
                    }
                    Console.Write("|\n");
                }
                Console.WriteLine(" --------------------");
                interact = false;
                key = Console.ReadKey().Key;
                if (key == ConsoleKey.UpArrow)
                {
                    dX = 0;
                    dY = -1;
                }
                if (key == ConsoleKey.RightArrow)
                {
                    dX = 1;
                    dY = 0;
                }
                if (key == ConsoleKey.DownArrow)
                {
                    dX = 0;
                    dY = 1;
                }
                if (key == ConsoleKey.LeftArrow)
                {
                    dX = -1;
                    dY = 0;
                }
                newPoint = new Point(playerPosition.X + dX, playerPosition.Y + dY);
                //Console.WriteLine("X: {0} Y: {1}", newPoint.X, newPoint.Y);
                if (newPoint.IsViable)
                {
                    foreach (var boardObject in boardObjects)
                    {
                        if (boardObject.Position.Equals(newPoint))
                        {
                            interact = true;
                            boardObject.Interact(player);
                        }
                    }
                    if (!interact)
                    {
                        tab[player.Position.X, player.Position.Y] = ' ';
                        player.Move(newPoint);
                        playerPosition = newPoint;
                        tab[player.Position.X, player.Position.Y] = player.Symbol;
                    }
                }
                //Console.WriteLine(Console.ReadKey().Key == ConsoleKey.UpArrow);
            }
        }
    }
}
