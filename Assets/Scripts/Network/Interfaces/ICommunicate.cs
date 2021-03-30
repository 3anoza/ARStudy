using System;
using JetBrains.Annotations;

namespace Assets.Scripts.Network.Interfaces
{
    public interface ICommunicate
    {
        /// <summary>
        /// Send message to server
        /// </summary>
        /// <param name="message">any message in text form</param>
        /// <exception cref="ArgumentNullException">The parameter <see cref="message"/> is <see langword="null"/></exception>
        /// <exception cref="ArgumentException">The parameter <exception cref="message"> is empty</exception></exception>
        void SendMessage([NotNull] string message);

        /// <summary>
        /// Upload selected file to server filesystem
        /// <br>Note: this request may take a long time to process.</br>
        /// </summary>
        /// <param name="filename">name of uploaded file</param>
        /// <param name="fileBytes">data of the file uploaded to the server as a byte array</param>
        /// <exception cref="ArgumentNullException">The parameter <see cref="filename"/> or <see cref="fileBytes"/> is <see langword="null"/></exception>
        /// <exception cref="ArgumentException">The parameter <see cref="filename"/> or <see cref="fileBytes"/> is empty</exception>
        void SendFile([NotNull] string filename, [NotNull] byte[] fileBytes);

        /// <summary>
        /// Asks the server for a file to download
        /// <br>Note: this request may take a long time to process.</br>
        /// </summary>
        /// <param name="filename">name of downloaded file</param>
        /// <returns>file data as <see cref="byte"/> array. If file is not found <see langword="null"/></returns>
        /// <exception cref="ArgumentNullException">The parameter <see cref="filename"/> is <see langword="null"/></exception>
        /// <exception cref="ArgumentException">The parameter <see cref="filename"/> is empty</exception>
        [CanBeNull]
        byte[] RequestFile([NotNull] string filename);
        /// <summary>
        /// Asks the server for the entire list of files.
        /// <br>Note: this request may take a long time to process.</br>
        /// </summary>
        /// <returns>list of all files available on the server</returns>
        [NotNull]
        string RequestFileList();
        /// <summary>
        /// Send a request to the server to update the list of available files
        /// </summary>
        /// <param name="callback">event handler</param>
        /// <exception cref="ArgumentNullException">The parameter <see cref="callback"/> is <see langword="null"/></exception>
        void SubscribeToFilesUpdate([NotNull] Action callback);
    }
}