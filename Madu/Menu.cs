using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Madu
{
    class Menu
    {
        private static string[] menu_options = { "START", "SETTINGS", "SCORE", "EXIT" }; // Опции главного меню

        public List<int> ShowOptions()
        {
            while (true) // Бесконечный цикл для отображения меню
            {
                int choice = Keyboard.ChooseOption("Main Menu", menu_options); // Получаем выбор пользователя из меню
                switch (choice) // Обрабатываем выбор
                {
                    case 0: // Если выбрано "START"
                        return SettingsOptions.DEFAULT.ToList(); // Возвращаем настройки по умолчанию

                    case 1: // Если выбрано "SETTINGS"
                        return Keyboard.ChooseOptionSettings("SETTINGS"); // Показываем меню настроек и возвращаем выбранные параметры

                    case 2: // Если выбрано "SCORE"
                        Score(); // Показываем таблицу рекордов
                        break;

                    case 3: // Если выбрано "EXIT"
                        Environment.Exit(0); // Завершаем приложение
                        break;

                    default: // В случае ошибки
                        Console.WriteLine("error"); // Выводим сообщение об ошибке
                        break;
                }
            }
        }

        public static int GetSpeed(List<int> choises)
        {
            // Возвращаем выбранную скорость из SpeedValues
            return SettingsOptions.SpeedValues[choises[0]];
        }

        public static string GetSymbol(List<int> choises)
        {
            return SettingsOptions.Symbol[choises[1]]; // Возвращаем выбранный символ змейки
        }

        public void Score()
        {
            Console.Clear(); // Очищаем консоль
            string path = "scores.txt"; // Путь к файлу рекордов

            if (File.Exists(path)) // Если файл существует
            {
                string[] lines = File.ReadAllLines(path); // Читаем все строки из файла
                Console.WriteLine("Scores:\n"); // Выводим заголовок "Scores"

                foreach (string line in lines) // Для каждой строки в файле
                {
                    Console.WriteLine(line); // Выводим строку (рекорд)
                }
            }
            else // Если файл не существует
            {
                Console.WriteLine("file made"); // Сообщаем о создании файла (это произойдет при первом сохранении)
            }

            Console.WriteLine("\nPress any key..."); // Просим пользователя нажать любую клавишу
            Console.ReadKey(); // Ждем нажатия клавиши
        }
    }
}