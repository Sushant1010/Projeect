using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace NCIT_MIS.Repository
{
    public class PasswordHash
    {
        //Creating Random Salt Value
        public String CreateSalt(int size)
        {
            var rng = new System.Security.Cryptography.RNGCryptoServiceProvider();
            var buff = new byte[size];
            rng.GetBytes(buff);
            return Convert.ToBase64String(buff);
        }

        //Generating Hash Value, Combining userpassword and above random salt value
        //public String GenerateSHA256Hash(String input, String salt)
        //{
        //    byte[] bytes = System.Text.Encoding.UTF8.GetBytes(input + salt);
        //    System.Security.Cryptography.SHA256Managed sha256hashstring = new System.Security.Cryptography.SHA256Managed();
        //    byte[] hash = sha256hashstring.ComputeHash(bytes);
        //    return ByteArrayToHexString(hash);
        //}

        public String GenerateSHA256Hash(String input, String salt)
        {
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(input + salt);
            System.Security.Cryptography.SHA512Managed sha512hashstring = new System.Security.Cryptography.SHA512Managed();
            byte[] hash = sha512hashstring.ComputeHash(bytes);
            return ByteArrayToHexString(hash);
        }

        //Converting Array Byte value to one single String value
        public static string ByteArrayToHexString(byte[] Bytes)
        {
            StringBuilder Result = new StringBuilder(Bytes.Length * 2);
            string HexAlphabet = "0123456789ABCDEF";

            foreach (byte B in Bytes)
            {
                Result.Append(HexAlphabet[(int)(B >> 4)]);
                Result.Append(HexAlphabet[(int)(B & 0xF)]);
            }

            return Result.ToString();
        }
    }
}