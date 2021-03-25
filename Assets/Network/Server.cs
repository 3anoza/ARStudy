using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace ARStudy.Server
{
    public class Server
    {
        public const long DEFAULT_ADDRESS = 65663;
        public const int DEFAULT_PORT = 21;

        private readonly Socket _socket;
        private readonly EndPoint _endPoint;
        private readonly IPProtectionLevel _protectionLevel;
        private readonly FileSystem _fileSystem;
        private EndPoint _remoteEndPoint = null;
        public Server()
        {
            _socket = new Socket(SocketType.Dgram, ProtocolType.Udp);
            //_endPoint = new DnsEndPoint(DEFAULT_ADDRESS, DEFAULT_PORT);
            _protectionLevel = IPProtectionLevel.Restricted;
            _fileSystem = new FileSystem();
            SocketConfigure();
        }

        public Server([NotNull] Socket socket,
            [NotNull] EndPoint endPoint,
            IPProtectionLevel protectionLevel)
        {
            if (!Enum.IsDefined(typeof(IPProtectionLevel), protectionLevel))
                throw new InvalidEnumArgumentException(nameof(protectionLevel), (int) protectionLevel,
                    typeof(IPProtectionLevel));

            _socket = socket ?? throw new ArgumentNullException(nameof(socket));
            _endPoint = endPoint ?? throw new ArgumentNullException(nameof(endPoint));
            _protectionLevel = protectionLevel;
            _fileSystem = new FileSystem();
            SocketConfigure();
        }

        public Server([NotNull] EndPoint endPoint,
            SocketType sockType,
            ProtocolType protocol,
            IPProtectionLevel protectionLevel)
        {
            if (!Enum.IsDefined(typeof(SocketType), sockType))
                throw new InvalidEnumArgumentException(nameof(sockType), (int) sockType, typeof(SocketType));
            
            if (!Enum.IsDefined(typeof(ProtocolType), protocol))
                throw new InvalidEnumArgumentException(nameof(protocol), (int) protocol, typeof(ProtocolType));

            if (!Enum.IsDefined(typeof(IPProtectionLevel), protectionLevel))
                throw new InvalidEnumArgumentException(nameof(protectionLevel), (int) protectionLevel,
                    typeof(IPProtectionLevel));

            _socket = new Socket(sockType, protocol);
            _endPoint = endPoint ?? throw new ArgumentNullException(nameof(endPoint));
            _protectionLevel = protectionLevel;
            _fileSystem = new FileSystem();
            SocketConfigure();
        }

        private void SocketConfigure()
        {
            _socket.EnableBroadcast = true;
            //_socket.DualMode = false;
            //_socket.ExclusiveAddressUse = false;
            _socket.MulticastLoopback = true;
            _socket.ReceiveBufferSize = 4056;

            //_socket.SetIPProtectionLevel(_protectionLevel);
            _socket.Bind(new IPEndPoint(65663, 21));
        }

        public async Task ListenAsync()
        {
            while(true)
            {
                var package = new byte[]{};
                //var sock = await _socket.AcceptAsync();
                _socket.Receive(package);
                //var receiveResult = await _socket.ReceiveFromAsync(package, SocketFlags.Peek, _remoteEndPoint);
                //_remoteEndPoint = receiveResult.RemoteEndPoint;
                PackageType packageType;
                var data = UnpackPackage(package, out packageType);
                await PackageHandlerAsync(packageType, data);

            }
        }

        public byte[] Listen()
        {
            var package = new byte[4096];
            _socket.Receive(package);
            return package;
        }

        private byte[] UnpackPackage(byte[] package, out PackageType type)
        {
            type = (PackageType)Enum.GetValues(typeof(PackageType)).GetValue(package[0]);
            return package.Skip(1).ToArray();
        }

        private byte[] PackPackage(byte[] bytes, PackageType type)
        {
            var package = new byte[bytes.Length+1];
            package[0] = (byte) type;
            bytes.CopyTo(package, 1);
            return package;
        }

        private async Task PackageHandlerAsync(PackageType type, byte[] package)
        {
            switch (type)
            {
                case PackageType.File:
                    _fileSystem.SaveFile($"{package.GetHashCode() / 194599}.bm", package);
                    break;
                case PackageType.FileRequest:
                    await SendFile(package);
                    break;
                case PackageType.FileListRequest:
                    await SendFileList();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        private async Task SendFileList()
        {
            var data = Encoding.UTF8.GetBytes(string.Join(",",_fileSystem.GetFilesList()));
            var package = new ArraySegment<byte>(PackPackage(data, PackageType.Message));
            var lost = await _socket.SendToAsync(package, SocketFlags.Broadcast, _remoteEndPoint);
        }

        private async Task SendFile(byte[] bytes)
        {
            var data = _fileSystem.GetFileContent(Encoding.UTF8.GetString(bytes));
            var package = new ArraySegment<byte>(PackPackage(data, PackageType.File));
            var lost = await _socket.SendToAsync(package, SocketFlags.Broadcast, _remoteEndPoint);
        }
    }
}