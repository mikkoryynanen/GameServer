using System;
using System.Collections.Generic;
using GameSever.Services.PlayerData;
using Shared.Types;

namespace GameSever.Services.ClientPosition
{
    public class ClientPositionService : IClientPositionService
    {
        private Dictionary<string, Vector2D> _clientPositions = new Dictionary<string, Vector2D>();

        public ClientPositionService()
        {
        }

        public Vector2D GetPosition(string clientId)
        {
            return _clientPositions.TryGetValue(clientId, out var value) ? value : null;
        }

        public void SetPosition(string clientId, Vector2D positionVector2D)
        {
            if (_clientPositions.TryGetValue(clientId, out var value))
            {
                _clientPositions[clientId] = positionVector2D;
            }
            else
            {
                _clientPositions.Add(clientId, positionVector2D);
            }
            
            Console.WriteLine($"Updated {clientId} position to X: {positionVector2D.X} Y: {positionVector2D.Y}");
        }
    }
}