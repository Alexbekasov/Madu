using System;
using System.Collections.Generic;
using System.Linq;

namespace Madu
{
    class Snake : Figure
    {
        public Directions direction; // Направление движения змейки

        public Snake(Point tail, int length, Directions _direction)
        {
            direction = _direction; // Устанавливаем начальное направление

            plist = new List<Point>(); // Инициализируем список точек, составляющих змейку
            // Extend snake backwards from tail
            for (int i = 0; i < length; i++) // Создаем точки для начальной длины змейки
            {
                Point p = new Point(tail); // Копируем точку хвоста
                p.Move(-i, direction); // Перемещаем точку назад относительно направления (для построения хвоста)
                plist.Add(p); // Добавляем точку в список змейки
            }
        }

        internal void Move() // Метод для движения змейки
        {
            Point tail = plist.First(); // Получаем первую (хвостовую) точку змейки
            plist.Remove(tail); // Удаляем хвост
            Point head = GetNextPoint(); // Получаем новую позицию для головы
            plist.Add(head); // Добавляем новую голову в список

            tail.Clear(); // Очищаем старую позицию хвоста (превращаем в пробел)
            head.Draw(); // Рисуем новую голову
        }

        internal bool Eat(Point food) // Метод для проверки, съела ли змейка еду
        {
            Point head = GetNextPoint(); // Получаем следующую позицию головы
            if (head.IsHit(food)) // Если голова совпадает с едой
            {
                food.sym = head.sym; // Присваиваем символу еды символ головы змейки (чтобы она рисовалась как часть змейки)
                plist.Add(food); // Добавляем еду как новую часть змейки
                return true; // Возвращаем true, что еда съедена
            }
            return false; // Возвращаем false, еда не съедена
        }

        public bool IsHit(Point p) // Проверяем, совпадает ли змейка с заданной точкой
        {
            foreach (var part in plist) // Для каждой части змейки
            {
                if (part.IsHit(p)) // Если часть змейки совпадает с точкой
                    return true; // Возвращаем true
            }
            return false; // Возвращаем false
        }

        public Point GetNextPoint() // Получаем следующую позицию головы змейки
        {
            Point head = plist.Last(); // Получаем текущую голову змейки
            Point nextPoint = new Point(head); // Создаем новую точку, копируя голову
            nextPoint.Move(1, direction); // Перемещаем эту новую точку на 1 шаг в текущем направлении
            return nextPoint; // Возвращаем следующую позицию головы
        }

        public void HandleKey(ConsoleKey key) // Обрабатываем нажатие клавиши для изменения направления
        {
            Directions newDirection = direction; // Предполагаемое новое направление

            {
                if (key == ConsoleKey.LeftArrow) // Если нажата стрелка влево
                    newDirection = Directions.LEFT; // Новое направление - влево
                else if (key == ConsoleKey.RightArrow) // Если нажата стрелка вправо
                    newDirection = Directions.RIGHT; // Новое направление - вправо
                else if (key == ConsoleKey.UpArrow) // Если нажата стрелка вверх
                    newDirection = Directions.UP; // Новое направление - вверх
                else if (key == ConsoleKey.DownArrow) // Если нажата стрелка вниз
                    newDirection = Directions.DOWN; // Новое направление - вниз
            }

            // Prevent reversing on itself
            // Проверяем, чтобы змейка не могла двигаться в прямо противоположном направлении (например, с права налево, если она движется вправо)
            if ((direction == Directions.LEFT && newDirection != Directions.RIGHT) ||
                (direction == Directions.RIGHT && newDirection != Directions.LEFT) ||
                (direction == Directions.UP && newDirection != Directions.DOWN) ||
                (direction == Directions.DOWN && newDirection != Directions.UP))
            {
                direction = newDirection; // Устанавливаем новое направление
            }
        }

        public override void Draw() // Рисуем змейку
        {
            // Set snake body color (e.g., Green for the snake itself)
            Console.ForegroundColor = ConsoleColor.Green; // Устанавливаем зеленый цвет для змейки
            base.Draw(); // Вызываем базовый метод Draw() для рисования всех точек списка (частей змейки)
            // Note: Console.ResetColor() is now called in Program.cs after snake.Draw() for better scope management.
        }

        internal bool IsHitTail() // Проверяем столкновение с хвостом
        {
            var head = plist.Last(); // Получаем голову змейки
            for (int i = 0; i < plist.Count - 1; i++) // Проходим по всем частям змейки, кроме головы
            {
                if (head.IsHit(plist[i])) // Если голова совпадает с какой-либо частью хвоста
                    return true; // Возвращаем true (столкновение с хвостом)
            }
            return false; // Возвращаем false (нет столкновения с хвостом)
        }

        public List<Point> GetPoints() // Получаем список всех точек, составляющих змейку
        {
            return plist; // Возвращаем список точек
        }
    }
}