using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Madu
{
    class SettingsOptions
    {
        public static string[] SettingsOptionsList = { "SPEED", "SYMBOL" };   // Список опций настроек

        // Варианты скорости в миллисекундах (меньше число = быстрее)
        public static int[] SpeedValues = { 300, 200, 100 };
        // Отображаемые имена для вариантов скорости
        public static string[] SpeedDisplayNames = { "SLOW", "FASTER", "FAST" };

        // Варианты символов для змейки, изменены на # и &
        public static string[] Symbol = { "#", "&" };

        // Настройки по умолчанию: скорость (индекс 1 = 200мс), символ (индекс 0 = "#")
        public static int[] DEFAULT = { 1, 0 };
    }
}