using GameSever.Services.ClientPosition;
using Shared.Packets.Core.Handlers;
using Shared.Packets.Position;
using Shared.Types;

namespace GameSever.Handlers.Position
{
    public class Position2DPacketHandler : PacketHandlerBase<Position2DPacket>
    {
        private readonly IClientPositionService _positionService;

        public Position2DPacketHandler(IClientPositionService positionService)
        {
            _positionService = positionService;
        }
        
        protected override void Handle(Position2DPacket packet)
        {
            _positionService.SetPosition(packet.ClientId, new Vector2D(packet.X, packet.Y));
            
            // TODO Echo position change to all/other clients
            
            
        }
    }
}