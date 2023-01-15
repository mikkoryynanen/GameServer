using System;
using Shared.Packets;
using Shared.Packets.Core.Handlers;

namespace GameSever.Handlers
{
    public class TestPacketHandler : PacketHandlerBase<TestPacket>
    {
        protected override void Handle(TestPacket packet)
        {
            Console.WriteLine($"Received message: {packet.Message}");
        }
    }
}