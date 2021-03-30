using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading;
using Assets.Scripts.Network.Interfaces;
using Assets.Scripts.Network.Shared;
using JetBrains.Annotations;

namespace Assets.Scripts.Network.Client
{
    /// <inheritdoc />
    public class ClientSender : ISender
    {
        /// <summary>
        /// Sends a packet to the network stream associated with the server
        /// </summary>
        /// <param name="stream">the network stream that is connected to the server and through which packets are transmitted</param>
        /// <param name="package">collected data packet (or not if not disclosed)</param>
        /// <exception cref="ArgumentNullException">The parameter <see cref="stream"/> or <see cref="package"/> is <see langword="null"/></exception>
        /// <exception cref="ArgumentException">The parameter <see cref="package"/> is empty</exception>
        public void SendPackage([NotNull] NetworkStream stream, [NotNull] byte[] package)
        {
            if (stream == null) throw new ArgumentNullException(nameof(stream));
            if (package == null) throw new ArgumentNullException(nameof(package));
            if (package.Length == 0)
                throw new ArgumentException("Value cannot be an empty collection.", nameof(package));
            var bytesLeft = 0;
            while (bytesLeft < package.Length)
            {
                if (stream.DataAvailable) continue;
                if (bytesLeft + NetConsts.BUFFER_SIZE > package.Length)
                    stream.Write(package, bytesLeft, package.Length - bytesLeft);
                stream.Write(package, bytesLeft, NetConsts.BUFFER_SIZE);
                bytesLeft += NetConsts.BUFFER_SIZE;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="stream">network stream, which is connected to the server and through which packets are received</param>
        public byte[] ReceivePackage([NotNull] NetworkStream stream)
        {
            var package = new List<byte>();
            if (stream == null) throw new ArgumentNullException(nameof(stream));
            do
            {
                var tempBuf = new byte[NetConsts.BUFFER_SIZE];
                stream.Read(tempBuf, 0, tempBuf.Length);
                package.AddRange(tempBuf);
            } while (stream.DataAvailable);
            return package.ToArray();
        }
    }
}