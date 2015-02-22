﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Snake
{
    class Program
    {
        
        struct Position
        {
            public int row;
            public int col;
            public Position(int row, int col)
            {
                this.row = row;
                this.col = col;
            }
        }

        static void Main(string[] args)
        {
            Console.Title = "Dragon-Snake";
            Console.WindowHeight = 60;
            Console.WindowWidth = 130;
            Console.BufferHeight = 60;
            Console.BufferWidth = 133;

            //map 65 x60
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Red;
            for (int i = 0; i < Console.WindowHeight; i++)
            {
                for (int j = 0; j < Console.WindowWidth; j++)
                {
                    Console.Write(' ');
                }
                Console.WriteLine(' ');
            }
            Queue<Position> snakePieces = new Queue<Position>();

            Console.SetCursorPosition(10, 10);
            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.Yellow;
            int row = 16;
            snakePieces.Enqueue(new Position(10, 10));
            snakePieces.Enqueue(new Position(12, 10));
            snakePieces.Enqueue(new Position(14, 10));
            snakePieces.Enqueue(new Position(16, 10));

            while (true)
            {
                foreach (Position p in snakePieces)
                {
                    Console.SetCursorPosition(p.row, p.col);
                    Console.Write("  ");

                }
                Position PositionToDelete = snakePieces.Dequeue();
                Console.SetCursorPosition(PositionToDelete.row, PositionToDelete.col);
                Console.BackgroundColor = ConsoleColor.White;
                Console.Write("  ");
                Console.BackgroundColor = ConsoleColor.Red;
                row += 2;              
                snakePieces.Enqueue(new Position(row%130, 10));
                Thread.Sleep(10);               
            }


            //   for (int i = 0; i < ImageToDisplay.GetLength(0); i++)
            //   {
            //
            //       for (int j = 0; j < ImageToDisplay.GetLength(1); j++)
            //       {
            //           Console.BackgroundColor = ConsoleColor.White;
            //           Console.ForegroundColor = ConsoleColor.Black;
            //
            //           switch (ImageToDisplay[i, j])
            //           {
            //               case 'k':
            //                   Console.BackgroundColor = ConsoleColor.Black;
            //                   Console.ForegroundColor = ConsoleColor.Black;
            //                   break;
            //               case 's':
            //                   Console.BackgroundColor = ConsoleColor.DarkGray;
            //                   Console.ForegroundColor = ConsoleColor.DarkGray;
            //                   break;
            //               case 'g':
            //                   Console.BackgroundColor = ConsoleColor.Gray;
            //                   Console.ForegroundColor = ConsoleColor.Gray;
            //                   break;
            //               case 'w':
            //                   Console.BackgroundColor = ConsoleColor.White;
            //                   Console.ForegroundColor = ConsoleColor.White;
            //                   break;
            //               case 'y':
            //                   Console.BackgroundColor = ConsoleColor.Yellow;
            //                   Console.ForegroundColor = ConsoleColor.Yellow;
            //                   break;
            //               case 'u':
            //                   Console.BackgroundColor = ConsoleColor.DarkYellow;
            //                   Console.ForegroundColor = ConsoleColor.DarkYellow;
            //                   break;
            //               case 'h':
            //                   Console.BackgroundColor = ConsoleColor.DarkRed;
            //                   Console.ForegroundColor = ConsoleColor.DarkRed;
            //                   break;
            //               case 'r':
            //                   Console.BackgroundColor = ConsoleColor.Red;
            //                   Console.ForegroundColor = ConsoleColor.Red;
            //                   break;
            //               case 'c':
            //                   Console.BackgroundColor = ConsoleColor.Cyan;
            //                   Console.ForegroundColor = ConsoleColor.Cyan;
            //                   break;
            //               case 'z':
            //                   Console.BackgroundColor = ConsoleColor.DarkCyan;
            //                   Console.ForegroundColor = ConsoleColor.DarkCyan;
            //                   break;
            //               case 'm':
            //                   Console.BackgroundColor = ConsoleColor.Magenta;
            //                   Console.ForegroundColor = ConsoleColor.Magenta;
            //                   break;
            //               case 'f':
            //                   Console.BackgroundColor = ConsoleColor.DarkMagenta;
            //                   Console.ForegroundColor = ConsoleColor.DarkMagenta;
            //                   break;
            //               case 'b':
            //                   Console.BackgroundColor = ConsoleColor.Blue;
            //                   Console.ForegroundColor = ConsoleColor.Blue;
            //                   break;
            //               case 'o':
            //                   Console.BackgroundColor = ConsoleColor.DarkBlue;
            //                   Console.ForegroundColor = ConsoleColor.DarkBlue;
            //                   break;
            //               case 'v':
            //                   Console.BackgroundColor = ConsoleColor.Green;
            //                   Console.ForegroundColor = ConsoleColor.Green;
            //                   break;
            //               case 'p':
            //                   Console.BackgroundColor = ConsoleColor.DarkGreen;
            //                   Console.ForegroundColor = ConsoleColor.DarkGreen;
            //                   break;
            //
            //               default:
            //                   Console.BackgroundColor = ConsoleColor.Black;
            //                   Console.ForegroundColor = ConsoleColor.Black;
            //                   break;
            //           }
            //
            //           Console.Write(' ');
            //       }
            //       Console.WriteLine();
            //   }
            //


        }
    }
}