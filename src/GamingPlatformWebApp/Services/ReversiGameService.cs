using GamingPlatformWebApp.Models;

namespace GamingPlatformWebApp.Services
{
    public class ReversiGameService : BaseBoardGame, IBoardGameService
    {
        public ReversiGameService() : base(_boardSize: 8)
        {
            StartNewGame();
        }

        public void StartNewGame()
        {
            InitEmptyBoard();

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
