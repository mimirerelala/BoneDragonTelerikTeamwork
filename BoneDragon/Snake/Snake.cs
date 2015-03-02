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

        public struct MapElement
        {
            public char symbol;
            public Position position;
            public MapElementsEnum type;
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

        public static int sleepTime = 500;
        public static Queue<Position> snakePieces = new Queue<Position>();
        public static List<Score> highScores = new List<Score>();
        public static List<MapElement> mapElements = new List<MapElement>();
        public static string username;

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

            //ClearGameField();
            PrintMenu();

            Console.SetCursorPosition(10, 10);
            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.Yellow;
            //int row = 16;
            //GenerateSnake();

            //while (true)
            //{
            //    DrawSnake();
            //    
            //    Console.BackgroundColor = ConsoleColor.Red;
            //    row += 2;
            //    snakePieces.Enqueue(new Position(row % 100, 10));
            //    Thread.Sleep(10);
            //}
        }

        public static void PlayGame()
        {
			DirectionEnum direction = DirectionEnum.right;
            Console.Write("Input Username:");
            username = Console.ReadLine();
            ClearGameField();
            GenerateSnake();

            while (true)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo userInput = Console.ReadKey(true);
                    if (userInput.Key == ConsoleKey.LeftArrow)
                    {
                        if (direction != DirectionEnum.right) direction = DirectionEnum.left;
                    }
                    if (userInput.Key == ConsoleKey.RightArrow)
                    {
                        if (direction != DirectionEnum.left) direction = DirectionEnum.right;
                    }
                    if (userInput.Key == ConsoleKey.DownArrow)
                    {
                        if (direction != DirectionEnum.down) direction = DirectionEnum.up;
                    }
                    if (userInput.Key == ConsoleKey.UpArrow)
                    {
                        if (direction != DirectionEnum.up) direction = DirectionEnum.down;
                    }
                }
                DrawSnake(direction);
                Thread.Sleep(sleepTime);
            }
        }

        private static void GenerateSnake()
        {
            snakePieces.Enqueue(new Position(10, 10));
            snakePieces.Enqueue(new Position(12, 10));
            snakePieces.Enqueue(new Position(14, 10));
            snakePieces.Enqueue(new Position(16, 10));
        }

        public static void GenerateFruit()
        {
            //TODO
            //Print fruit
        }

        private static void DrawSnake(DirectionEnum direction)
        {
            Position newHead;
            Position head = snakePieces.LastOrDefault();
            switch (direction)
            {
                case DirectionEnum.right:
                    newHead = new Position((head.row + 2)%100, head.col);
                    snakePieces.Enqueue(newHead);
                    break;
                case DirectionEnum.left:
                    if (head.row < 2)
                    {
                        head.row += 100;
                    }
                    newHead = new Position(head.row - 2, head.col);
                    snakePieces.Enqueue(newHead);
                    break;
                case DirectionEnum.down:
                    if (head.col < 1)
                    {
                        head.col += 50;
                    }
                    newHead = new Position(head.row, head.col - 1);
                    snakePieces.Enqueue(newHead);
                    break;
                case DirectionEnum.up:
                    newHead = new Position(head.row, (head.col + 1)%50);
                    snakePieces.Enqueue(newHead);
                    break;
                default:
                    throw new ArgumentException("Snake has unknown direction.");
                    break;
            }

            MapElementsEnum headHitType = CollisionDetection(newHead);

            switch (headHitType)
            {
                case MapElementsEnum.None:
                    Console.BackgroundColor = ConsoleColor.Red;

                    foreach (Position p in snakePieces)
                    {
                        Console.SetCursorPosition(p.row, p.col);
                        Console.Write("  ");
                    }

                    Position PositionToDelete = snakePieces.Dequeue();
                    Console.SetCursorPosition(PositionToDelete.row, PositionToDelete.col);
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.Write("  ");
                    break;
                case MapElementsEnum.Rock:
                    SnakeIsDeath();
                    break;
                case MapElementsEnum.Fruit:
                    Console.BackgroundColor = ConsoleColor.Red;

                    foreach (Position p in snakePieces)
                    {
                        Console.SetCursorPosition(p.row, p.col);
                        Console.Write("  ");
                    }
                    break;
                case MapElementsEnum.Snake:
                    SnakeIsDeath();
                    break;
                default:
                    break;
            }

            
        }

        private static void SnakeIsDeath()
        {
            ///TODO 
            ///Handle GAME OVER
            throw new NotImplementedException();
        }

        private static MapElementsEnum CollisionDetection(Position newHead)
        {
            foreach (MapElement item in mapElements)
            {
                if ((item.position.col == newHead.col) && (item.position.row == newHead.row))
                {
                    return item.type;
                }
            }

            foreach (Position item in snakePieces)
            {
                if ((item.col == newHead.col) && (item.row == newHead.row))
                {
                    return MapElementsEnum.Snake;
                }
            }

            return MapElementsEnum.None;
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
            Console.SetCursorPosition(0, 0);
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("Please choose one");
            Console.WriteLine("1. Play New Game");
            Console.WriteLine("2. High Score");
            Console.WriteLine("3. Exit");
            Console.Write("Enter your choice: ");
            int choice;
            bool isParsed = int.TryParse(Console.ReadLine(),out choice);
            if (isParsed)
            {
                switch (choice)
                {
                    case 1: Console.Clear(); PlayGame(); break;
                    case 2: Console.Clear(); PrintHighScore(); break;
                    case 3: Environment.Exit(0); break;
                    default: Console.WriteLine("Invalid argument for choice!"); PrintMenu(); break;
                }
            }
            else
            {
                Console.WriteLine("Invalid argument for choice!"); PrintMenu();
            }
            
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
