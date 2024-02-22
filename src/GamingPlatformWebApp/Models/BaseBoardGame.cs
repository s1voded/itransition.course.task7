namespace GamingPlatformWebApp.Models
{
    public abstract class BaseBoardGame
    {
        protected BaseBoardGame(byte _boardSize)
        {
            boardSize = _boardSize;
            gameBoard = new BoardItem[boardSize, boardSize];
        }

        protected byte boardSize;
        public byte BoardSize => boardSize;

        public BoardItem PlayerItem => BoardItem.X;
        public BoardItem OpponentItem => BoardItem.O;
        public BoardItem EmptyItem => BoardItem.Empty;

        protected BoardItem[,] gameBoard;
        public BoardItem[,] GameBoard => gameBoard;

        protected char movePossibleSymbol;
        public char MovePossibleSymbol => movePossibleSymbol;

        protected GameResult result;
        public GameResult Result => result;

        public void InitEmptyBoard()
        {
            for (var i = 0; i < BoardSize; i++)
            {
                for (var j = 0; j < BoardSize; j++)
                {
                    gameBoard[i, j] = EmptyItem;
                }
            }
        }
    }
}
