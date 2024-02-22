using GamingPlatformWebApp.Models;
using System.Drawing;

namespace GamingPlatformWebApp.Services
{
    public class ReversiGameService : BaseBoardGame, IBoardGameService
    {
        public ReversiGameService() : base(_boardSize: 8)
        {
            movePossibleSymbol = '·';
        }

        public void StartNewGame(bool isOpponent)
        {
            InitEmptyBoard();

            var center = BoardSize / 2;

            if (isOpponent)
            {
                gameBoard[center - 1, center] = gameBoard[center, center - 1] = PlayerItem;
                gameBoard[center - 1, center - 1] = gameBoard[center, center] = OpponentItem;
            }
            else
            {
                gameBoard[center - 1, center] = gameBoard[center, center - 1] = OpponentItem;
                gameBoard[center - 1, center - 1] = gameBoard[center, center] = PlayerItem;
            }
        }

        public bool MakeGameMove(GameMove gameMove, BoardItem boardItem)
        {
            MakeMove(gameMove, boardItem);
            return false;
        }

        //https://www.codeproject.com/Articles/4672/Reversi-in-C
        private void MakeMove(GameMove gameMove, BoardItem boardItem)
        {
            // Set the disc on the square.
            gameBoard[gameMove.Row, gameMove.Col] = boardItem;

            // Flip any flanked opponents.
            int dr, dc;
            int r, c;
            for (dr = -1; dr <= 1; dr++)
                for (dc = -1; dc <= 1; dc++)
                    // Are there any outflanked opponents?
                    if (!(dr == 0 && dc == 0) && IsOutflanking(boardItem, gameMove.Row, gameMove.Col, dr, dc))
                    {
                        r = gameMove.Row + dr;
                        c = gameMove.Col + dc;
                        // Flip 'em.
                        while (gameBoard[r, c] != EmptyItem && gameBoard[r, c] != boardItem)//s1voded fix
                        {
                            gameBoard[r, c] = boardItem;
                            r += dr;
                            c += dc;
                        }
                    }

            // Update the counts.
            //this.UpdateCounts();
        }

        private bool IsOutflanking(BoardItem boardItem, int row, int col, int dr, int dc)
        {
            // Move in the given direction as long as we stay on the board and
            // land on a disc of the opposite color.
            int r = row + dr;
            int c = col + dc;
            while (r >= 0 && r < 8 && c >= 0 && c < 8 && gameBoard[r, c] != EmptyItem && gameBoard[r, c] != boardItem)//s1voded fix
            {
                r += dr;
                c += dc;
            }

            // If we ran off the board, only moved one space or didn't land on
            // a disc of the same color, return false.
            if (r < 0 || r > 7 || c < 0 || c > 7 || (r - dr == row && c - dc == col) || gameBoard[r, c] != boardItem)
                return false;

            // Otherwise, return true;
            return true;
        }

        public bool MovePossible(GameMove gameMove, BoardItem boardItem)
        {
            // The square must be empty.
            if (gameBoard[gameMove.Row, gameMove.Col] != EmptyItem)
                return false;

            // Must be able to flip at least one opponent disc.
            int dr, dc;
            for (dr = -1; dr <= 1; dr++)
                for (dc = -1; dc <= 1; dc++)
                    if (!(dr == 0 && dc == 0) && this.IsOutflanking(boardItem, gameMove.Row, gameMove.Col, dr, dc))
                        return true;

            // No opponents could be flipped.
            return false;
        }

        //dx    -1 0 +1
        //dy -1 NW N NE
        //    0 W  .  E
        //   +1 SW S SE
        //https://codereview.stackexchange.com/questions/236759/reversi-move-checker
        /*public bool MovePossible(GameMove gameMove, BoardItem boardItem)
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
        }*/
    }
}
