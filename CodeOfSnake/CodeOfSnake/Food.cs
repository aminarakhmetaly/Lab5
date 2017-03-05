using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CodeOfSnake

{
    [Serializable]

    public class Food : Warp

    {

        public Food()

        {
        }

        public Food(Wall wall)

        {

            color = ConsoleColor.Green;

            sign = '&';

            SetRandomPosition(wall);

        }

        public bool GoodPosition(int x, int y, Wall wall)

        {

            for (int i = 0; i < wall.body.Count; ++i)

            {

                if (x == wall.body[i].x && y == wall.body[i].y)

                    return false;

            }

            return true;

        }

        public void SetRandomPosition(Wall wall)

        {

            body.Clear();

            int x = 1 + new Random().Next() % 30;

            int y = 1 + new Random().Next() % 30;

            while (!GoodPosition(x, y, wall))

            {
                x = 1 + new Random().Next() % 30;

                y = 1 + new Random().Next() % 30;
            }

            body.Add(new Point(x, y));
        }
    }

}