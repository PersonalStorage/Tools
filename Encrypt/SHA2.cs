using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using SHA2Type = Tools.Enum.Encrypt.SHA2Type;

namespace Tools.Encrypt
{
    /// <summary>
    /// 雜湊類不用解密-
    /// </summary>
    public class SHA2
    {
        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="source"></param>
        /// <param name="type"></param>
        /// <remarks>64Base</remarks>
        /// <returns></returns>
        public static string Encrypt(string source, SHA2Type type = SHA2Type.Use256)
        {
            string encrypt = string.Empty;
            byte[] bytes = Encoding.Default.GetBytes(source);
            byte[] temp = null;

            switch (type)
            {
                case SHA2Type.Use256:
                    temp = new SHA256CryptoServiceProvider().ComputeHash(bytes);
                    break;
                case SHA2Type.Use384:
                    temp = new SHA384CryptoServiceProvider().ComputeHash(bytes);
                    break;
                case SHA2Type.Use512:
                    temp = new SHA512CryptoServiceProvider().ComputeHash(bytes);
                    break;
            }
            encrypt = Convert.ToBase64String(temp);

            return encrypt;
        }
    }
}
