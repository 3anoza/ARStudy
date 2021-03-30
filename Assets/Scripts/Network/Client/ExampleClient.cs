using System;
using System.Net.Sockets;
using Assets.Scripts.Network.Interfaces;
using JetBrains.Annotations;

namespace Assets.Scripts.Network.Client
{
    public class ExampleClient : ICommunicate
    {
        private readonly TcpClient _client;
        private readonly ClientSender _sender;

        public ExampleClient()
        {
            _sender = new ClientSender();
            var clientDeploy = new ClientDeploy(new TcpClient(), _sender);
            _client = clientDeploy.Deploy();
        }

        /// <inheritdoc />
        public void SendMessage(string message)
        {
            if (message == null) throw new ArgumentNullException(nameof(message));
            if (string.IsNullOrEmpty(message))
                throw new ArgumentException("Value cannot be null or empty.", nameof(message));
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public void SendFile(string filename, byte[] fileBytes)
        {
            if (filename == null) throw new ArgumentNullException(nameof(filename));
            if (fileBytes == null) throw new ArgumentNullException(nameof(fileBytes));
            if (string.IsNullOrEmpty(filename))
                throw new ArgumentException("Value cannot be null or empty.", nameof(filename));
            if (fileBytes.Length == 0)
                throw new ArgumentException("Value cannot be an empty collection.", nameof(fileBytes));
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        [CanBeNull]
        public byte[] RequestFile(string filename)
        {
            if (filename == null) throw new ArgumentNullException(nameof(filename));
            if (string.IsNullOrEmpty(filename))
                throw new ArgumentException("Value cannot be null or empty.", nameof(filename));
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        [NotNull]
        public string RequestFileList()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public void SubscribeToFilesUpdate(Action callback)
        {
            if (callback == null) throw new ArgumentNullException(nameof(callback));
            throw new NotImplementedException();
        }
    }
}