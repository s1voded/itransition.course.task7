﻿using GamingPlatformWebApp.Data;
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
        }

        private async Task JoinGroup(GameRoom gameRoom)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, gameRoom.Id.ToString());
            await Clients.Group(gameRoom.Id.ToString()).ReceiveMyGameRoom(gameRoom);
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

        public async Task SendMoveToOpponent(int gameRoomId, PlayerMove playerMove)
        {
            await Clients.OthersInGroup(gameRoomId.ToString()).ReceiveOpponentMove(playerMove);
        }

        public async Task SendResetGame(int gameRoomId)
        {
            await Clients.OthersInGroup(gameRoomId.ToString()).ReceiveResetGame();
        }
    }

    public interface IGameClient
    {
        Task ReceiveAvailableGameRooms(List<GameRoom> availableGameRooms);
        Task ReceiveMyGameRoom(GameRoom gameRoom);
        Task ReceiveOpponentMove(PlayerMove playerMove);
        Task ReceiveResetGame();
    }
}
