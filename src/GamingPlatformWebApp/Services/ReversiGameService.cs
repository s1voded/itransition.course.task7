using GamingPlatformWebApp.Models;
using System.Drawing;

namespace GamingPlatformWebApp.Services
{
    public class ReversiGameService : BaseBoardGame, IBoardGameService
    {
        public ReversiGameService() : base(_boardSize: 8)
        {
            movePossibleSymbol = '·';
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

        //dx    -1 0 +1
        //dy -1 NW N NE
        //    0 W  .  E
        //   +1 SW S SE
        //https://codereview.stackexchange.com/questions/236759/reversi-move-checker
        public bool MovePossible(GameMove gameMove, BoardItem boardItem)
        {
            var x = gameMove.Col;
            var y = gameMove.Row;

            for (int dx = -1; dx <= 1; dx++)
            {
                for (int dy = -1; dy <= 1; dy++)
                {
                    if (dx == 0 && dy == 0)
                        continue;

                    if (x + dx < 0 || x + dx > 7 || y + dy < 0 || y + dy > 7)
                        continue;

                    if (gameBoard[y + dy, x + dx] != OpponentItem)
                        continue;

                    int i = 2;
                    while (i <= 7)
                    {
                        if (x + i * dx < 0 || x + i * dx > 7 || y + i * dy < 0 || y + i * dy > 7)
                            break;
                        if (gameBoard[y + i * dy, x + i * dx] == EmptyItem)
                            break;
                        if (gameBoard[y + i * dy, x + i * dx] == boardItem)
                        {
                            return true;
                        }
                        i++;
                    }
                }
            }

            return false;
        }
    }
}
