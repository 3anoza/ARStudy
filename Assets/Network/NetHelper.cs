using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Assets.Network
{
    public class NetHelper
    {
        private readonly UdpClient _client;
        private long _lostBytes = 0;
        public NetHelper()
        {
            _client = new UdpClient {EnableBroadcast = true};
            _client.Connect("localhost", 21);
        }

        public async Task SendPackageAsync(PackageType packageType, [NotNull] byte[] bytes)
        {
            if (bytes == null) 
                throw new ArgumentNullException(nameof(bytes));
            
            if (!Enum.IsDefined(typeof(PackageType), packageType))
                throw new InvalidEnumArgumentException(nameof(packageType), (int) packageType, typeof(PackageType));
            
            var package = new byte[bytes.Length + 1];
            package[0] = (byte) packageType;
            
            bytes.CopyTo(package, 1);

            _lostBytes += await _client.SendAsync(package, package.Length);
        }

        public async Task<byte[]> ReceivePackageAsync()
        {
            var package = new UdpReceiveResult();
            while (package.Buffer != null)
            {
                package = await _client.ReceiveAsync();
            }
            return package.Buffer;
        }
    }
}