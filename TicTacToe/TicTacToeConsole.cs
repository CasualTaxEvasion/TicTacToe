using System;
using System.Threading;

namespace Games
{
    public class TicTacToeConsole
    {
        public const string VERSION = "1.0";
        private const int AI_SLEEPTIMER = 500;
        private readonly TicTacToe instance;

        public TicTacToePlayer PlayerX { get; set; } = TicTacToePlayer.AI;
        public TicTacToePlayer PlayerO { get; set; } = TicTacToePlayer.AI;
        public TicTacToeConsole()
        {
            instance = new TicTacToe();
            Console.Clear();
            Refresh();
        }
        private void Refresh()
        {
            Console.CursorVisible = false;
            Console.SetCursorPosition(0, 0);

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n  _____ _     _____         _____          \n |_   _(_) __|_   _|_ _  __|_   _|__   ___ \n   | | | |/ __|| |/ _` |/ __|| |/ _ \\ / _ \\\n   | | | | (__ | | (_| | (__ | | (_) |  __/\n   |_| |_|\\___||_|\\__,_|\\___||_|\\___/ \\___|");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"   -By Casual Tax Evasion V{VERSION}\n\n");
            char[,] _ = new char[3,3];
            for (int x = 0; x<3; x++)
                for(int y = 0; y<3; y++)
                {
                    switch (instance.GetValue(x, y))
                    {
                        case TicTacToe.BlockType.X:
                            _[x, y] = 'X';
                            break;
                        case TicTacToe.BlockType.O:
                            _[x, y] = 'O';
                            break;
                        case TicTacToe.BlockType.None:
                            _[x, y] = ' ';
                            break;
                            
                    }
                }

            Console.WriteLine("        _____________________________ " +
                             "\n       |         |         |         |" +
                             "\n       |         |         |         |" +
                            $"\n       |    {_[0,2]}    |    {_[1, 2]}    |    {_[2, 2]}    |" +
                             "\n       |         |         |         |" +
                             "\n       |_________|_________|_________|" +
                             "\n       |         |         |         |" +
                             "\n       |         |         |         |" +
                            $"\n       |    {_[0, 1]}    |    {_[1, 1]}    |    {_[2, 1]}    |" +
                             "\n       |         |         |         |" +
                             "\n       |_________|_________|_________|" +
                             "\n       |         |         |         |" +
                             "\n       |         |         |         |" +
                            $"\n       |    {_[0, 0]}    |    {_[1, 0]}    |    {_[2, 0]}    |" +
                             "\n       |         |         |         |" +
                             "\n       |_________|_________|_________|\n");

            if((instance.Player == TicTacToe.BlockType.X ? PlayerX : PlayerO) == TicTacToePlayer.Human)
            {
                Console.WriteLine("Commands:\n set [x] [y]\n");
            }
            else
            {
                Console.WriteLine("\n\n");
            }
            

            Console.CursorVisible = true;
        }
        public void Run()
        {
            if (instance.IsDone)
            {
                Restart();
                return;
            }

            var player = instance.Player == TicTacToe.BlockType.X ? PlayerX : PlayerO;
            
            switch (player) 
            {
                case TicTacToePlayer.AI:
                    Thread.Sleep(AI_SLEEPTIMER);
                    instance.SetValue(TicTacToeAI.GetBestMove(instance));
                    break;
                case TicTacToePlayer.Human:
                    ParseInput(instance);
                    break;
                default:
                    break;
            }

            Refresh();

            if (instance.IsDone)
            {
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor= ConsoleColor.Black;
                Console.WriteLine($"               Winner was {instance.Winner}               ");
                Console.ResetColor();
                return;
            }

            Run();
        }
        public void Restart()
        {
            instance.Reset();
            Console.Clear();
            Refresh();
        }
        private void ParseInput(TicTacToe instance)
        {
            string[] input = Console.ReadLine().ToLower().Split(' ');

            //Clear input line
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, Console.CursorTop - 1);

            if (input.Length == 0)
            {
                ParseInput(instance);           
            }
            
            switch(input[0]) 
            {
                case "set":
                    if(input.Length != 3) 
                        ParseInput(instance);

                    if (int.TryParse(input[1], out int x) && int.TryParse(input[2], out int y) && instance.SetValue(x-1, y-1))
                    {
                        return;
                    }

                    break;
                case "restart":
                    instance.Reset();
                    return;
            }

           
            ParseInput(instance);
        }
        public enum TicTacToePlayer
        {
            AI,
            Human,
        }
    }
}
