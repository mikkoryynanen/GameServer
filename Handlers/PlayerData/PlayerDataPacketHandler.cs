using System;
using ENet;
using GameSever.Services.Connection;
using GameSever.Services.PlayerData;
using Shared.Packets.Core.Handlers;
using Shared.Packets.PlayerData;
using Shared.Serializer;

namespace GameSever.Handlers.PlayerData
{
    public class PlayerDataPacketHandler : PacketHandlerBase<PlayerInventoryRequest>
    {
        private readonly IPlayerDataService _playerDataService;
        private readonly Host _host;

        public PlayerDataPacketHandler(IPlayerDataService playerDataService, Host host)
        {
            _playerDataService = playerDataService;
            _host = host;
        }
        
        protected override void Handle(PlayerInventoryRequest packet)
        {
            var items = _playerDataService.GetItems(packet.PlayerId);
            
            Packet enetPacket = default(Packet);
            byte[] data = Serializer.Serialize(new PlayerInventoryResponse { Items = items });
            enetPacket.Create(data, PacketFlags.Reliable);

            Peer peer = ConnectionService.GetPeer(packet.PlayerId);
            _host.Broadcast(0, ref enetPacket, peer);
        }
    }
}