namespace Vacations.Business.Services.Contracts
{
    public interface IEncryptService
    {
        /// <summary>
        /// Encrypt string by key with TripleDES 
        /// </summary>
        /// <param name="inputString">input string</param>
        /// <param name="key">encryption key</param>
        /// <returns>TripleDES-encrypted string in Base64 encoding</returns>
        string Encrypt(string inputString, string key);

        /// <summary>
        /// Encrypt string by key with TripleDES 
        /// </summary>
        /// <param name="inputString">input string</param>
        /// <param name="key">encryption key</param>
        /// <returns>TripleDES-encrypted string in Base64 encoding</returns>
        string Decrypt(string inputString, string key);

        /// <summary>
        /// get SHA1 hash of text
        /// </summary>
        /// <param name="text">text</param>
        /// <returns>text SHA1 hash</returns>
        byte[] Sha1Hash(string text);

    }
}
