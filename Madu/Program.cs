using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Madu
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Figure> extraWalls = new List<Figure>(); // Список дополнительных стен

            while (true) // Бесконечный цикл для перезапуска игры
            {
                Menu menu = new Menu(); // Создаем объект меню
                List<int> a = menu.ShowOptions(); // Показываем меню и получаем выбранные настройки
                int Sp = Menu.GetSpeed(a); // Получаем скорость из настроек
                string Sy = Menu.GetSymbol(a); // Получаем символ змейки из настроек

                Console.Clear(); // Очищаем консоль

                Console.Write("Name: "); // Просим ввести имя
                string Name = Console.ReadLine(); // Считываем имя игрока
                int score = 0; // Инициализируем счет игры

                Console.SetWindowSize(80, 25); // Устанавливаем размер окна консоли
                Console.SetBufferSize(80, 25); // Устанавливаем размер буфера консоли
                Console.OutputEncoding = System.Text.Encoding.UTF8; // Устанавливаем кодировку для поддержки Unicode символов

                Walls walls = new Walls(80, 25); // Создаем объект стен
                walls.Draw(); // Рисуем стены

                // Изменено начальное положение змейки, чтобы она не врезалась в стену сразу.
                // Начальная точка p = (20,10) - это хвост. Голова будет на (20 - (длина-1), 10).
                // Для длины 4, голова будет на (20-3, 10) = (17,10), что безопасно.
                Point p = new Point(20, 10, Sy); // Создаем начальную точку для змейки (дальше от края)
                Snake snake = new Snake(p, 4, Directions.RIGHT); // Создаем объект змейки
                snake.Draw(); // Рисуем змейку
                Console.ResetColor(); // Сбрасываем цвет консоли после рисования змейки

                // Set food color before drawing
                Console.ForegroundColor = ConsoleColor.Red; // Устанавливаем красный цвет для еды
                FoodCreator foodCreator = new FoodCreator(80, 25, "●"); // Создаем создателя еды, используем символ "●"
                Point food = foodCreator.CreateFood(snake.GetPoints(), walls.GetPoints()); // Создаем еду
                food.Draw(); // Рисуем еду
                Console.ResetColor(); // Сбрасываем цвет консоли на стандартный после рисования еды


                bool isGameOver = false; // Флаг окончания игры

                while (!isGameOver) // Цикл игры
                {
                    if (walls.IsHit(snake) || snake.IsHitTail()) // Проверяем столкновение со стенами или хвостом
                    {
                        isGameOver = true; // Устанавливаем флаг окончания игры
                        continue; // Переходим к следующей итерации цикла (выйдем из игрового цикла)
                    }

                    bool hitExtraWall = false; // Флаг столкновения с дополнительной стеной
                    foreach (var wall in extraWalls) // Проходим по всем дополнительным стенам
                    {
                        if (wall.IsHit(snake)) // Если змейка столкнулась с дополнительной стеной
                        {
                            hitExtraWall = true; // Устанавливаем флаг
                            break; // Выходим из цикла
                        }
                    }
                    if (hitExtraWall) // Если было столкновение с дополнительной стеной
                        break; // Выходим из игрового цикла

                    if (snake.Eat(food)) // Если змейка съела еду
                    {
                        score++; // Увеличиваем счет
                        // Set food color before creating new food
                        Console.ForegroundColor = ConsoleColor.Red; // Устанавливаем красный цвет для новой еды
                        if (score % 3 == 0) // Каждые 3 очка
                        {
                            // wall!
                            Random rnd = new Random(); // Создаем объект Random
                            int x = rnd.Next(2, 74); // Генерируем случайную X-координату для новой стены
                            int y = rnd.Next(2, 23); // Генерируем случайную Y-координату для новой стены

                            HorizontalLine newWall = new HorizontalLine(x, x + 4, y, "#"); // Создаем новую горизонтальную стену
                            extraWalls.Add(newWall); // Добавляем стену в список
                            newWall.Draw(); // Рисуем новую стену
                        }

                        do // Цикл для создания новой еды
                        {
                            food = foodCreator.CreateFood(snake.GetPoints(), walls.GetPoints()); // Создаем новую еду, избегая змейки и стен
                        } while (snake.IsHit(food) || walls.GetPoints().Any(p => p.IsHit(food))); // Повторяем, если еда появилась на змейке или стене

                        food.Draw(); // Рисуем новую еду
                        Console.ResetColor(); // Сбрасываем цвет консоли после рисования еды
                    }
                    else
                    {
                        snake.Move(); // Змейка движется
                    }

                    // Display score with a distinct color
                    Console.SetCursorPosition(0, 0); // Устанавливаем курсор в верхний левый угол
                    Console.ForegroundColor = ConsoleColor.White; // Устанавливаем белый цвет для текста счета
                    Console.Write($"Player: {Name} | Score: {score} "); // Выводим имя игрока и счет
                    Console.ResetColor(); // Сбрасываем цвет текста

                    Thread.Sleep(Sp); // Задержка для контроля скорости игры

                    if (Console.KeyAvailable) // Если нажата клавиша
                    {
                        ConsoleKeyInfo key = Console.ReadKey(true); // Считываем клавишу
                        snake.HandleKey(key.Key); // Обрабатываем нажатие клавиши для управления змейкой
                    }
                }

                Console.Clear(); // Очищаем консоль
                Console.SetCursorPosition(30, 10); // Устанавливаем позицию курсора
                Console.WriteLine("DEAD. Your score: " + score); // Выводим сообщение об окончании игры и финальный счет

                string path = "scores.txt"; // Путь к файлу рекордов

                using (StreamWriter writer = new StreamWriter(path, true)) // Открываем файл для записи (в режиме добавления)
                {
                    writer.WriteLine(Name + " - " + score); // Записываем имя игрока и его счет в файл
                }

                Console.WriteLine("\n                                 Press any key"); // Просим нажать любую клавишу
                Console.ReadKey(); // Ждем нажатия клавиши
                Console.Clear(); // Очищаем консоль перед следующим циклом игры
            }
        }
    }
}