using Games;
using System.Threading;

namespace TicTacToeCons
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var tictactoe = new TicTacToeConsole();

            tictactoe.PlayerX = TicTacToeConsole.TicTacToePlayer.Human;
            tictactoe.PlayerO = TicTacToeConsole.TicTacToePlayer.AI;
            tictactoe.Restart();

            while (true)
            {
                tictactoe.Run();
                Thread.Sleep(1000);
            }
        }
    }
}
