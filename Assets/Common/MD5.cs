using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Common
{
    public class MD5
    {
        [ThreadStatic]
        private static System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();

        public static string ComputeHashString(byte[] data) { return ToString(ComputeHash(data)); }

        public static string ComputeHashString(string data) { return ToString(ComputeHash(System.Text.Encoding.UTF8.GetBytes(data))); }

        public static byte[] ComputeHash(byte[] data)
        {
            if (md5 == null)
                md5 = System.Security.Cryptography.MD5.Create();
            return md5.ComputeHash(data);
        }

        public static string ComputeHashStringFromFile(string filename) { return ToString(ComputeHashFromFile(filename)); }

        public static byte[] ComputeHashFromFile(string filename)
        {
            return ComputeHash(File.ReadAllBytes(filename));
        }

        public static string ToString(byte[] data)
        {
            return BitConverter.ToString(data).Replace("-", string.Empty).ToLowerInvariant();
        }
    }
}
