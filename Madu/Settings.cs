namespace Madu
{
    // Статический класс для хранения настроек игры
    public static class Settings
    {
        public static string[] SpeedNames = { "МЕДЛЕННО", "БЫСТРЕЕ", "БЫСТРО" }; // Названия скоростей для отображения в меню
        public static int[] SpeedValues = { 300, 200, 100 }; // Задержка в миллисекундах (чем меньше, тем быстрее)
        public static string[] SnakeSymbols = { "#", "&" }; // Символы для отображения змейки

        // Настройки по умолчанию: скорость (индекс 1 = БЫСТРЕЕ), символ (индекс 0 = #)
        public static int DefaultSpeedIndex = 1;
        public static int DefaultSymbolIndex = 0;
    }
}