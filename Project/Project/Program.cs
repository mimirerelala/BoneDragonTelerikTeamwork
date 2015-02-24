using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            Console.WindowWidth = gameWidth;
            Console.BufferWidth = gameWidth;
            Console.WindowHeight = gameHeight;
            Console.BufferHeight = Console.WindowHeight + 1;
            Console.OutputEncoding = Encoding.GetEncoding(1252);

            while (true)
            {
                PrintBorders();
                PrintInfoPannel();
            }
            
            
        }

        static void PrintAtPosition(int row, int col, object data) // Printing at position
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition(col, row);
            Console.Write(data);    
        }

        const int gameWidth = 130;
        const int gameHeight = 25;
        const int SnakeWidth = 100;
        const int SnakeHeight = 23;

        public static void PrintBorders() // Printing borders of the GameField
        {
            for (int col = 0; col < gameWidth; col++)
            {
                PrintAtPosition(0, col, (char)254);
                PrintAtPosition(gameHeight - 1, col, (char)254);
            }

            for (int row = 0; row < gameHeight; row++)
            {
                PrintAtPosition(row, 0, (char)254);
                PrintAtPosition(row, SnakeWidth + 1, (char)254);
                PrintAtPosition(row, gameWidth - 1, (char)254);
            }
        }

        public static void PrintInfoPannel()
        {
            PrintAtPosition(2, 110, "Username:");
            PrintAtPosition(5, 107, "Current points:");
            PrintAtPosition(8, 111, "Level:");
            PrintAtPosition(11, 107, "Remaming time for");
            PrintAtPosition(12, 110, "super food:");
            PrintAtPosition(15, 110, "Controls:");
            PrintAtPosition(17, 113, (char)30);
            PrintAtPosition(18, 111, (char)17);
            PrintAtPosition(18, 115, (char)16);
            PrintAtPosition(19, 113, (char)31);
            PrintAtPosition(21, 112, "space");
        }
    }
}
