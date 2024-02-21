using GamingPlatformWebApp.Models;

namespace GamingPlatformWebApp.Services
{
    public class TicTacToeGameService
    {
        public const byte BoardSize = 3;
        public readonly BoardItem playerItem = BoardItem.X;
        public readonly BoardItem opponentItem = BoardItem.O;
        private byte countGameMoves;

        public GameResult Result { get; set; }

        public enum BoardItem
        {
            Empty,
            X,
            O,
        }

        private readonly BoardItem[,] gameBoard = new BoardItem[BoardSize, BoardSize];
        public BoardItem[,] GameBoard { get => gameBoard; }

        public TicTacToeGameService()
        {
            InitEmptyBoard();
        }

        public void InitEmptyBoard()
        {
            countGameMoves = 0;
            for (var i = 0; i < BoardSize; i++)
            {
                for (var j = 0; j < BoardSize; j++)
                {
                    gameBoard[i, j] = BoardItem.Empty;
                }
            }
        }

        public bool MakeGameMove(GameMove gameMove, BoardItem boardItem)
        {
            countGameMoves++;
            gameBoard[gameMove.Row, gameMove.Col] = boardItem;
            return CheckGameOver(gameMove, boardItem);
        }

        private bool CheckGameOver(GameMove gameMove, BoardItem boardItem)
        {
            byte rowItems = 0, colItems = 0, diagItems = 0, rdiagItems = 0;

            for (var i = 0; i < BoardSize; i++)
            {
                if (gameBoard[i, gameMove.Col] == boardItem) colItems++;
                if (gameBoard[gameMove.Row, i] == boardItem) rowItems++;
                if (gameBoard[i, i] == boardItem) diagItems++;
                if (gameBoard[i, (BoardSize - 1) - i] == boardItem) rdiagItems++;
            }

            if (colItems == BoardSize || rowItems == BoardSize || diagItems == BoardSize || rdiagItems == BoardSize)
            {
                Result = boardItem == playerItem ? GameResult.Win : GameResult.Lose;
                return true;
            }

            if (countGameMoves == Math.Pow(BoardSize, 2))
            {
                Result = GameResult.Draw;
                return true;
            }

            return false;
        }
    }
}
