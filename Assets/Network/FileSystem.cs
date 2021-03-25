using System;
using System.IO;
using JetBrains.Annotations;

namespace ARStudy.Server
{
    public class FileSystem
    {
        public readonly string Path;

        public FileSystem(string path)
        {
            Path = path;
        }

        public FileSystem()
        {
            Path = Directory.GetCurrentDirectory();
        }

        public void SaveFile([NotNull] string filename, [NotNull] byte[] bytes)
        {
            if (bytes == null) 
                throw new ArgumentNullException(nameof(bytes));
            if (bytes.Length == 0)
                throw new ArgumentException("Value cannot be an empty collection.", nameof(bytes));

            if (string.IsNullOrEmpty(filename))
                throw new ArgumentException("Value cannot be null or empty.", nameof(filename));

            File.WriteAllBytes(filename.Contains(".") ? $"{Path}\\{filename}" : $"{Path}\\{filename}.bm", bytes);
        }

        public byte[] GetFileContent([NotNull] string filename)
        {
            if (string.IsNullOrEmpty(filename))
                throw new ArgumentException("Value cannot be null or empty.", nameof(filename));

            return File.ReadAllBytes(filename.Contains(".") ? $"{Path}\\{filename}" : $"{Path}\\{filename}.bm");
        }

        public string[] GetFilesList()
        {
           return Directory.GetFiles(Directory.GetCurrentDirectory());
        }
    }
}