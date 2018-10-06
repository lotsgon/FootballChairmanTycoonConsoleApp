using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FootballChairmanTycoonConsoleApp.MenuInterface
{
    public static class Menu
    {
        public static void ShowMenu()
        {
            //Console.SetCursorPosition(0, 0);
            Console.Write($"Main Menu\n" +
            $"\n1. Create\n" +
            $"2. Edit\n" +
            $"3. Delete\n" +
            $"4. List\n" +
            $"5. File\n" +
            $"6. Close\n");

        }

        public static void Line()
        {
            string lineyBoy = String.Concat(Enumerable.Repeat("_", Console.WindowWidth));
            Console.Write("\n{0}\n", lineyBoy);
        }
        public static void ClearCurrentConsoleLine()
        {
            int currentLineCursor = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, currentLineCursor);
        }
    }
}
