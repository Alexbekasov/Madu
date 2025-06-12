using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Madu
{
    class FoodCreator
    {
        int mapWidth;
        int mapHeight;
        string sym;

        Random random = new Random();

        public FoodCreator(int mapWidth, int mapHeight, string sym)
        {
            this.mapWidth = mapWidth; // Присваиваем ширину карты
            this.mapHeight = mapHeight; // Присваиваем высоту карты
            this.sym = sym; // Присваиваем символ еды
        }
        public Point CreateFood(List<Point> snakePoints, List<Point> wallPoints)
        {
            Point p;
            do
            {
                int x = random.Next(2, mapWidth - 2); // Генерируем случайную X-координату в пределах карты
                int y = random.Next(2, mapHeight - 2); // Генерируем случайную Y-координату в пределах карты
                p = new Point(x, y, sym); // Создаем новую точку для еды
            }
            while (snakePoints.Any(sp => sp.IsHit(p)) || wallPoints.Any(wp => wp.IsHit(p))); // Проверяем, чтобы еда не появилась на змейке или стенах

            return p; // Возвращаем созданную точку еды
        }

    }
}