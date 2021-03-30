using System;
using System.ComponentModel;
using System.Linq;
using JetBrains.Annotations;

namespace Assets.Scripts.Network.Shared
{
    public class PackagePacker
    {
        public static byte[] PackPackage([NotNull] byte[] data, PackageType type)
        {
            if (data == null) throw new ArgumentNullException(nameof(data));
            if (data.Length == 0) throw new ArgumentException("Value cannot be an empty collection.", nameof(data));
            if (!Enum.IsDefined(typeof(PackageType), type))
                throw new InvalidEnumArgumentException(nameof(type), (int) type, typeof(PackageType));
            
            var package = new byte[data.Length + 1];
            package[0] = (byte) type;
            data.CopyTo(package, 1);
            
            return package;
        }

        public static byte[] UnpackPackage(byte[] package, out PackageType type)
        {
            type = (PackageType) package[0];
            return package.Skip(1).ToArray();
        }
    }
}