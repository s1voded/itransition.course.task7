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

        //isOpponent not use for tic-tac-toe
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

        public bool MovePossible(GameMove gameMove, BoardItem boardItem)
        {
            return gameBoard[gameMove.Row, gameMove.Col] == EmptyItem;
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
                result = boardItem == PlayerItem ? GameResult.Win : GameResult.Lose;
                return true;
            }

            if (countGameMoves == Math.Pow(BoardSize, 2))
            {
                result = GameResult.Draw;
                return true;
            }

            return false;
        }
    }
}
