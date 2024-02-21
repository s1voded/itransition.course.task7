namespace GamingPlatformWebApp.Models
{
    public enum GameType
    {
        TicTacToe,
        Reversi,
        Battleship
    }

    public enum GameResult
    {
        Win,
        Lose,
        Draw
    }

    public class GameRoom
    {
        public int Id { get; set; }
        public GameType Game { get; set; }
        public string? Player1 { get; set; }
        public string? Player2 { get; set; }
    }
}
