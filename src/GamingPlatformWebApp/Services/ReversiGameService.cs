using GamingPlatformWebApp.Models;

namespace GamingPlatformWebApp.Services
{
    public class ReversiGameService : BaseBoardGame, IBoardGameService
    {
        private ushort playerItemsCount;
        private ushort opponentItemsCount;
        private ushort emptyItemsCount;
        public ReversiGameService() : base(_boardSize: 8)
        {
            movePossibleSymbol = 'x';
        }

        public void StartNewGame(bool isOpponent)
        {
            InitEmptyBoard();

            var center = boardSize / 2;

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

            CountBoardItems();
        }

        public bool MakeGameMove(GameMove gameMove, BoardItem boardItem)
        {
            MakeMove(gameMove, boardItem);

            if (!HasAnyValidMove(boardItem == PlayerItem ? OpponentItem : PlayerItem))//game over
            {
                if (playerItemsCount != opponentItemsCount)
                {
                    result = boardItem == PlayerItem
                        ? playerItemsCount > opponentItemsCount ? GameResult.Win : GameResult.Lose
                        : opponentItemsCount > playerItemsCount ? GameResult.Lose : GameResult.Win;
                }
                else result = GameResult.Draw;
                return true;
            }
            else
            {
                return false;
            }
        }

        public string GetIntermediateResult()
        {
            return $"{PlayerItem}:{playerItemsCount} - {OpponentItem}:{opponentItemsCount}";
        }

        private void CountBoardItems()
        {
            playerItemsCount = opponentItemsCount = 0;
            for (var i = 0; i < boardSize; i++)
                for (var j = 0; j < boardSize; j++)
                {
                    if (gameBoard[i, j] == PlayerItem)
                    {
                        playerItemsCount++;
                    }
                    else if (gameBoard[i, j] == OpponentItem)
                    {
                        opponentItemsCount++;
                    }
                    else
                        emptyItemsCount++;
                }
        }

        //partially use and adapt the code from here: https://www.codeproject.com/Articles/4672/Reversi-in-C
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
                        while (gameBoard[r, c] != EmptyItem && gameBoard[r, c] != boardItem)
                        {
                            gameBoard[r, c] = boardItem;
                            r += dr;
                            c += dc;
                        }
                    }

            // Update the counts.
            CountBoardItems();
        }

        private bool IsOutflanking(BoardItem boardItem, int row, int col, int dr, int dc)
        {
            // Move in the given direction as long as we stay on the board and
            // land on a disc of the opposite color.
            var r = row + dr;
            var c = col + dc;
            while (r >= 0 && r < boardSize && c >= 0 && c < boardSize && gameBoard[r, c] != EmptyItem && gameBoard[r, c] != boardItem)
            {
                r += dr;
                c += dc;
            }

            // If we ran off the board, only moved one space or didn't land on
            // a disc of the same color, return false.
            if (r < 0 || r > boardSize - 1 || c < 0 || c > boardSize - 1 || (r - dr == row && c - dc == col) || gameBoard[r, c] != boardItem)
                return false;

            // Otherwise, return true;
            return true;
        }

        public bool IsValidMove(GameMove gameMove, BoardItem boardItem)
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

        public bool HasAnyValidMove(BoardItem boardItem)
        {
            // Check all board positions for a valid move.
            short r, c;
            for (r = 0; r < boardSize; r++)
                for (c = 0; c < boardSize; c++)
                    if (IsValidMove(new GameMove { Row = r, Col = c }, boardItem))
                        return true;

            // None found.
            return false;
        }
    }
}
