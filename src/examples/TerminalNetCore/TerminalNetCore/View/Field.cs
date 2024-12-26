using System; 
using System.Linq;
using System.Collections.Generic;

namespace TerminalNetCore
{
    public class Field 
    {
        public void Draw(string field)
        {
            switch (field)
            {
                case "Main": // MainField was called.
                    break;
                case "Button": // BtnField was called. 
                    Button load = new Button();
                    Button snake = new Button();
                    Button exit = new Button();
                    
                    load.Draw("Load");
                    snake.Draw("Snake");
                    exit.Draw("Exit");

                    List<Button> buttons = new List<Button>();
                    buttons.Add(load);
                    buttons.Add(snake);
                    buttons.Add(exit);

                    SaveElements.ConvertIntoJsonString(buttons);

                    break;
                case "Command": // CmdField was called. 
                    break;
            }
        }
    }
}