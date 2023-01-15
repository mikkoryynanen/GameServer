using System.Collections.Generic;
using Shared.Packets.Connection;
using Shared.Packets.Core.Handlers;

namespace GameSever.Handlers
{
    public class ClientConnectionPacketHandler : PacketHandlerBase<ClientConnectionPacket>
    {
        private Dictionary<byte, string> _connectedClients = new Dictionary<byte, string>();

        protected override void Handle(ClientConnectionPacket packet)
        {
            _connectedClients.Add((byte)_connectedClients.Count, packet.ClientId);
        }
    }
}