using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Tools.Enum;

namespace Tools
{
    /// <summary>
    /// 故意分開的，用Switch會變慢
    /// </summary>
    public class Random
    {
        /// <summary>
        /// 數字
        /// </summary>
        /// <param name="digits"></param>
        /// <returns></returns>
        public static string Number(int digits)
        {
            char[] source = "0123456789".ToCharArray();
            return Seed(digits, source);
        }

        /// <summary>
        /// 文字(大小寫)
        /// </summary>
        /// <param name="digits"></param>
        /// <returns></returns>
        public static string Word(int digits)
        {
            char[] source = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
            return Seed(digits, source);
        }

        /// <summary>
        /// 文字(大寫)
        /// </summary>
        /// <param name="digits"></param>
        /// <returns></returns>
        public static string WordUpper(int digits)
        {
            char[] source = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
            return Seed(digits, source);
        }

        /// <summary>
        /// 文字(小寫)
        /// </summary>
        /// <param name="digits"></param>
        /// <returns></returns>
        public static string WordLower(int digits)
        {
            char[] source = "abcdefghijklmnopqrstuvwxyz".ToCharArray();
            return Seed(digits, source);
        }

        /// <summary>
        /// 文字(大小寫)+數字
        /// </summary>
        /// <param name="digits"></param>
        /// <returns></returns>
        public static string Mix(int digits)
        {
            char[] source = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789".ToCharArray();
            return Seed(digits, source);
        }

        /// <summary>
        /// 文字加數字
        /// </summary>
        /// <param name="digits"></param>
        /// <returns></returns>
        private static string Seed(int digits, char[] chars)
        {            
            byte[] data = new byte[digits];
            using (RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider())
            {
                crypto.GetNonZeroBytes(data);
            }
            StringBuilder result = new StringBuilder(digits);
            foreach (byte b in data)
            {
                result.Append(chars[b % (chars.Length)]);
            }
            return result.ToString();
        }
    }
}
