using System;
using System.Collections.Generic;
using System.IO; // Для работы с файлами (рекорды)
using System.Linq; // Для LINQ (проще работа со списками)

namespace Madu
{
    // Класс, отвечающий за отображение и взаимодействие с меню
    public class Menu
    {
        private int selectedOptionIndex; // Индекс выбранной опции в главном меню
        private List<string> mainOptions; // Список опций главного меню

        public Menu()
        {
            mainOptions = new List<string> { "НАЧАТЬ ИГРУ", "НАСТРОЙКИ", "РЕКОРДЫ", "ВЫХОД" };
            selectedOptionIndex = 0; // По умолчанию выбрана первая опция
        }

        // Метод для отображения главного меню
        private void DisplayMainMenu()
        {
            Console.Clear(); // Очищаем консоль
            Console.SetWindowSize(80, 25); // Устанавливаем размер окна
            Console.SetBufferSize(80, 25); // Устанавливаем размер буфера
            Console.OutputEncoding = System.Text.Encoding.UTF8; // Устанавливаем кодировку для Unicode

            Console.SetCursorPosition(Console.WindowWidth / 2 - 5, Console.WindowHeight / 2 - 5);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("ИГРА \"ЗМЕЙКА\"");
            Console.ResetColor();

            // Отображаем каждую опцию меню
            for (int i = 0; i < mainOptions.Count; i++)
            {
                Console.SetCursorPosition(Console.WindowWidth / 2 - 7, Console.WindowHeight / 2 - 2 + i);
                if (i == selectedOptionIndex)
                {
                    Console.ForegroundColor = ConsoleColor.Green; // Выделяем выбранную опцию зеленым
                    Console.WriteLine($"> {mainOptions[i]} <");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.White; // Остальные опции белым
                    Console.WriteLine($"  {mainOptions[i]}  ");
                }
            }
            Console.ResetColor();
        }

        // Метод для отображения меню настроек
        private void DisplaySettingsMenu(int currentSelectedSetting, int currentSpeedIndex, int currentSymbolIndex)
        {
            Console.Clear();
            Console.SetWindowSize(80, 25);
            Console.SetBufferSize(80, 25);
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.SetCursorPosition(Console.WindowWidth / 2 - 5, Console.WindowHeight / 2 - 5);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("НАСТРОЙКИ");
            Console.ResetColor();

            // "Скорость"
            Console.SetCursorPosition(Console.WindowWidth / 2 - 10, Console.WindowHeight / 2 - 2);
            if (currentSelectedSetting == 0) Console.ForegroundColor = ConsoleColor.Green; else Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"СКОРОСТЬ: < {Settings.SpeedNames[currentSpeedIndex]} >");
            Console.ResetColor();

            // "Символ змейки"
            Console.SetCursorPosition(Console.WindowWidth / 2 - 10, Console.WindowHeight / 2 - 1);
            if (currentSelectedSetting == 1) Console.ForegroundColor = ConsoleColor.Green; else Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"СИМВОЛ: < {Settings.SnakeSymbols[currentSymbolIndex]} >");
            Console.ResetColor();

            // Опция "НАЗАД"
            Console.SetCursorPosition(Console.WindowWidth / 2 - 4, Console.WindowHeight / 2 + 2);
            if (currentSelectedSetting == 2) Console.ForegroundColor = ConsoleColor.Green; else Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("> НАЗАД <");
            Console.ResetColor();
        }

        // Метод для отображения рекордов
        private void DisplayHighScores()
        {
            Console.Clear();
            Console.SetWindowSize(80, 25);
            Console.SetBufferSize(80, 25);
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.SetCursorPosition(Console.WindowWidth / 2 - 4, Console.WindowHeight / 2 - 5);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("РЕКОРДЫ");
            Console.ResetColor();

            string scoresPath = "scores.txt";
            if (File.Exists(scoresPath))
            {
                string[] scores = File.ReadAllLines(scoresPath);
                // Отображаем до 10 лучших рекордов
                for (int i = 0; i < Math.Min(scores.Length, 10); i++)
                {
                    Console.SetCursorPosition(Console.WindowWidth / 2 - 15, Console.WindowHeight / 2 - 2 + i);
                    Console.WriteLine($"{i + 1}. {scores[i]}");
                }
            }
            else
            {
                Console.SetCursorPosition(Console.WindowWidth / 2 - 10, Console.WindowHeight / 2 - 2);
                Console.WriteLine("Пока нет рекордов.");
            }

            Console.SetCursorPosition(Console.WindowWidth / 2 - 12, Console.WindowHeight / 2 + 5);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Нажмите любую клавишу для возврата...");
            Console.ReadKey(true); // Ждем нажатия клавиши
            Console.ResetColor();
        }

        // Основной метод, который запускает меню и возвращает выбранные настройки игры
        public (int speedIndex, int symbolIndex) Show()
        {
            int currentSpeedIndex = Settings.DefaultSpeedIndex;
            int currentSymbolIndex = Settings.DefaultSymbolIndex;

            while (true)
            {
                DisplayMainMenu(); // Показываем главное меню
                ConsoleKeyInfo keyInfo = Console.ReadKey(true); // Считываем нажатую клавишу

                // Обработка навигации по главному меню
                if (keyInfo.Key == ConsoleKey.UpArrow)
                {
                    selectedOptionIndex = (selectedOptionIndex == 0) ? mainOptions.Count - 1 : selectedOptionIndex - 1;
                }
                else if (keyInfo.Key == ConsoleKey.DownArrow)
                {
                    selectedOptionIndex = (selectedOptionIndex == mainOptions.Count - 1) ? 0 : selectedOptionIndex + 1;
                }
                else if (keyInfo.Key == ConsoleKey.Enter) // Если нажата Enter
                {
                    string chosenOption = mainOptions[selectedOptionIndex];

                    if (chosenOption == "НАЧАТЬ ИГРУ")
                    {
                        // Возвращаем выбранные индексы скорости и символа
                        return (currentSpeedIndex, currentSymbolIndex);
                    }
                    else if (chosenOption == "НАСТРОЙКИ")
                    {
                        int currentSelectedSetting = 0; // 0: Скорость, 1: Символ, 2: Назад
                        while (true) // Цикл для меню настроек
                        {
                            DisplaySettingsMenu(currentSelectedSetting, currentSpeedIndex, currentSymbolIndex);
                            ConsoleKeyInfo settingKeyInfo = Console.ReadKey(true);

                            // Навигация по настройкам
                            if (settingKeyInfo.Key == ConsoleKey.UpArrow)
                            {
                                currentSelectedSetting = (currentSelectedSetting == 0) ? 2 : currentSelectedSetting - 1;
                            }
                            else if (settingKeyInfo.Key == ConsoleKey.DownArrow)
                            {
                                currentSelectedSetting = (currentSelectedSetting == 2) ? 0 : currentSelectedSetting + 1;
                            }
                            // Изменение значений настроек
                            else if (currentSelectedSetting == 0) // Изменение скорости
                            {
                                if (settingKeyInfo.Key == ConsoleKey.LeftArrow)
                                {
                                    currentSpeedIndex = (currentSpeedIndex == 0) ? Settings.SpeedValues.Length - 1 : currentSpeedIndex - 1;
                                }
                                else if (settingKeyInfo.Key == ConsoleKey.RightArrow)
                                {
                                    currentSpeedIndex = (currentSpeedIndex == Settings.SpeedValues.Length - 1) ? 0 : currentSpeedIndex + 1;
                                }
                            }
                            else if (currentSelectedSetting == 1) // Изменение символа
                            {
                                if (settingKeyInfo.Key == ConsoleKey.LeftArrow)
                                {
                                    currentSymbolIndex = (currentSymbolIndex == 0) ? Settings.SnakeSymbols.Length - 1 : currentSymbolIndex - 1;
                                }
                                else if (settingKeyInfo.Key == ConsoleKey.RightArrow)
                                {
                                    currentSymbolIndex = (currentSymbolIndex == Settings.SnakeSymbols.Length - 1) ? 0 : currentSymbolIndex + 1;
                                }
                            }
                            // Выход из меню настроек
                            else if (settingKeyInfo.Key == ConsoleKey.Enter && currentSelectedSetting == 2) // Если выбрано "Назад"
                            {
                                break; // Выходим из цикла настроек и возвращаемся в главное меню
                            }
                        }
                    }
                    else if (chosenOption == "РЕКОРДЫ")
                    {
                        DisplayHighScores();
                    }
                    else if (chosenOption == "ВЫХОД")
                    {
                        Environment.Exit(0); // Завершаем приложение
                    }
                }
            }
        }
    }
}