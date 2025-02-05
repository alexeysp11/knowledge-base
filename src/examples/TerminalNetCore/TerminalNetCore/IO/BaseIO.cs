using System;

namespace TerminalNetCore
{
    class BaseIO
    {
        public static void ClearMainField()
        {
            BaseIO.ClearLine(
                UI.InitCol, 
                UI.InitRow + 5
            );
            BaseIO.ClearLine(
                UI.InitCol, 
                UI.InitRow + 6
            );
        }

        public static void ClearLine(int col, int row)
        {
            Console.SetCursorPosition(col, row);
            
            int currentLineCursor = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth)); 
            Console.SetCursorPosition(0, currentLineCursor);
        }

        public static void PrintInfoLine(string message)
        {
            BaseIO.ClearLine(
                UI.InitCol, 
                UI.InitRow + 5
            );

            BaseIO.ClearLine(
                UI.InitCol, 
                UI.InitRow + 6
            );

            Console.SetCursorPosition(
                UI.InitCol, 
                UI.InitRow + 5
            );

            System.Console.WriteLine(message);
        }
    }
}