using GamingPlatformWebApp.Models;

namespace GamingPlatformWebApp.Services
{
    public interface IBoardGameService
    {
        public byte BoardSize { get; }
        public BoardItem[,] GameBoard { get; }
        public BoardItem PlayerItem { get; }
        public BoardItem OpponentItem { get; }
        public BoardItem EmptyItem { get; }
        public GameResult Result { get; }
        public char MovePossibleSymbol { get; }
        public void StartNewGame(bool isOpponent);
        public bool MakeGameMove(GameMove gameMove, BoardItem boardItem);

        public bool MovePossible(GameMove gameMove, BoardItem boardItem);
    }
}
