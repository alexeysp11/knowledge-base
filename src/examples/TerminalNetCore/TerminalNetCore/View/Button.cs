using System; 
using System.Linq;

namespace TerminalNetCore
{
    class Button : UI
    {
        // Initial state of cursor position for the entire BtnField. 
        public static new int InitCol { get; private set; }
        public static new int InitRow { get; private set; }

        // Properties of the object of this class.
        public string Name { get; private set; }
        public int BtnInitCol { get; private set; }
        public int BtnInitRow { get; private set; }
        public int Width { get; private set; }

        // In order to indicate where to set cursor. 
        private static int PrevCursorInitCol { get; set; }
        private static int PrevCursorInitRow { get; set; }
        private static int prevBtnWidth = 0; 
        private static int indBtn = 0; 
        
        public void Draw(string name) 
        {
            Name = name; 
            string line = String.Concat(Enumerable.Repeat("-", name.Length));

            // Set cursor position depending on index of the button. 
            if (indBtn == 0)
            {
                InitCol = Console.CursorLeft; 
                InitRow = Console.CursorTop; 
                BtnInitCol = InitCol; 
                BtnInitRow = InitRow; 
            }
            else
            {
                BtnInitCol = InitCol + PrevCursorInitCol + prevBtnWidth + 2; 
                BtnInitRow = InitRow; 
            }

            // Print out all lines that represents a button. 
            for (int i = 0; i < 3; i++)
            {
                Console.SetCursorPosition(
                    BtnInitCol, 
                    BtnInitRow + i
                );
                
                if (i != 1) // If current line is not 2nd. 
                {
                    System.Console.WriteLine($"+ {line} +");
                }
                else
                {
                    System.Console.WriteLine($"| {name} |");
                }
            }

            // Assign initial position of this button.
            PrevCursorInitCol = BtnInitCol; 
            PrevCursorInitRow = BtnInitRow;

            // Assign this button's width as a width of a previous button. 
            this.Width = name.Length + 4;
            prevBtnWidth = this.Width;

            // Index of a next button should be increased by one. 
            indBtn++; 
        }

        public bool IsPressed()
        {
            return true;
        }
    }
}