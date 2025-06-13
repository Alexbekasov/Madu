using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading; // Для Thread.Sleep
using System.IO; // Для работы с файлами

namespace Madu
{
    class Program
    {
        // Размеры игрового поля (консоли)
        const int MAP_WIDTH = 80;
        const int MAP_HEIGHT = 25;

        static void Main(string[] args)
        {
            Console.Title = "Игра Змейка"; // Устанавливаем заголовок окна консоли

            // Главный цикл игры, который позволяет перезапускать игру после "Game Over"
            while (true)
            {
                Menu menu = new Menu();
                // Показываем меню и получаем выбранные пользователем настройки
                (int speedIndex, int symbolIndex) gameSettings = menu.Show();

                int gameSpeed = Settings.SpeedValues[gameSettings.speedIndex]; // Получаем задержку (скорость) игры
                string snakeSymbol = Settings.SnakeSymbols[gameSettings.symbolIndex]; // Получаем символ для змейки

                Console.Clear(); // Очищаем консоль перед началом игры

                // Запрос имени игрока
                Console.SetCursorPosition(MAP_WIDTH / 2 - 10, MAP_HEIGHT / 2 - 2);
                Console.Write("Введите ваше имя: ");
                string playerName = Console.ReadLine();
                playerName = string.IsNullOrEmpty(playerName) ? "Игрок" : playerName; // Имя по умолчанию, если не введено

                int currentScore = 0; // Начальный счет игры

                // Настраиваем окно консоли
                Console.SetWindowSize(MAP_WIDTH, MAP_HEIGHT);
                Console.SetBufferSize(MAP_WIDTH, MAP_HEIGHT);
                Console.OutputEncoding = System.Text.Encoding.UTF8; // Важно для корректного отображения символов

                // Создаем и отрисовываем стены игрового поля
                Walls walls = new Walls(MAP_WIDTH, MAP_HEIGHT);
                walls.Draw();

                // Создаем змейку. Начальная позиция головы (10, 10) - это безопасное место.
                Snake snake = new Snake(10, 10, 4, snakeSymbol); // Змейка длиной 4, символ из настроек
                snake.Draw(); // Отрисовываем змейку

                // Создаем еду
                Food food = new Food("*"); // Еда будет символом "*"
                food.GenerateNewPosition(MAP_WIDTH, MAP_HEIGHT, snake.GetAllBodyPoints(), walls.GetAllWallPoints());
                food.Draw(); // Отрисовываем еду

                bool isGameOver = false; // Флаг состояния игры

                // Главный игровой цикл
                while (!isGameOver)
                {
                    // Отображаем информацию об игроке и счете в верхней части экрана
                    Console.SetCursorPosition(2, 0); // Чуть отступаем от края
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write($"Игрок: {playerName} | Счет: {currentScore}     "); // Пробелы для очистки старого счета
                    Console.ResetColor();

                    // Проверяем ввод пользователя (нажатие клавиш)
                    if (Console.KeyAvailable)
                    {
                        ConsoleKeyInfo key = Console.ReadKey(true); // Считываем клавишу без отображения
                        snake.ChangeDirection(key.Key); // Меняем направление змейки
                    }

                    // Проверяем столкновения ЗМЕЙКИ
                    // 1. Со стенами (рамкой карты) - проверка теперь внутри Snake.CheckWallCollision
                    // 2. С самой собой (хвостом)
                    if (snake.CheckWallCollision(MAP_WIDTH, MAP_HEIGHT) || snake.CheckSelfCollision())
                    {
                        isGameOver = true; // Если столкновение, игра окончена
                        continue; // Переходим к экрану Game Over
                    }

                    // Проверяем, съела ли змейка еду
                    if (snake.EatFood(food))
                    {
                        currentScore++; // Увеличиваем счет

                        // В этой версии нет дополнительных стен, но можно добавить здесь
                        // if (currentScore % 5 == 0) { // Добавляем стену каждые 5 очков }

                        // Генерируем новую еду
                        food.GenerateNewPosition(MAP_WIDTH, MAP_HEIGHT, snake.GetAllBodyPoints(), walls.GetAllWallPoints());
                        food.Draw(); // Отрисовываем новую еду
                    }
                    else
                    {
                        snake.Move(); // Змейка просто движется, если еда не была съедена
                    }

                    Thread.Sleep(gameSpeed); // Пауза для контроля скорости игры
                }

                // Экран "Игра окончена"
                Console.Clear();
                Console.SetCursorPosition(MAP_WIDTH / 2 - 10, MAP_HEIGHT / 2 - 2);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("ИГРА ОКОНЧЕНА!");
                Console.SetCursorPosition(MAP_WIDTH / 2 - 10, MAP_HEIGHT / 2);
                Console.WriteLine($"Ваш счет: {currentScore}");
                Console.ResetColor();

                // Сохранение рекорда в файл
                string scoresPath = "scores.txt";
                using (StreamWriter writer = new StreamWriter(scoresPath, true, Encoding.UTF8)) // Используем UTF8 для записи
                {
                    writer.WriteLine($"{playerName} - {currentScore}");
                }

                Console.SetCursorPosition(MAP_WIDTH / 2 - 15, MAP_HEIGHT / 2 + 3);
                Console.WriteLine("Нажмите любую клавишу для возврата в меню...");
                Console.ReadKey(true); // Ждем нажатия клавиши
                Console.Clear(); // Очищаем консоль перед новым запуском
            }
        }
    }
}