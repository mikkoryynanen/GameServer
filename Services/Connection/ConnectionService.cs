using System.Collections.Generic;
using ENet;

namespace GameSever.Services.Connection
{
    public static class ConnectionService
    {
        // private Dictionary<byte, int> _connectedClients = new Dictionary<byte, int>();
        private static List<Peer> _connectedClients = new List<Peer>();

        public static void AddPeer(Peer peer)
        {
            _connectedClients.Add(peer);
        }

        public static void RemovePeer(Peer peer)
        {
            _connectedClients.Remove(peer);
        }

        public static Peer[] GetConnectedPeers()
        {
            return _connectedClients.ToArray();
        }

        public static Peer GetPeer(int peerId)
        {
            return _connectedClients.Find(peer => peer.GetHashCode() == peerId);
        }
    }
}