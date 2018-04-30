using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Tools.Encrypt
{
    /// <summary>
    /// DES加解密
    /// </summary>
    public static class DES
    {
        //加密需要的資訊和class
        private static readonly byte[] salt = Encoding.ASCII.GetBytes("bnmjhgtu");
        private static readonly byte[] _key = Encoding.ASCII.GetBytes("juikmnhy");
        private static readonly byte[] _iv = Encoding.ASCII.GetBytes("sdewqazx");
        private static readonly DESCryptoServiceProvider des = 
            new DESCryptoServiceProvider() 
                { 
                    Key = new Rfc2898DeriveBytes(_key, salt, 8).GetBytes(8),
                    IV = new Rfc2898DeriveBytes(_iv, salt, 8).GetBytes(8)
                };

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="source"></param>
        /// <remarks>64Base</remarks>
        /// <returns></returns>
        public static string Encrypt(string source)
        {            
            byte[] dataByteArray = Encoding.UTF8.GetBytes(source);

            string encrypt = "";
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(dataByteArray, 0, dataByteArray.Length);
                    cs.FlushFinalBlock();
                    encrypt = Convert.ToBase64String(ms.ToArray());
                }
                return encrypt;
            }
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="encrypt"></param>
        /// <remarks>64Base</remarks>
        /// <returns></returns>
        public static string Decrypt(string encrypt)
        {
            byte[] dataByteArray = Convert.FromBase64String(encrypt);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(dataByteArray, 0, dataByteArray.Length);
                    cs.FlushFinalBlock();
                    return Encoding.UTF8.GetString(ms.ToArray());
                }
            }
        }
    }
}
