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

        public bool MakePlayerMove(GameMove playerGameMove)
        {
            return MakeGameMove(playerGameMove, playerItem);
        }

        public bool MakeOpponentMove(GameMove opponentGameMove)
        {
            return MakeGameMove(opponentGameMove, opponentItem);
        }

        private bool MakeGameMove(GameMove gameMove, BoardItem boardItem)
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
