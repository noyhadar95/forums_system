using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ForumsSystem.Server.PRG
{
    public class Hash
    {
         private static SHA256 hash = new SHA256CryptoServiceProvider();

        public static String GetHash(String value)
        {
            using (SHA256 hash = SHA256Managed.Create())
            {
                return String.Join("", hash
                  .ComputeHash(Encoding.UTF8.GetBytes(value))
                  .Select(item => item.ToString("x2")));
            }
        }

        /*public static string GetHash(string input)
        {
            return Encoding.ASCII.GetString(hash.ComputeHash(Encoding.ASCII.GetBytes(input))) ;
            /*Byte[] inputBytes = GetBytes(input);
            Byte[] outputBytes = hash.ComputeHash(inputBytes);
            return GetString(outputBytes);
            
        }

        private static byte[] GetBytes(string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        private static string GetString(byte[] bytes)
        {
            char[] chars = new char[bytes.Length / sizeof(char)];
            System.Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
            return new string(chars);
        }*/
    }
}
