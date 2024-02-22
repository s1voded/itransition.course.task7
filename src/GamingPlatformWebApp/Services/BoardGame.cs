using GamingPlatformWebApp.Models;

namespace GamingPlatformWebApp.Services
{
    public abstract class BoardGame
    {
        public BoardItem PlayerItem => BoardItem.X;
        public BoardItem OpponentItem => BoardItem.O;
        public BoardItem EmptyItem => BoardItem.Empty;

        protected BoardItem[,] gameBoard;
        public BoardItem[,] GameBoard => gameBoard;

        protected GameResult result;
        public GameResult Result => result;
    }
}
