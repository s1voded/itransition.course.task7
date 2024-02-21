using GamingPlatformWebApp.Data;
using GamingPlatformWebApp.Models;
using Microsoft.AspNetCore.SignalR;

namespace GamingPlatformWebApp.Hubs
{
    public class GameHub : Hub<IGameClient>
    {
        private readonly IGameRepository _gameRepository;

        public GameHub(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }

        public async Task CreateAndJoinGameRoom(GameType game, string player)
        {
            var gameRoom = _gameRepository.AddGameRoom(game, player);
            await SendAvailableGameRooms();
            await JoinGroup(gameRoom);
        }

        public async Task JoinGameRoom(int gameRoomId, string opponent)
        {
            var gameRoom = _gameRepository.JoinGameRoom(gameRoomId, opponent);
            if (gameRoom != null) await JoinGroup(gameRoom);
            await SendAvailableGameRooms();
        }

        private async Task JoinGroup(GameRoom gameRoom)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, gameRoom.Id.ToString());
            await Clients.Group(gameRoom.Id.ToString()).ReceiveCurrentGameRoom(gameRoom);
        }

        private async Task SendAvailableGameRooms()
        {
            var availableGameRooms = _gameRepository.GetAvailableGameRooms();
            await Clients.All.ReceiveAvailableGameRooms(availableGameRooms);
        }

        public async Task<List<GameRoom>> GetAvailableGameRooms()
        {
            var availableGameRooms = _gameRepository.GetAvailableGameRooms();
            return availableGameRooms;
        }

        public async Task DeleteGameRoom(GameRoom gameRoom)
        {
            _gameRepository.RemoveGameRoom(gameRoom);
            await SendAvailableGameRooms();
        }

        public async Task SendMoveToOpponent(int gameRoomId, GameMove gameMove)
        {
            await Clients.OthersInGroup(gameRoomId.ToString()).ReceiveOpponentMove(gameMove);
        }

        public async Task SendRestartGame(int gameRoomId)
        {
            await Clients.OthersInGroup(gameRoomId.ToString()).ReceiveRestartGame();
        }
    }

    public interface IGameClient
    {
        Task ReceiveAvailableGameRooms(List<GameRoom> availableGameRooms);
        Task ReceiveCurrentGameRoom(GameRoom gameRoom);
        Task ReceiveOpponentMove(GameMove gameMove);
        Task ReceiveRestartGame();
    }
}
