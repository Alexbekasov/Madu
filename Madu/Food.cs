using System;
using System.Collections.Generic;
using System.Linq;

namespace Madu
{
    // Класс, представляющий еду для змейки
    public class Food
    {
        public Point Position { get; private set; } // Позиция еды
        private string foodSymbol; // Символ еды
        private Random random; // Генератор случайных чисел

        // Конструктор еды
        public Food(string symbol)
        {
            foodSymbol = symbol;
            random = new Random();
        }

        // Метод для создания новой еды в случайном месте на карте
        // Принимает список сегментов тела змейки и точек стен для избегания столкновений
        public void GenerateNewPosition(int mapWidth, int mapHeight, List<Point> snakeBody, List<Point> wallPoints)
        {
            Point newFoodPosition;
            do
            {
                // Генерируем случайные координаты внутри игрового поля,
                // отступая от краев на 2 единицы, чтобы избежать появления еды на стенах
                int x = random.Next(2, mapWidth - 2);
                int y = random.Next(2, mapHeight - 2);
                newFoodPosition = new Point(x, y, foodSymbol);
            }
            // Повторяем, пока новая позиция еды не будет свободной (не на змейке и не на стене)
            while (snakeBody.Any(s => s.IsHit(newFoodPosition)) || wallPoints.Any(w => w.IsHit(newFoodPosition)));

            Position = newFoodPosition; // Устанавливаем новую позицию еды
        }

        // Метод для отрисовки еды
        public void Draw()
        {
            Console.ForegroundColor = ConsoleColor.Red; // Цвет еды - красный
            Position.Draw();
            Console.ResetColor();
        }
    }
}