using System;
using ENet;
using GameSever.Handlers;
using GameSever.Handlers.Position;
using GameSever.Services.ClientPosition;
using GameSever.Utils;
using Microsoft.Extensions.DependencyInjection;
using Shared.Packets.Core;
using Shared.Serializer;

namespace GameSever
{
    public class Server
    {
        public Server(IServiceProvider serviceProvider)
        {
            Library.Initialize();

            var packetProcessor = new PacketProcessor();
            packetProcessor.AddHandler(new ClientConnectionPacketHandler());
            packetProcessor.AddHandler(new Position2DPacketHandler(serviceProvider.GetService<IClientPositionService>()));
            
            using (Host server = new Host()) {
                Address address = new Address();

                var envVariables = EnvironmentParameters.GetEnvironmentVariables();
                if (!envVariables.HasValue)
                {
                    return;
                }
                address.Port = envVariables.Value.Port;
                server.Create(address, envVariables.Value.MaxClients);

                Console.WriteLine($"Server running on {address.GetIP()}:{envVariables.Value.Port}");

                Event netEvent;

                while (!Console.KeyAvailable) {
                    bool polled = false;

                    while (!polled) {
                        if (server.CheckEvents(out netEvent) <= 0) {
                            if (server.Service(15, out netEvent) <= 0)
                                break;

                            polled = true;
                        }

                        switch (netEvent.Type) {
                            case EventType.None:
                                break;

                            case EventType.Connect:
                                Console.WriteLine("Client connected - ID: " + netEvent.Peer.ID + ", IP: " + netEvent.Peer.IP);
                                break;

                            case EventType.Disconnect:
                                Console.WriteLine("Client disconnected - ID: " + netEvent.Peer.ID + ", IP: " + netEvent.Peer.IP);
                                break;

                            case EventType.Timeout:
                                Console.WriteLine("Client timeout - ID: " + netEvent.Peer.ID + ", IP: " + netEvent.Peer.IP);
                                break;

                            case EventType.Receive:
                                Console.WriteLine("Packet received from - ID: " + netEvent.Peer.ID + ", IP: " + netEvent.Peer.IP + ", Channel ID: " + netEvent.ChannelID + ", Data length: " + netEvent.Packet.Length);
                                
                                byte[] buffer = new byte[netEvent.Packet.Length];
                                netEvent.Packet.CopyTo(buffer);
                                
                                IPacket deserializedPacket = Serializer.Deserialize<IPacket>(buffer);
                                packetProcessor.Process(deserializedPacket);
                                
                                netEvent.Packet.Dispose();
                                break;
                        }
                    }
                }

                server.Flush();
            }
            
            Library.Deinitialize();
        }
    }
}