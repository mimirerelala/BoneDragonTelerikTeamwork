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
        public static int score = 0;
        public static Queue<Position> snakePieces = new Queue<Position>();
        public static List<Score> highScores = new List<Score>();
        public static List<MapElement> mapElements = new List<MapElement>();
        public static bool isSnakeAlive = true;

        public static string scoresFileName = "scores.xml";
        static string[] menuEntries = { "NEW GAME", "HALL OF FAME", "EXIT" };
        public static ConsoleColor[] colors = (ConsoleColor[])ConsoleColor.GetValues(typeof(ConsoleColor));
        public static ConsoleColor currentSnakeColor = colors[4];


        static void Main(string[] args)
        {
            PrintMenu();
        }

        public static void PlayGame()
        {
			DirectionEnum direction = DirectionEnum.right;
            ClearGameField();
            GenerateSnake();
            isSnakeAlive = true;

            Random rng = new Random();
            GenerateFruit(rng);


            while (isSnakeAlive)
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
                    if (userInput.Key == ConsoleKey.Spacebar)
                    {
                        currentSnakeColor = colors[rng.Next(0, colors.Length)];
                    }

                }
                DrawSnake(direction,rng);
                score += 500 * snakePieces.Count / sleepTime;
                Thread.Sleep(sleepTime);
                if (sleepTime%50==0)
                {
                    GenerateRock(rng);
                }
                if (sleepTime > 10)
                {
                    sleepTime--;
                }
            }
        }

        private static void GenerateSnake()
        {
            snakePieces.Clear();
            snakePieces.Enqueue(new Position(10, 10));
            snakePieces.Enqueue(new Position(12, 10));
            snakePieces.Enqueue(new Position(14, 10));
            snakePieces.Enqueue(new Position(16, 10));
        }

        public static void GenerateFruit(Random rng)
        {
            Position food;
            do
            {
                food = new Position(rng.Next(0, 50)*2,
                    rng.Next(0, 50));
            }
            while (snakePieces.Contains(food) || mapElements.Any(e=>e.position.col == food.col && e.position.row == food.row));
            
            MapElement newElement = new MapElement()
            {
                position = food,
                type = MapElementsEnum.Fruit
            };
            mapElements.Add(newElement);
            Console.SetCursorPosition(food.row, food.col);
            Console.BackgroundColor = ConsoleColor.Green;
            Console.Write("  ");
        }

        public static void GenerateRock(Random rng)
        {
            Position rock;
            do
            {
                rock = new Position(rng.Next(0, 50) * 2,
                    rng.Next(0, 50));
            }
            while (snakePieces.Contains(rock) || mapElements.Any(e => e.position.col == rock.col && e.position.row == rock.row));

            MapElement newElement = new MapElement()
            {
                position = rock,
                type = MapElementsEnum.Rock
            };
            mapElements.Add(newElement);
            Console.SetCursorPosition(rock.row, rock.col);
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.Write("  ");
        }

        private static void DrawSnake(DirectionEnum direction, Random rng)
        {
            Position newHead;
            Position head = snakePieces.LastOrDefault();
            switch (direction)
            {
                case DirectionEnum.right:
                    newHead = new Position((head.row + 2)%100, head.col);
                    break;
                case DirectionEnum.left:
                    if (head.row < 2)
                    {
                        head.row += 100;
                    }
                    newHead = new Position(head.row - 2, head.col);
                    break;
                case DirectionEnum.down:
                    if (head.col < 1)
                    {
                        head.col += 50;
                    }
                    newHead = new Position(head.row, head.col - 1);
                    break;
                case DirectionEnum.up:
                    newHead = new Position(head.row, (head.col + 1)%50);
                    break;
                default:
                    throw new ArgumentException("Snake has unknown direction.");
            }

            MapElementsEnum headHitType = CollisionDetection(newHead);
            snakePieces.Enqueue(newHead);

            switch (headHitType)
            {
                case MapElementsEnum.None:
                    Console.BackgroundColor = currentSnakeColor;

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
                    SnakeIsDeath(score); // TODO: pass current score
                    break;
                case MapElementsEnum.Fruit:
                    Console.BackgroundColor = currentSnakeColor;

                    foreach (Position p in snakePieces)
                    {
                        Console.SetCursorPosition(p.row, p.col);
                        Console.Write("  ");
                    }
                    score++;
                    mapElements.Remove(
                        new MapElement(){
                            position = newHead,
                            type = MapElementsEnum.Fruit
                        }
                        );
                    GenerateFruit(rng);
                    break;
                case MapElementsEnum.Snake:
                    SnakeIsDeath(score); // TODO: pass current score
                    break;
                default:
                    break;
            }

            
        }

        private static void SnakeIsDeath(int currentScore)
        {
            isSnakeAlive = false;
            FileHandler scoreFile = new FileHandler(scoresFileName);
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.Black;
            if (scoreFile.IsScoreSaveable(currentScore)) 
            {
                string congratulationsMessage = "CONGRATULATIONS, YOU MADE IT TO THE TOP!";
                Console.SetCursorPosition((Console.WindowWidth / 2) - (congratulationsMessage.Length / 2), (Console.WindowHeight / 2) - 3);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(congratulationsMessage);
                Console.SetCursorPosition((Console.WindowWidth / 2) - 20, (Console.WindowHeight / 2) - 1);
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("ENTER A USERNAME: ");
                Console.SetCursorPosition((Console.WindowWidth / 2) - 2, (Console.WindowHeight / 2) - 1);
                scoreFile.SaveUserScore(Console.ReadLine(), currentScore);
                // Go to main menu or quit
                PrintMenu();
            }
            else
            {
                string gameoverMessage = "GAME OVER";
                Console.SetCursorPosition((Console.WindowWidth / 2) - (gameoverMessage.Length / 2), (Console.WindowHeight / 2) - 1);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(gameoverMessage);
                Console.ForegroundColor = ConsoleColor.DarkGray;
                string scoreMessage = string.Format("SCORE: {0}", currentScore);
                Console.SetCursorPosition((Console.WindowWidth / 2) - (scoreMessage.Length / 2), Console.WindowHeight / 2);
                Console.WriteLine(scoreMessage);
                string quitMessage = "(Press any key to go back to menu!)";
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.SetCursorPosition((Console.WindowWidth / 2) - (quitMessage.Length / 2), (Console.WindowHeight / 2) + 2);
                Console.WriteLine(quitMessage);
                // Handle input
                PrintMenu();
            }
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
            Console.SetBufferSize(Console.WindowWidth, Console.WindowHeight);
            Console.CursorVisible = false;

            bool exitedMenu = false;
            int currentSelectionIndex = 0;
            do
            {
                while (!Console.KeyAvailable && exitedMenu == false)
                {
                    PrintMenuSelection(currentSelectionIndex);
                    ConsoleKeyInfo currentModifier = Console.ReadKey();
                    switch (currentModifier.Key)
                    {
                        case ConsoleKey.DownArrow:
                            currentSelectionIndex += 1;
                            if (currentSelectionIndex > menuEntries.Length - 1)
                            {
                                currentSelectionIndex = 0;
                            }
                            break;
                        case ConsoleKey.UpArrow:
                            currentSelectionIndex -= 1;
                            if (currentSelectionIndex < 0)
                            {
                                currentSelectionIndex = menuEntries.Length - 1;
                            }
                            break;
                        case ConsoleKey.Enter:
                            switch (currentSelectionIndex)
                            {
                                case 0:
                                    Console.Clear();
                                    PlayGame();
                                    break;
                                case 1:
                                    Console.Clear();
                                    PrintHighScores();
                                    break;
                                case 2:
                                    Environment.Exit(0);
                                    break;
                            }
                            exitedMenu = true;
                            break;
                    }
                }
            } while (Console.ReadKey(true).Key != ConsoleKey.Escape);
        }

        public static void PrintMenuSelection(int currentSelectionIndex)
        {
            Console.Clear();
            Console.WriteLine("                      __    __    __    __");
            Console.WriteLine("                     /  \\  /  \\  /  \\  /  \\");
            Console.WriteLine("____________________/  __\\/  __\\/  __\\/  __\\___________________________________");
            Console.WriteLine("___________________/  /__/  /__/  /__/  /______________________________________");
            Console.WriteLine("                  | / \\   / \\   / \\   / \\  \\____");
            Console.WriteLine("                  |/   \\_/   \\_/   \\_/   \\    o \\");
            Console.WriteLine("                                           \\_____/--<");
            int initialStart = Console.WindowHeight / 2 - (int)Math.Ceiling((double)menuEntries.Length / 2);
            int menuSpacing = 0;
            for (int i = 0; i < menuEntries.Length; i++)
            {
                Console.SetCursorPosition(Console.WindowWidth / 2 - (int)Math.Ceiling((double)menuEntries[i].Length / 2), initialStart + menuSpacing);
                menuSpacing += 1;
                if (i == currentSelectionIndex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(menuEntries[i]);
                    Console.ForegroundColor = ConsoleColor.White;
                    continue;
                }

                Console.WriteLine(menuEntries[i]);
            }
        }

        /// <summary>
        /// Print top ten scores from the file
        /// </summary>
        public static void PrintHighScores()
        {
            FileHandler scoreFile = new FileHandler(scoresFileName);
            scoreFile.PrintScores();
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
