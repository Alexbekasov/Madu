using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Madu
{
    class Keyboard
    {
        public static int ChooseOption(string title, string[] options)
        {
            int selectedIndex = 0; // Индекс выбранного элемента
            ConsoleKey key; // Переменная для хранения нажатой клавиши

            do
            {
                Console.Clear(); // Очищаем консоль
                Console.SetCursorPosition(35, 7); // Устанавливаем позицию курсора для заголовка меню
                Console.WriteLine(title); // Выводим заголовок
                Console.SetCursorPosition(35, 8); // Устанавливаем позицию курсора
                Console.WriteLine(new string('~', title.Length)); // Выводим разделительную линию

                for (int i = 0; i < options.Length; i++) // Проходим по всем опциям
                {
                    if (i == selectedIndex) // Если текущая опция выбрана
                    {
                        Console.SetCursorPosition(33, 10 + (2 * i)); // Устанавливаем позицию курсора для выбранной опции
                        Console.ForegroundColor = ConsoleColor.Green; // Устанавливаем зеленый цвет текста
                        Console.WriteLine("-> " + options[i]); // Выводим выбранную опцию со стрелкой
                        Console.ResetColor(); // Сбрасываем цвет текста
                    }
                    else
                    {
                        Console.SetCursorPosition(33, 10 + (2 * i)); // Устанавливаем позицию курсора для невыбранной опции
                        Console.ForegroundColor = ConsoleColor.DarkGray; // Устанавливаем темно-серый цвет текста для невыбранной опции
                        Console.WriteLine("   " + options[i]); // Выводим невыбранную опцию
                        Console.ResetColor(); // Сбрасываем цвет текста
                    }
                }

                key = Console.ReadKey(true).Key; // Считываем нажатую клавишу

                switch (key) // Обрабатываем нажатую клавишу
                {
                    case ConsoleKey.UpArrow: // Если нажата стрелка вверх
                        selectedIndex = (selectedIndex - 1 + options.Length) % options.Length; // Перемещаем выбор вверх
                        break;
                    case ConsoleKey.DownArrow: // Если нажата стрелка вниз
                        selectedIndex = (selectedIndex + 1) % options.Length; // Перемещаем выбор вниз
                        break;
                    case ConsoleKey.Enter: // Если нажата Enter
                        return selectedIndex; // Возвращаем выбранный индекс
                }

            } while (true); // Повторяем, пока не будет нажата Enter

        }

        /////////////////////////////////////// 

        public static List<int> ChooseOptionSettings(string title)
        {
            int selectedIndex = 0; // Индекс выбранного элемента
            ConsoleKey key; // Переменная для хранения нажатой клавиши
            int Sp = 1; // Индекс для скорости (по умолчанию FASTER)
            int Sy = 0; // Индекс для символа змейки (по умолчанию "█")

            List<int> optionslist = new List<int> { SettingsOptions.SpeedValues.Length, SettingsOptions.Symbol.Length }; // Список длин опций
            List<int> param = new List<int> { Sp, Sy }; // Список выбранных параметров (скорость, символ)

            do
            {

                Console.Clear(); // Очищаем консоль
                Console.SetCursorPosition(35, 7); // Устанавливаем позицию курсора для заголовка настроек
                Console.WriteLine(title); // Выводим заголовок
                Console.SetCursorPosition(35, 8); // Устанавливаем позицию курсора
                Console.WriteLine(new string('~', title.Length)); // Выводим разделительную линию

                for (int i = 0; i < SettingsOptions.SettingsOptionsList.Length; i++) // Проходим по всем опциям настроек
                {
                    if (i == selectedIndex) // Если текущая опция выбрана
                    {
                        Console.SetCursorPosition(33, 10 + (2 * i)); // Устанавливаем позицию курсора для выбранной опции
                        Console.ForegroundColor = ConsoleColor.Green; // Устанавливаем зеленый цвет текста
                        Console.WriteLine("   " + SettingsOptions.SettingsOptionsList[i]); // Выводим выбранную опцию
                        Console.SetCursorPosition(48, 10 + (2 * i)); // Устанавливаем позицию курсора для значения опции
                        if (i == 0) // Если это опция скорости
                        { Console.WriteLine("< " + SettingsOptions.SpeedDisplayNames[Sp] + " >"); } // Выводим текущую скорость по имени
                        else if (i == 1) // Если это опция символа
                        { Console.WriteLine("< " + SettingsOptions.Symbol[Sy] + " >"); } // Выводим текущий символ змейки

                        Console.ResetColor(); // Сбрасываем цвет текста

                    }
                    else // Если опция не выбрана
                    {
                        Console.SetCursorPosition(33, 10 + (2 * i)); // Устанавливаем позицию курсора
                        Console.ForegroundColor = ConsoleColor.DarkGray; // Устанавливаем темно-серый цвет текста
                        Console.WriteLine("   " + SettingsOptions.SettingsOptionsList[i]); // Выводим опцию
                        Console.SetCursorPosition(48, 10 + (2 * i)); // Устанавливаем позицию курсора для значения
                        if (i == 0) // Если это опция скорости
                        { Console.WriteLine("< " + SettingsOptions.SpeedDisplayNames[Sp] + " >"); } // Выводим текущую скорость по имени
                        else if (i == 1) // Если это опция символа
                        { Console.WriteLine("< " + SettingsOptions.Symbol[Sy] + " >"); } // Выводим текущий символ змейки
                        Console.ResetColor(); // Сбрасываем цвет текста
                    }
                }


                key = Console.ReadKey(true).Key; // Считываем нажатую клавишу
                switch (key) // Обрабатываем нажатую клавишу
                {
                    case ConsoleKey.UpArrow: // Если нажата стрелка вверх
                        selectedIndex = (selectedIndex - 1 + SettingsOptions.SettingsOptionsList.Length) % SettingsOptions.SettingsOptionsList.Length; // Перемещаем выбор вверх

                        break;
                    case ConsoleKey.DownArrow: // Если нажата стрелка вниз
                        selectedIndex = (selectedIndex + 1) % SettingsOptions.SettingsOptionsList.Length; // Перемещаем выбор вниз

                        break;
                    case ConsoleKey.LeftArrow: // Если нажата стрелка влево
                        if (param[selectedIndex] > 0) // Если значение параметра не минимальное
                        { param[selectedIndex]--; } // Уменьшаем значение параметра
                        Sp = param[0]; // Обновляем скорость
                        Sy = param[1]; // Обновляем символ змейки
                        break;
                    case ConsoleKey.RightArrow: // Если нажата стрелка вправо
                        if (param[selectedIndex] < optionslist[selectedIndex] - 1) // Если значение параметра не максимальное
                        { param[selectedIndex]++; } // Увеличиваем значение параметра
                        Sp = param[0]; // Обновляем скорость
                        Sy = param[1]; // Обновляем символ змейки
                        break;
                    case ConsoleKey.Enter: // Если нажата Enter
                        return param; // Возвращаем список выбранных параметров

                }
            }
            while (true); // Повторяем, пока не будет нажата Enter
        }
    }
}