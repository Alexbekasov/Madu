using System;
using System.Collections.Generic;
using System.Linq;

namespace Madu
{
    // Класс, представляющий змейку    
    public class Snake
    {
        private List<Point> body; // Список сегментов тела змейки
        public Direction CurrentDirection { get; private set; } // Текущее направление движения змейки
        private string snakeSymbol; // Символ, которым отображается змейка

        // Конструктор змейки: принимает начальную позицию головы, длину и символ
        public Snake(int startX, int startY, int length, string symbol)
        {
            body = new List<Point>();
            snakeSymbol = symbol;
            CurrentDirection = Direction.Right; // Змейка по умолчанию начинает двигаться вправо

            // Создаем начальные сегменты тела змейки. Голова будет последним элементом в списке.
            for (int i = 0; i < length; i++)
            {
                // Сегменты строятся назад от головы
                body.Insert(0, new Point(startX - i, startY, snakeSymbol));
            }
        }

        // Публичный метод для получения всех сегментов тела змейки
        public List<Point> GetAllBodyPoints()
        {
            return body;
        }

        // Метод для отрисовки всей змейки
        public void Draw()
        {
            Console.ForegroundColor = ConsoleColor.Green; // Цвет змейки - зеленый
            foreach (var segment in body)
            {
                segment.Draw();
            }
            Console.ResetColor();
        }

        // Метод для движения змейки на один шаг
        public void Move()
        {
            Point tail = body.First(); // Получаем хвост
            body.Remove(tail); // Удаляем хвост из списка
            tail.Clear(); // Очищаем хвост на экране

            Point newHead = GetNextHeadPosition(); // Вычисляем новую позицию головы
            body.Add(newHead); // Добавляем новую голову в список
            newHead.Draw(); // Рисуем новую голову
        }

        // Публичный метод для получения следующей позиции головы змейки (для проверок столкновений)
        public Point GetNextHeadPosition()
        {
            Point currentHead = body.Last(); // Текущая голова змейки
            Point newHead = new Point(currentHead.X, currentHead.Y, snakeSymbol); // Новая точка для головы

            // Перемещаем новую точку в текущем направлении
            switch (CurrentDirection)
            {
                case Direction.Up:
                    newHead.Y--;
                    break;
                case Direction.Down:
                    newHead.Y++;
                    break;
                case Direction.Left:
                    newHead.X--;
                    break;
                case Direction.Right:
                    newHead.X++;
                    break;
            }
            return newHead;
        }

        // Метод для обработки нажатий клавиш и изменения направления змейки
        public void ChangeDirection(ConsoleKey key)
        {
            Direction newDirection = CurrentDirection;

            // Определяем новое направление на основе нажатой клавиши
            switch (key)
            {
                case ConsoleKey.UpArrow:
                    newDirection = Direction.Up;
                    break;
                case ConsoleKey.DownArrow:
                    newDirection = Direction.Down;
                    break;
                case ConsoleKey.LeftArrow:
                    newDirection = Direction.Left;
                    break;
                case ConsoleKey.RightArrow:
                    newDirection = Direction.Right;
                    break;
            }

            // Предотвращаем разворот змейки на 180 градусов (столкновение с собой)
            if ((CurrentDirection == Direction.Up && newDirection == Direction.Down) ||
                (CurrentDirection == Direction.Down && newDirection == Direction.Up) ||
                (CurrentDirection == Direction.Left && newDirection == Direction.Right) ||
                (CurrentDirection == Direction.Right && newDirection == Direction.Left))
            {
                // Если попытка разворота, игнорируем изменение направления
                return;
            }

            CurrentDirection = newDirection; // Устанавливаем новое направление
        }

        // Метод для проверки столкновения змейки с самой собой (хвостом)
        public bool CheckSelfCollision()
        {
            Point head = body.Last(); // Голова змейки
            // Проверяем, совпадает ли голова с каким-либо другим сегментом тела (кроме самой себя)
            for (int i = 0; i < body.Count - 1; i++)
            {
                if (head.IsHit(body[i]))
                {
                    return true; // Столкновение с хвостом
                }
            }
            return false;
        }

        // Метод для проверки столкновения змейки с границами стены
        // Этот метод проверяет, выйдет ли *будущая* голова змейки за пределы карты
        public bool CheckWallCollision(int mapWidth, int mapHeight)
        {
            Point head = GetNextHeadPosition(); // Получаем *следующую* позицию головы
            // Проверяем, выйдет ли голова за границы карты
            return head.X <= 0 || head.X >= mapWidth - 1 || head.Y <= 0 || head.Y >= mapHeight - 1;
        }

        // Метод для проверки, съела ли змейка еду
        public bool EatFood(Food food)
        {
            Point head = GetNextHeadPosition(); // Получаем следующую позицию головы
            if (head.IsHit(food.Position)) // Если следующая позиция головы совпадает с едой
            {
                body.Add(new Point(food.Position.X, food.Position.Y, snakeSymbol)); // Добавляем еду как новый сегмент тела
                return true;
            }
            return false;
        }
    }
}