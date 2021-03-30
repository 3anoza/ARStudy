namespace Assets.Scripts.Network.Shared
{
    public enum PackageType
    {
        /// <summary>
        /// request to connect to the server and register on its network
        /// </summary>
        ConnectionRequest = 0x01,
        /// <summary>
        /// received / sent package is a simple message
        /// </summary>
        Message = 0x03,
        /// <summary>
        /// request to track changes in the available server network
        /// </summary>
        Subscribe = 0x02,
        /// <summary>
        /// received / sent packet is a bunch of file name and its internal information
        /// </summary>
        File = 0x04,
        /// <summary>
        /// request to the server to get a list of files from the server network
        /// </summary>
        FileListRequest = 0x06,
        /// <summary>
        /// request to the server to send a file
        /// </summary>
        FileRequest = 0x05,
    }
}