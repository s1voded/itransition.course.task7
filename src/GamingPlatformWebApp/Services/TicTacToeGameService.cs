using GamingPlatformWebApp.Models;

namespace GamingPlatformWebApp.Services
{
    public class TicTacToeGameService
    {
        public const byte BoardSize = 3;
        private readonly BoardItem playerItem = BoardItem.X;
        private readonly BoardItem opponentItem = BoardItem.O;

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
            for (var i = 0; i < BoardSize; i++)
            {
                for (var j = 0; j < BoardSize; j++)
                {
                    gameBoard[i, j] = BoardItem.Empty;
                }
            }
        }

        public bool MakePlayerMove(PlayerMove playerMove)
        {
            return MakeGameMove(playerMove, playerItem);
        }

        public bool MakeOpponentMove(PlayerMove opponentMove)
        {
            return MakeGameMove(opponentMove, opponentItem);
        }

        private bool MakeGameMove(PlayerMove gameMove, BoardItem boardItem)
        {
            gameBoard[gameMove.Row, gameMove.Col] = boardItem;
            return GameResult();
        }

        private bool GameResult()
        {
            return false;
        }
    }
}
