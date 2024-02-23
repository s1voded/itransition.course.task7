using GamingPlatformWebApp.Models;

namespace GamingPlatformWebApp.Services
{
    public class TicTacToeGameService : BaseBoardGame, IBoardGameService
    {
        private byte countGameMoves;

        public TicTacToeGameService() : base(_boardSize: 3)
        {
            movePossibleSymbol = ' ';
        }

        public void StartNewGame(bool isOpponent)
        {
            countGameMoves = 0;
            InitEmptyBoard();
        }

        public bool MakeGameMove(GameMove gameMove, BoardItem boardItem)
        {
            countGameMoves++;
            gameBoard[gameMove.Row, gameMove.Col] = boardItem;
            return CheckGameOver(gameMove, boardItem);
        }

        public bool IsValidMove(GameMove gameMove, BoardItem boardItem)
        {
            return gameBoard[gameMove.Row, gameMove.Col] == EmptyItem;
        }

        private bool CheckGameOver(GameMove gameMove, BoardItem boardItem)
        {
            byte rowItems = 0, colItems = 0, diagItems = 0, rdiagItems = 0;

            for (var i = 0; i < boardSize; i++)
            {
                if (gameBoard[i, gameMove.Col] == boardItem) colItems++;
                if (gameBoard[gameMove.Row, i] == boardItem) rowItems++;
                if (gameBoard[i, i] == boardItem) diagItems++;
                if (gameBoard[i, (boardSize - 1) - i] == boardItem) rdiagItems++;
            }

            if (colItems == boardSize || rowItems == boardSize || diagItems == boardSize || rdiagItems == boardSize)
            {
                result = boardItem == PlayerItem ? GameResult.Win : GameResult.Lose;
                return true;
            }

            if (countGameMoves == Math.Pow(boardSize, 2))
            {
                result = GameResult.Draw;
                return true;
            }

            return false;
        }

        public string GetIntermediateResult()
        {
            return "";
        }
    }
}
