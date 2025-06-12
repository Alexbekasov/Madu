using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Madu
{
    class HorizontalLine : Figure
    {
        public HorizontalLine(int xLeft, int xRight, int y, string sym)
        {
            plist = new List<Point>(); // Инициализируем список точек для линии
            for (int x = xLeft; x <= xRight; x++) // Проходим от левой до правой X-координаты
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