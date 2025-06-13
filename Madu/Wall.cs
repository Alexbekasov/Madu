using System;
using System.Collections.Generic;
using System.Linq; // Для метода Any()

namespace Madu
{
    // Класс, представляющий одну линию стены (горизонтальную или вертикальную)
    class LineWall
    {
        protected List<Point> points; // Список точек, из которых состоит линия

        public LineWall(int startX, int startY, int endX, int endY, string symbol)
        {
            points = new List<Point>();
            if (startX == endX) // Вертикальная линия
            {
                for (int y = Math.Min(startY, endY); y <= Math.Max(startY, endY); y++)
                {
                    points.Add(new Point(startX, y, symbol));
                }
            }
            else if (startY == endY) // Горизонтальная линия
            {
                for (int x = Math.Min(startX, endX); x <= Math.Max(startX, endX); x++)
                {
                    points.Add(new Point(x, startY, symbol));
                }
            }
        }

        // Метод для отрисовки линии стены
        public void Draw()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow; // Цвет стен - темно-желтый
            foreach (var p in points)
            {
                p.Draw();
            }
            Console.ResetColor();
        }

        // Метод для получения всех точек линии
        public List<Point> GetPoints()
        {
            return points;
        }

        // Метод для проверки столкновения змейки с этой линией стены (включая будущую позицию головы)
        public bool CheckCollision(Snake snake)
        {
            Point snakeHead = snake.GetNextHeadPosition(); // Получаем следующую позицию головы змейки
            foreach (var wallPoint in points)
            {
                if (snakeHead.IsHit(wallPoint))
                {
                    return true; // Столкновение со стеной
                }
            }
            return false;
        }
    }

    // Класс, управляющий всеми стенами на карте (рамка)
    public class Walls
    {
        private List<LineWall> walls; // Список всех линий стен
        public int MapWidth { get; private set; }
        public int MapHeight { get; private set; }

        public Walls(int mapWidth, int mapHeight)
        {
            MapWidth = mapWidth;
            MapHeight = mapHeight;
            walls = new List<LineWall>();

            // Создаем линии, формирующие рамку карты
            walls.Add(new LineWall(0, 0, mapWidth - 1, 0, "+"));             // Верхняя горизонтальная
            walls.Add(new LineWall(0, mapHeight - 1, mapWidth - 1, mapHeight - 1, "+")); // Нижняя горизонтальная
            walls.Add(new LineWall(0, 0, 0, mapHeight - 1, "+"));             // Левая вертикальная
            walls.Add(new LineWall(mapWidth - 1, 0, mapWidth - 1, mapHeight - 1, "+")); // Правая вертикальная
        }

        // Метод для отрисовки всех стен
        public void Draw()
        {
            foreach (var wall in walls)
            {
                wall.Draw();
            }
        }

        // Метод для получения списка всех точек, из которых состоят стены
        public List<Point> GetAllWallPoints()
        {
            List<Point> allPoints = new List<Point>();
            foreach (var wallLine in walls)
            {
                allPoints.AddRange(wallLine.GetPoints());
            }
            return allPoints;
        }

        // Метод для проверки столкновения змейки с любой частью рамки
        public bool CheckCollision(Snake snake)
        {
            // Здесь мы проверяем, столкнется ли голова змейки с любой из внутренних точек стены.
            // CheckWallCollision в Snake.cs уже проверяет выход за границы MapWidth/MapHeight.
            // Этот метод будет использоваться, если мы добавим внутренние препятствия.
            // Сейчас он дублирует часть проверки Snake.CheckWallCollision, но делает это через точки.
            Point snakeHead = snake.GetNextHeadPosition();
            return GetAllWallPoints().Any(wallPoint => snakeHead.IsHit(wallPoint));
        }
    }
}