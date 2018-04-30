using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using MD5Type = Tools.Enum.Encrypt.MD5Type;

namespace Tools.Encrypt
{
    public static class AES
    {
        //加密需要的資訊和class
        private static readonly byte[] salt = Encoding.UTF8.GetBytes("bnmjhgtu");
        private static readonly byte[] _key = Encoding.UTF8.GetBytes("juikmnhy");
        private static readonly byte[] _iv = Encoding.UTF8.GetBytes("sdewqazx");
        private static readonly AesCryptoServiceProvider aes =
            new AesCryptoServiceProvider()
            {
                Key = new SHA256CryptoServiceProvider().ComputeHash(new Rfc2898DeriveBytes(_key, salt, 8).GetBytes(8)),
                IV = new MD5CryptoServiceProvider().ComputeHash(new Rfc2898DeriveBytes(_iv, salt, 8).GetBytes(8))
            };

        /// <summary>
        /// 字串加密
        /// </summary>
        /// <param name="source"></param>
        /// <remarks>64Base</remarks>
        /// <returns></returns>
        public static string Encrypt(string source)
        {
            string encrypt = string.Empty;
            byte[] dataByteArray = Encoding.UTF8.GetBytes(source);
            using (MemoryStream ms = new MemoryStream())
            using (CryptoStream cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
            {
                cs.Write(dataByteArray, 0, dataByteArray.Length);
                cs.FlushFinalBlock();
                encrypt = Convert.ToBase64String(ms.ToArray());
            }
            return encrypt;
        }

        /// <summary>
        /// 字串解密
        /// </summary>
        /// <param name="encrypt"></param>
        /// <remarks>64Base</remarks>
        /// <returns></returns>
        public static string Decrypt(string encrypt)
        {
            string decrypt = string.Empty;

            byte[] dataByteArray = Convert.FromBase64String(encrypt);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(dataByteArray, 0, dataByteArray.Length);
                    cs.FlushFinalBlock();
                    decrypt = Encoding.UTF8.GetString(ms.ToArray());
                }
            }
            return decrypt;
        }
    }
}
