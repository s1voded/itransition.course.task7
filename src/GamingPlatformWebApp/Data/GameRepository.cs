using GamingPlatformWebApp.Models;

namespace GamingPlatformWebApp.Data
{
    public class GameRepository: IGameRepository
    {
        private static readonly List<GameRoom> gameRooms = [];
        private static int id = 1;
        private static int GenerateId => id++;

        public GameRoom AddGameRoom(GameType game, string player)
        {
            var gameRoom = new GameRoom() { Id = GenerateId, Game = game, Player1 = player };
            gameRooms.Add(gameRoom);
            return gameRoom;
        }

        public GameRoom? JoinGameRoom(int gameRoomId, string opponent)
        {
            var gameRoom = gameRooms.FirstOrDefault(g => g.Id == gameRoomId);
            if (gameRoom != null) gameRoom.Player2 = opponent;
            return gameRoom;
        }

        public void RemoveGameRoom(GameRoom gameRoom) 
        {
            var gameRoomForRemove = gameRooms.FirstOrDefault(g => g.Id == gameRoom.Id);
            if (gameRoomForRemove != null) gameRooms.Remove(gameRoomForRemove); 
        }

        public List<GameRoom> GetAvailableGameRooms()
        {
            var availableGameRooms = gameRooms.Where(g => g.Player2 == null).ToList();
            return availableGameRooms;
        }
    }

    public interface IGameRepository
    {
        GameRoom AddGameRoom(GameType game, string player);
        GameRoom? JoinGameRoom(int gameRoomId, string opponent);
        void RemoveGameRoom(GameRoom gameRoom);
        List<GameRoom> GetAvailableGameRooms();
    }
}
