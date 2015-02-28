using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Snake
{
    public class Snake
    {
        public struct Position
        {
            public int row;
            public int col;
            public Position(int row, int col)
            {
                this.row = row;
                this.col = col;
            }
        }

        public struct Score
        {
            public string name;
            public int result;
            public Score(string name, int result)
            {
                this.name = name;
                this.result = result;
            }
        }

        public static uint sleepTime = 1000;
        public static Queue<Position> snakePieces = new Queue<Position>();
        public static List<Score> highScores = new List<Score>();

        static void Main(string[] args)
        {
            Position[] directions = new Position[]
            {
                new Position(0, 1), // right
                new Position(0, -1), // left
                new Position(1, 0), // down
                new Position(-1, 0), // up
            };

            Random randomNumbersGenerator = new Random();

            ClearGameField();

            Console.SetCursorPosition(10, 10);
            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.Yellow;
            int row = 16;
            GenerateSnake();

            while (true)
            {
                DrawSnake();
                
                Console.BackgroundColor = ConsoleColor.Red;
                row += 2;
                snakePieces.Enqueue(new Position(row % 100, 10));
                Thread.Sleep(10);
            }
        }

        private static void GenerateSnake()
        {
            snakePieces.Enqueue(new Position(10, 10));
            snakePieces.Enqueue(new Position(12, 10));
            snakePieces.Enqueue(new Position(14, 10));
            snakePieces.Enqueue(new Position(16, 10));
        }

        private static void DrawSnake()
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
        }

        public static void ClearGameField()
        {
            //TODO
            //Clear game field

            Console.Title = "Dragon-Snake";
            Console.WindowHeight = 50;
            Console.WindowWidth = 100;
            Console.BufferHeight = 50;
            Console.BufferWidth = 103;
            //add PlayIntro() options
            //add Menu() with options
            DrawField(ConsoleColor.White);
        }

        public static void ClearSnake()
        {
            //TODO
            //Clear snake Queue
        }

        public static void PrintField()
        {
            //TODO
            //Print game field
        }

        public static void UpdateFile()
        {
            //TODO
            //Just like printing it but will only update differences on the field
            //not printing the whole field
            //can be delayed for first versions
        }

        public static void PrintMenu()
        {
            //TODO
            //Print game Menu
        }

        /// <summary>
        /// Print 10 best to Console
        /// </summary>
        public static void PrintHighScore()
        {
            //TODO
            //Print HighScores
            //don't make reading from file here
        }

        /// <summary>
        /// Saves highscores to file
        /// </summary>
        public static void SaveHighScore()
        {
            //TODO
            //Save highscores to file
        }

        /// <summary>
        /// Reads highscores from file and saves into runtime struct
        /// </summary>
        public static void ReadHighScore()
        {
            //TODO
            //Read highscores from file and save them to Highscores struct
        }


        public static void DrawField(ConsoleColor ConsoleBackgroundColor)
        {
            //map 65 x60
            Console.BackgroundColor = ConsoleBackgroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            for (int i = 0; i < Console.WindowHeight; i++)
            {
                for (int j = 0; j < Console.WindowWidth; j++)
                {
                    Console.Write(' ');
                }
                Console.WriteLine(' ');
            }
        }
    }
}
