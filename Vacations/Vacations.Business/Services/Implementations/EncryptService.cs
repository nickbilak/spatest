using System;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using Vacations.Business.Services.Contracts;

namespace Vacations.Business.Services.Implementations
{
    public class EncryptService : IEncryptService
    {

        /// <summary>
        /// get SHA1 hash of text
        /// </summary>
        /// <param name="text">text</param>
        /// <returns>text SHA1 hash</returns>
        public byte[] Sha1Hash(string text)
        {
            if (string.IsNullOrWhiteSpace(text)) throw new ArgumentNullException("text");

            SHA1 hash = new SHA1CryptoServiceProvider();
            return hash.ComputeHash(Encoding.Unicode.GetBytes(text));
        }

        /// <summary>
        /// Encrypt string by key with TripleDES 
        /// </summary>
        /// <param name="inputString">input string</param>
        /// <param name="key">encryption key</param>
        /// <returns>TripleDES-encrypted string in Base64 encoding</returns>
        public string Encrypt(string inputString, string key)
        {
            if (string.IsNullOrWhiteSpace(inputString)) throw new ArgumentNullException("inputString");
            if (string.IsNullOrWhiteSpace(key)) throw new ArgumentNullException("key");

            byte[] buffer = Encoding.ASCII.GetBytes(inputString);
            TripleDESCryptoServiceProvider tripleDes = new TripleDESCryptoServiceProvider
            {
                Key = Encoding.ASCII.GetBytes(key),
                Mode = CipherMode.ECB
            };
            ICryptoTransform transform = tripleDes.CreateEncryptor();
            var _utf8 = new UTF8Encoding();
            var output = transform.TransformFinalBlock(buffer, 0, buffer.Length);
            var res = HttpServerUtility.UrlTokenEncode(output);
            return res;
        }

        /// <summary>
        /// Encrypt string by key with TripleDES 
        /// </summary>
        /// <param name="inputString">input string</param>
        /// <param name="key">encryption key</param>
        /// <returns>TripleDES-encrypted string in Base64 encoding</returns>
        public string Decrypt(string inputString, string key)
        {
            if (string.IsNullOrWhiteSpace(inputString)) throw new ArgumentNullException("inputString");
            if (string.IsNullOrWhiteSpace(key)) throw new ArgumentNullException("key");

            //byte[] buffer = Encoding.ASCII.GetBytes(inputString);
            byte[] buffer = HttpServerUtility.UrlTokenDecode(inputString);
            TripleDESCryptoServiceProvider tripleDes = new TripleDESCryptoServiceProvider
            {
                Key = Encoding.ASCII.GetBytes(key),
                Mode = CipherMode.ECB
            };
            ICryptoTransform transform = tripleDes.CreateDecryptor();
            var _utf8 = new UTF8Encoding();
            var output = transform.TransformFinalBlock(buffer, 0, buffer.Length);
            var res = _utf8.GetString(output);
            return res;
        }

    }
}
