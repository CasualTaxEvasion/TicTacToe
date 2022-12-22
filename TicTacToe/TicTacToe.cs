namespace Games
{
    public class TicTacToe
    {
        public TicTacToe()
        {

        }

        public TicTacToe(TicTacToe ticTacToe)
        {
            xGrid = ticTacToe.xGrid;
            oGrid = ticTacToe.oGrid;
            IsDone = ticTacToe.IsDone;
            Turn = ticTacToe.Turn;
            Winner = ticTacToe.Winner;
        }

        private static int[] WinConditions { get; set; } = new int[]
        {
            0B_111_000_000,
            0B000_111_000,
            0B_000_000_111,
            0B_100_100_100,
            0B_010_010_010,
            0B_001_001_001,
            0B_100_010_001,
            0B_001_010_100,
        };

        //Ints cause faster
        internal int xGrid;
        internal int oGrid;

        public BlockType Winner { get; private set; } = BlockType.None;
        public bool IsDone { get; private set; } = false;
        public int Turn { get; private set; }

        public BlockType Player { get { return (BlockType)(Turn % 2); } }

        public bool SetValue(Vector2Int pos) => SetValue(pos.X, pos.Y);
        public bool SetValue(int x, int y)
        {
            //check if out of range
            if(x< 0 || y < 0) return false;
            if(x> 2|| y> 2) return false;

            int place = 1 << x + y * 3;

            //Checks if a space is occupied
            if (((oGrid | xGrid) & place) != 0)
                return false;

            //Set value
            if ((Turn & 1) == 1)
                oGrid |= place;
            else xGrid |= place;

            Turn++;

            CheckWin();

            return true;
        }
        public BlockType GetValue(int x, int y)
        {
            if((xGrid & (1 << ( x + y * 3))) != 0)
            {
                return BlockType.X;
            }
            else if ((oGrid & (1 << (x + y * 3))) != 0)
            {
                return BlockType.O;
            }

            return BlockType.None;
        }
        private void CheckWin()
        {
            for (int i = 0; i < WinConditions.Length; i++)
            {
                if ((WinConditions[i] & oGrid) == WinConditions[i])
                {
                    Winner = BlockType.O;
                    IsDone = true;
                    return;
                }
                if ((WinConditions[i] & xGrid) == WinConditions[i])
                {
                    Winner = BlockType.X;
                    IsDone = true;
                    return;
                }
            }

            if(Turn == 9)
            {
                IsDone = true;
                Winner = BlockType.None;
            }
        }

        //this is faster than creating a new object
        public void Reset()
        {
            xGrid = 0;
            oGrid = 0;
            IsDone = false;
            Winner= BlockType.None;
            Turn = 0;
        }
        public enum BlockType
        {
            X,
            O,
            None
        }
    } 
}


