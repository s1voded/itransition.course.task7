namespace GamingPlatformWebApp.Models
{
    public abstract class BaseBoardGame
    {
        protected BaseBoardGame(short _boardSize)
        {
            boardSize = _boardSize;
            gameBoard = new BoardItem[boardSize, boardSize];
        }
        public BoardItem PlayerItem => BoardItem.X;
        public BoardItem OpponentItem => BoardItem.O;
        public BoardItem EmptyItem => BoardItem.Empty;

        protected short boardSize;
        public short BoardSize => boardSize;

        protected BoardItem[,] gameBoard;
        public BoardItem[,] GameBoard => gameBoard;

        protected char movePossibleSymbol;
        public char MovePossibleSymbol => movePossibleSymbol;

        protected GameResult result;
        public GameResult Result => result;

        public void InitEmptyBoard()
        {
            for (short i = 0; i < BoardSize; i++)
            {
                for (short j = 0; j < BoardSize; j++)
                {
                    gameBoard[i, j] = EmptyItem;
                }
            }
        }
    }
}
