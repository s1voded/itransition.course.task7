using GamingPlatformWebApp.Models;

namespace GamingPlatformWebApp.Services
{
    public class ReversiGameService: BoardGame, IBoardGameService
    {
        private readonly byte boardSize = 8;
        public byte BoardSize => boardSize;

        public ReversiGameService()
        {
            gameBoard = new BoardItem[boardSize, boardSize];
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

            var center = BoardSize / 2;
            gameBoard[center - 1, center] = gameBoard[center, center - 1] = PlayerItem;
            gameBoard[center - 1, center - 1] = gameBoard[center, center] = OpponentItem;
        }

        public bool MakeGameMove(GameMove gameMove, BoardItem boardItem)
        {
            gameBoard[gameMove.Row, gameMove.Col] = boardItem;
            return false;
        }
    }
}
