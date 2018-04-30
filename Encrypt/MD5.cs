using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using MD5Type = Tools.Enum.Encrypt.MD5Type;

namespace Tools.Encrypt
{
    public static class MD5
    {
        /// <summary>   
        /// 取得 MD5 編碼後的 Hex 字串 
        /// <param name="encrypt">原始字串</param>
        public static string Encrypt(string source, MD5Type type = MD5Type.Use32Char)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] bytes = md5.ComputeHash(Encoding.UTF8.GetBytes(source));

            string encrypt = string.Empty;

            switch (type)
            {
                case MD5Type.Use16Char:
                    string temp = BitConverter.ToString(bytes, 4, 8);
                    encrypt = temp.Replace("-", "");
                    break;
                case MD5Type.Use32Char:
                    for (int i = 0; i < bytes.Length; i++)
                    {
                        encrypt = encrypt + bytes[i].ToString("x");
                    }
                    break;
                case MD5Type.Use64Char:
                    encrypt = Convert.ToBase64String(bytes);
                    break;
            }
            return encrypt;
        }
    }
}
