using System;
using System.Net.Sockets;
using Assets.Scripts.Network.Shared;
using JetBrains.Annotations;

namespace Assets.Scripts.Network.Client
{
    public class ClientDeploy
    {
        #region Injections

        private readonly TcpClient _tcpClient;
        private ClientSender _sender;

        /// <summary>Connect tcp client to server and send init package </summary>
        /// <exception cref="SocketException" >An error has occurred, the connection has already been established</exception>
        /// <exception cref="SocketException" >An error has occurred, the server is not available</exception>
        /// <param name="tcpClient"></param>
        /// <param name="sender"></param>
        public ClientDeploy([NotNull] TcpClient tcpClient, [NotNull] ClientSender sender)
        {
            _tcpClient = tcpClient ?? throw new ArgumentNullException(nameof(tcpClient));
            _sender = sender ?? throw new ArgumentNullException(nameof(sender));
            SetConnection_Test();
            SendConnectionRequest();
        }

        #endregion

        #region TEST deployer private methods

        public TcpClient Deploy()
        {
            return _tcpClient;
        }

        private void SetConnection_Test()
        {
            if (_tcpClient.Connected)
                throw new SocketException(NetConsts.SOCKET_ALREADY_CONNECTED);
            _tcpClient.Connect(NetConsts.DEBUG_IP_ADDRESS, NetConsts.DEBUG_HOST);
        }

        private void SendConnectionRequest()
        {
            _sender.SendPackage(_tcpClient.GetStream(), PackagePacker.PackPackage(new byte[0], PackageType.ConnectionRequest));
        }

        #endregion
    }
}