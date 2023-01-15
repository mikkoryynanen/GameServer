using ENet;

namespace GameSever.Services.Connection
{
    public interface IConnectionService
    {
        void AddPeer(Peer peer);
        void RemovePeer(Peer peer);
        Peer[] GetConnectedPeers();
        Peer GetPeer(int peerId);

    }
}