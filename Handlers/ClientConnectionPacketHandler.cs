using System;
using GameSever.Services.Connection;
using Shared.Packets.Connection;
using Shared.Packets.Core.Handlers;

namespace GameSever.Handlers
{
    public class ClientConnectionPacketHandler : PacketHandlerBase<ClientConnectionPacket>
    {
        private readonly IConnectionService _connectionService;

        public ClientConnectionPacketHandler(IConnectionService connectionService)
        {
            _connectionService = connectionService;
        }
        
        protected override void Handle(ClientConnectionPacket packet)
        {
            Console.WriteLine(packet);
        }
    }
}