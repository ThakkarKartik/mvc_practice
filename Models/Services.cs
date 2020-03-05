using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using WebPractice.Models;
namespace WebPractice.Models
{
    public static class Services
    {
        static Random R = new Random(0001);
        public static string GenerateOTP()
        {
            string OTP;
            OTP = R.Next(1111, 9999).ToString();
            return OTP;
        }
        public static string UniqueCode()
        {
            string Code;
            Code = DateTime.Now.ToShortDateString().Replace("/", "");
            return Code;
        }
        public static string Decryptdata(string encryptpwd)
        {
            string decryptpwd = string.Empty;
            UTF8Encoding encodepwd = new UTF8Encoding();
            Decoder Decode = encodepwd.GetDecoder();
            byte[] todecode_byte = Convert.FromBase64String(encryptpwd);
            int charCount = Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
            char[] decoded_char = new char[charCount];
            Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
            decryptpwd = new String(decoded_char);
            return decryptpwd;
        }
        public static string Encryptdata(string password)
        {
            string strmsg = string.Empty;
            byte[] encode = new byte[password.Length];
            encode = Encoding.UTF8.GetBytes(password);
            strmsg = Convert.ToBase64String(encode);
            return strmsg;
        }
    }
}