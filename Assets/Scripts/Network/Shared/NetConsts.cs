namespace Assets.Scripts.Network.Shared
{
    public class NetConsts
    {
        public const string DEBUG_IP_ADDRESS = "127.0.0.1";
        public const int SOCKET_ALREADY_CONNECTED = 10056;
        public const int DEBUG_HOST = 1024;
        /// <summary>
        /// message buffer size that mean how much bytes can content 1 message
        /// </summary>
        public const int BUFFER_SIZE = 6_291_456; // equals 6MB
    }
}