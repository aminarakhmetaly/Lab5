using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CodeOfSnake

{
    class Program
    {

        static Thread toMove;
        static bool Gameover;
        static Wall wall;
        static Snake snake;
        static Food food;
        static int level;
        static int score;
        static bool level_change;
        static void draw()

        {
            while (!Gameover)
            {

                Console.Clear();//chistka

                snake.move();// чтобы змейка двигалась 

                if (snake.CanEat(food))
                {
                    score++;

                    food.SetRandomPosition(wall);

                }
                if (snake.Collision(wall))

                {

                    Gameover = true;

                }

                if (snake.body.Count >= 10)

                {

                    level++;

                    level_change = true;

                }

                snake.Draw();

                wall.Draw();

                food.Draw();

                Console.SetCursorPosition(0, 39);

                Console.WriteLine("Your score is: {0}", score);

                Thread.Sleep(300);

            }

        }

        static void Main(string[] args)

        {

            Console.SetWindowSize(40, 40);

            Console.SetBufferSize(40, 40);

            Console.CursorVisible = false;

            wall = new Wall(level);

            snake = new Snake();

            food = new Food(wall);

            Gameover = false;

            snake.gen();

            toMove = new Thread(draw);

            toMove.Start();



            Menu:;

            toMove.Abort();//завершаем поток

            bool new_game = false; //для проверки начатия новой игры

            Console.Clear();//

            Console.WriteLine("F1 - new game");//новая игра

            Console.WriteLine("F2 - continue");//продолжить новую игру

            Console.WriteLine("F3 - exit");//выход

            ConsoleKeyInfo command = Console.ReadKey();//

            toMove = new Thread(draw);

            toMove.Start();////запуск потока

            switch (command.Key)

            {

                case ConsoleKey.F1:

                    new_game = true;

                    break;

                case ConsoleKey.F2:

                    new_game = false;

                    break;

                case ConsoleKey.F3:

                    toMove.Abort();// завершаем поток 

                    return;

            }

            level = 1;

            next_level:;//метка (корректный идентификатор)

            wall = new Wall(level);

            snake = new Snake();

            food = new Food(wall);

            Gameover = false;

            snake.gen();

            if (!new_game)

            {

                if (File.Exists("Snake.bin"))//проверяет существует файл или нет

                {

                    snake.Load();
                }

                if (File.Exists("Food.bin"))

                {

                    food.Load();

                }

                if (File.Exists("Wall.bin"))

                {

                    wall.Load();

                }

                if (File.Exists("Level.bin"))

                {

                    BinaryFormatter xs = new BinaryFormatter();

                    using (FileStream fs = new FileStream("Level.bin", FileMode.Open, FileAccess.Read))

                    {

                        level = (int)xs.Deserialize(fs);

                    }

                }

                if (File.Exists("Score.bin"))

                {

                    BinaryFormatter xs = new BinaryFormatter();

                    using (FileStream fs = new FileStream("Score.bin", FileMode.Open, FileAccess.Read))

                    {

                        score = (int)xs.Deserialize(fs);

                    }
                }
            }

            while (!Gameover)

            {

                if (level_change)

                {

                    level_change = false;

                    goto next_level;

                }

                ConsoleKeyInfo btn = Console.ReadKey();

                switch (btn.Key)

                {

                    case ConsoleKey.UpArrow:

                        snake.move(0, -1);

                        break;

                    case ConsoleKey.DownArrow:

                        snake.move(0, 1);

                        break;

                    case ConsoleKey.LeftArrow:

                        snake.move(-1, 0);

                        break;

                    case ConsoleKey.RightArrow:

                        snake.move(1, 0);

                        break;

                    case ConsoleKey.Escape:

                        {

                            toMove.Abort();

                            if (File.Exists("Snake.bin"))

                            {

                                File.Delete("Snake.bin");

                            }

                            if (File.Exists("Food.bin"))

                            {

                                File.Delete("Food.bin");

                            }

                            if (File.Exists("Wall.bin"))

                            {

                                File.Delete("Wall.bin");

                            }

                            if (File.Exists("Level.bin"))

                            {

                                File.Delete("Level.bin");

                            }

                            if (File.Exists("Score.bin"))

                            {

                                File.Delete("Score.bin");

                            }

                            snake.Save();

                            food.Save();

                            wall.Save();

                            {

                                BinaryFormatter xs = new BinaryFormatter();

                                using (FileStream fs = new FileStream("Level.bin", FileMode.OpenOrCreate, FileAccess.Write))

                                {

                                    xs.Serialize(fs, level);

                                }

                            }

                            {

                                BinaryFormatter xs = new BinaryFormatter();

                                using (FileStream fs = new FileStream("Score.bin", FileMode.OpenOrCreate, FileAccess.Write))

                                {

                                    xs.Serialize(fs, score);

                                }

                            }
                            goto Menu;

                        }
                }

            }

            if (Gameover)

            {

                Console.Clear();
                Console.WriteLine("Game over!");
                Console.ReadKey();
                goto Menu;
            }

        }

    }

}
