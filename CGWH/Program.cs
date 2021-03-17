using CGWH.Utilities;
using System;

namespace CGWH
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.Title = Cheat.NAME;
            Console.CursorVisible = false;

            DebugUtility.Log("\n\n");
            WriteWithColor("CGWH - Started", ConsoleColor.Yellow, true);
            WriteWithColor($"> [VD] = {Cheat.VERSION_DATE} [VT] = {Cheat.VERSION_TIME}\n\n", ConsoleColor.White, true);

            Cheat cheat = new Cheat();
            ESP esp = new ESP(cheat);
            if (cheat.IsValidVersion())
            {
                if (cheat.IsGetProcess())
                {
                    WriteWithColor("Sucsesfully working!", ConsoleColor.Green, true);
                    esp.LoadESP();
                }
                else
                    WriteWithColor("Process '" + Cheat.PROCESS_NAME + "' was not found!", ConsoleColor.Red, true);
            }
            else
                WriteWithColor("Version of game is not valid", ConsoleColor.Red, true);

            Console.ForegroundColor = ConsoleColor.Black;
            Console.ReadLine();
        }


        internal static void WriteWithColor(string text, ConsoleColor color = ConsoleColor.White)
        {
            WriteWithColor(text, color, false);
        }
        internal static void WriteWithColor(string text, ConsoleColor color = ConsoleColor.White, bool log = false)
        {
            ConsoleColor oldColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            if (log)
                DebugUtility.Log(text);
            Console.ForegroundColor = oldColor;
        }
    }
}
