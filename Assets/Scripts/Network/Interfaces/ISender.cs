using System.Net.Sockets;

namespace Assets.Scripts.Network.Interfaces
{
    public interface ISender
    {
        void SendPackage(NetworkStream stream, byte[] package);
        byte[] ReceivePackage(NetworkStream stream);
    }
}