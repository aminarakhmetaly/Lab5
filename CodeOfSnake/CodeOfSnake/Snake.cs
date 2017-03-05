using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace CodeOfSnake

{
    [Serializable]

    public class Snake : Warp

    {

        public int dx, dy;

        public Snake()

        {
            sign = 'o';

            color = ConsoleColor.DarkCyan;

        }

        public void gen()

        {

            dx = dy = 0;

            body.Add(new Point(10, 10));

            body.Add(new Point(9, 10));

        }

        public bool Collision(Wall wall)

        {

            for (int i = 0; i < wall.body.Count; i++)

            {

                if (wall.body[i].x == body[0].x && wall.body[i].y == body[0].y)

                    return true;

            }

            return false;

        }

        public void move(int _dx, int _dy)

        {

            dx = _dx;

            dy = _dy;

        }

        public void move()

        {

            for (int i = body.Count - 1; i > 0; --i)

            {

                body[i].x = body[i - 1].x;

                body[i].y = body[i - 1].y;

            }

            body[0].x += dx;

            body[0].y += dy;

            if (body[0].x > 30)

            {

                body[0].x = 1;

            }

            if (body[0].x < 1)

            {

                body[0].x = 30;

            }

            if (body[0].y > 30)

            {

                body[0].y = 1;

            }

            if (body[0].y < 1)

            {

                body[0].y = 30;

            }
            /*for (int i = 2; i < body.Count; i++)
            {
                if (body[0].x == body[i].x && body[0].y == body[i].y)
                {
                    Console.Clear();
                    Console.WriteLine("Game Over!");
                    Console.ReadKey();
                }
            }*/
        }

        public bool CanEat(Food f)

        {

            if (body[0].x == f.body[0].x && body[0].y == f.body[0].y)

            {

                body.Add(new Point(f.body[0].x, f.body[0].y));

                return true;

            }

            return false;

        }

    }

}

