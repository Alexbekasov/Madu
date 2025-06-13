using System;

namespace Madu
{
    // Класс, представляющий одну точку на консоли.
    public class Point
    {
        public int X { get; set; } // Координата X
        public int Y { get; set; } // Координата Y
        public string Symbol { get; set; } // Символ для отображения точки

        // Конструктор точки
        public Point(int x, int y, string symbol)
        {
            X = x;
            Y = y;
            Symbol = symbol;
        }

        // Метод для отрисовки точки на консоли
        public void Draw()
        {
            Console.SetCursorPosition(X, Y);
            Console.Write(Symbol);
        }

        // Метод для очистки точки (перерисовка пробелом)
        public void Clear()
        {
            Console.SetCursorPosition(X, Y);
            Console.Write(" ");
        }

        // Метод для проверки, находится ли эта точка на той же позиции, что и другая точка
        public bool IsHit(Point otherPoint)
        {
            return X == otherPoint.X && Y == otherPoint.Y;
        }
    }
}