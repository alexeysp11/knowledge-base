using System;

namespace TerminalNetCore
{
    public class Terminal
    {
        private bool isExit = false; 

        public void Run()
        {
            this.DrawUI();
            
            while (true)
            {
                if (isExit == true)
                {
                    break;
                }

                this.CallKeyboardHandler();
            }
        }

        private void DrawUI()
        {
            UI ui = new UI();
            ui.Draw();
        }

        private void CallKeyboardHandler()
        {
            // Handle if user pressed some key. 
            KeyboardHandler handler = new KeyboardHandler();
            string key = handler.HandleKeys();

            // Invoke necessary process. 
            if (key == "Load")
            {
                this.CallProcessBar();
            }
            else if (key == "Snake")
            {
                this.CallSnakeGame();
            }
            else if (key == "Exit")
            {
                this.Exit();
            }
        }

        private void CallProcessBar()
        {
            // Define cursor position. 
            int col = UI.InitCol;
            int row = UI.InitRow + 5;
            
            BaseIO.ClearMainField();
            Console.SetCursorPosition(col, row);

            var load = new Downloader.ProcessBar();
            load.Run(col, row);
        }

        private void CallSnakeGame()
        {
            BaseIO.ClearMainField();
            Console.SetCursorPosition(
                UI.InitCol, 
                UI.InitRow + 5
            );
            
            var snake = new SnakeGame.Game();
            snake.Run();
        }
        
        private void Exit()
        {
            DB.TerminalDB.DeleteJsonFile();
            BaseIO.PrintInfoLine("Terminal.Exit() was invoked.");
            isExit = true; 
        }
    }
}