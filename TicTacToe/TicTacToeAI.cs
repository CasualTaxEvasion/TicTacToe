using System;
using System.Collections.Generic;
using System.Linq;
using static Games.TicTacToe;

namespace Games
{
    internal static class TicTacToeAI
    {
        private struct Move : IComparable
        {
            int X, Y;
            public Vector2Int Vector => new Vector2Int(X, Y);

            public int Rating;

            public Move(int x, int y, int rating)
            {
                X = x;
                Y = y;
                Rating = rating;
            }

            public int CompareTo(object obj)
            {
                var _ = (Move)obj;
                return Rating.CompareTo(_.Rating);

            }
        }

        public static Vector2Int GetBestMove(TicTacToe instance)
        {
            if (instance.Turn == 0)
                return new Vector2Int(new Random().Next(0, 3), new Random().Next(0, 3));

            var player = instance.Player;

            List<Move> moves = new List<Move>();

            for (int y = 0; y < 3; y++)
            {
                for (int x = 0; x < 3; x++)
                {
                    if (instance.GetValue(x, y) == BlockType.None)
                    {
                        var newInst = new TicTacToe(instance);
                        newInst.SetValue(x, y);
                        var value = MinMax(newInst, player, 0);
                        moves.Add(new Move(x, y, value));
                    }
                }
            }

            return moves.Max().Vector;
        }

        private static int MinMax(TicTacToe instance, BlockType player, int depth)
        {
            if (instance.IsDone)
            {
                if (instance.Winner == BlockType.None)
                    return 0;

                if (instance.Winner == player)
                    return 10 - depth;

                return -10;
            }

            List<int> scores = new List<int>();

            for (int y = 0; y < 3; y++)
            {
                for (int x = 0; x < 3; x++)
                {
                    if(instance.GetValue(x, y) == BlockType.None)
                    {
                        var newInst = new TicTacToe(instance);
                        newInst.SetValue(x, y);
                        scores.Add(MinMax(newInst, player, depth + 1));
                    }
                }
            }

            if(instance.Player == player)
            {
                return scores.Max();
            }
            else
            {
                return scores.Min();
            }
        }
    }
}