using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        }

        public static void ClearGameField()
        {
            //TODO
            //Clear game field
            Console.WriteLine("hi");
            Console.Title = "Dragon-Snake";
            Console.WindowHeight = 60;
            Console.WindowWidth = 130;
            Console.BufferHeight = 60;
            Console.BufferWidth = 133;
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

            Console.BackgroundColor = ConsoleBackgroundColor;
            for (int i = 0; i < Console.WindowHeight; i++)
            {
                for (int j = 0; j < Console.WindowWidth; j++)
                {
                    Console.Write(' ');
                }
                Console.WriteLine(' ');
            }
            Console.SetCursorPosition(0, 0);
        }

    }
}
