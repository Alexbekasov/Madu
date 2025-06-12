using System;
using System.Collections.Generic;

namespace Madu
{
    internal class VerticalLine : Figure
    {
        public VerticalLine(int yUp, int yDown, int x, string sym)
        {
            plist = new List<Point>(); // Инициализируем список точек для линии
            for (int y = yUp; y <= yDown; y++) // Проходим от верхней до нижней Y-координаты
            {
                Point p = new Point(x, y, sym); // Создаем новую точку
                plist.Add(p); // Добавляем точку в список
            }
        }

        public override void Draw()
        {
            // Changed wall color to DarkYellow for better contrast and consistent wall color
            Console.ForegroundColor = ConsoleColor.DarkYellow; // Устанавливаем цвет стен (темно-желтый)

            foreach (Point p in plist) // Для каждой точки в линии
            {
                p.Draw(); // Рисуем точку
            }
            // Reset color after drawing to not affect subsequent output
            Console.ResetColor(); // Сбрасываем цвет консоли на стандартный
        }
    }
}