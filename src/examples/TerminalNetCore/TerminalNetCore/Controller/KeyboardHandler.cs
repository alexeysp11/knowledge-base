using System;
using System.Collections.Generic;

namespace TerminalNetCore
{
    class KeyboardHandler
    {
        public string HandleKeys()
        {
            ConsoleKey choice;

            do
            {
                choice = Console.ReadKey(true).Key; 
                switch (choice)
                {
                    case ConsoleKey.Enter:
                        List<DB.ElementUI> buttons = DB.TerminalDB.ReadJsonFile(); 

                        foreach (var button in buttons)
                        {
                            bool isRighterLeftBorder = Console.CursorLeft >= button.Left;
                            bool isLefterRightBorder = Console.CursorLeft <= button.Right;
                            bool isBelowTopBorder = Console.CursorTop >= button.Top;
                            bool isAboveButtomBorder = Console.CursorTop <= button.Bottom;
                            
                            if (isRighterLeftBorder && isLefterRightBorder 
                                && isBelowTopBorder && isAboveButtomBorder)
                            {
                                return button.Name;
                            }
                        }

                        break;
                        
                    case ConsoleKey.UpArrow:
                        this.MoveCursor("Up");
                        
                        break;
                    
                    case ConsoleKey.DownArrow:
                        this.MoveCursor("Down");

                        break;
                    
                    case ConsoleKey.LeftArrow:
                        this.MoveCursor("Left");
                        
                        break;

                    case ConsoleKey.RightArrow:
                        this.MoveCursor("Right");
                        
                        break;
                }
            } while (true);
        }

        private void MoveCursor(string direction)
        {
            // Go up. 
            if (direction == "Up")
            {
                if (Console.CursorTop > UI.InitRow)
                {
                    Console.SetCursorPosition(
                        Console.CursorLeft, 
                        Console.CursorTop - 1
                    );
                }
            }

            // Gow down. 
            if (direction == "Down")
            {
                if (Console.CursorTop < UI.InitRow + 20)
                {
                    Console.SetCursorPosition(
                        Console.CursorLeft, 
                        Console.CursorTop + 1
                    );
                }
            }

            // Go left.
            if (direction == "Left")
            {
                if (Console.CursorLeft > 0)
                {
                    Console.SetCursorPosition(
                        Console.CursorLeft - 1, 
                        Console.CursorTop
                    );   
                }
            }

            // Go right. 
            if (direction == "Right")
            {
                if (Console.CursorLeft < 50)
                {
                    Console.SetCursorPosition(
                        Console.CursorLeft + 1, 
                        Console.CursorTop
                    );
                }
            }
        }

        private void ChangeField()
        {
            
        }
    }
}