using System;

namespace TerminalNetCore
{
    public class UI
    {
        public static int InitCol { get; private set; }
        public static int InitRow { get; private set; }
        
        public virtual void Draw()
        {
            // Initialize current position of a cursor as its initial position. 
            InitCol = Console.CursorLeft; 
            InitRow = Console.CursorTop; 

            Field mainField = new Field();
            Field btnField = new Field();
            Field cmdField = new Field();

            mainField.Draw("Main");
            btnField.Draw("Button");
            cmdField.Draw("Command");
        }
    }
}