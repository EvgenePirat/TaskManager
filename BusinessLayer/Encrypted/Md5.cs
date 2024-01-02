using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Encrypted
{
    /// <summary>
    /// Class for releaze logic encrypted password
    /// </summary>
    public static class Md5
    {
        /// <summary>
        /// Method for hash password
        /// </summary>
        /// <param name="password">not hash password</param>
        /// <returns>returned string with hash password</returns>
        public static string? HashPassword(string? password)
        {
            if(password != null)
            {
                MD5 md5 = MD5.Create();

                byte[] encodingBytes = Encoding.UTF8.GetBytes(password);
                byte[] hash = md5.ComputeHash(encodingBytes);

                StringBuilder resultEncreptedString = new StringBuilder();
                foreach (byte b in hash)
                {
                    resultEncreptedString.Append(b.ToString("X2"));
                }

                return Convert.ToString(resultEncreptedString);
            }
            return null;
        }
    }
}
