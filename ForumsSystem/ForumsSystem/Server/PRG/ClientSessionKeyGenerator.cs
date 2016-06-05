using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ForumsSystem.Server.PRG
{
    public class ClientSessionKeyGenerator
    {
        public static string GetUniqueKey()
        {
            int maxSeedLength = 8;
            char[] chars = new char[10];
            chars =
            "1234567890".ToCharArray();
            byte[] data = new byte[1];
            using (RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider())
            {
                crypto.GetNonZeroBytes(data);
                data = new byte[maxSeedLength];
                crypto.GetNonZeroBytes(data);
            }
            StringBuilder result = new StringBuilder(maxSeedLength);
            foreach (byte b in data)
            {
                result.Append(chars[b % (chars.Length)]);
            }
            return result.ToString();
        }
    }
}
