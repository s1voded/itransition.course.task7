namespace GamingPlatformWebApp.Models
{
    public enum GameType
    {
        TicTacToe, Reversi//, Battleship
    }

    public enum GameResult
    {
        Draw, Win, Lose
    }

    public enum BoardItem
    {
        Empty, X, O
    }

    public class GameRoom
    {
        public int Id { get; set; }
        public GameType Game { get; set; }
        public string? Player1 { get; set; }
        public string? Player2 { get; set; }
    }
}
