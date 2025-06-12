using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Madu
{
    internal class Point
    {
        public int x; // Координата X
        public int y; // Координата Y
        public string sym; // Символ точки

        public Point(int _x, int _y, string _sym)
        {
            x = _x; // Присваиваем X
            y = _y; // Присваиваем Y
            sym = _sym; // Присваиваем символ

        }

        public Point(Point p) // Конструктор копирования
        {
            x = p.x; // Копируем X
            y = p.y; // Копируем Y
            sym = p.sym; // Копируем символ
        }

        public void Move(int offset, Directions direction) // Перемещаем точку
        {
            if (direction == Directions.RIGHT) // Если направление вправо
            {
                x += offset; // Увеличиваем X
            }
            else if (direction == Directions.LEFT) // Если направление влево
            {
                x -= offset; // Уменьшаем X
            }
            else if (direction == Directions.UP) // Если направление вверх
            {
                y -= offset; // Уменьшаем Y
            }
            else if (direction == Directions.DOWN) // Если направление вниз
            {
                y += offset; // Увеличиваем Y
            }
        }

        public bool IsHit(Point p) // Проверяем, совпадает ли точка с другой точкой
        {
            return p.x == this.x && p.y == this.y; // Возвращаем true, если X и Y совпадают
        }

        public void Draw() // Рисуем точку
        {
            Console.SetCursorPosition(x, y); // Устанавливаем позицию курсора
            Console.Write(sym); // Выводим символ
        }

        public void Clear() // Очищаем точку (заменяем символом пробела)
        {
            sym = " "; // Устанавливаем символ пробела
            Draw(); // Рисуем пробел, чтобы стереть старый символ
        }

    }
}