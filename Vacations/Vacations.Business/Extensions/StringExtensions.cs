using System;
using System.Linq;

namespace Vacations.Business.Extensions
{
    /// <summary>
    /// Stinring extension class
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Converts a hex string to byte array
        /// </summary>
        /// <param name="hex">The base string</param>
        /// <returns>The byte array</returns>
        public static byte[] ToByteArray(this string hex)
        {
            if (hex.StartsWith("0x"))
            {
                hex = hex.Substring(2);
            }
            return Enumerable.Range(0, hex.Length)
                .Where(x => x % 2 == 0)
                .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                .ToArray();
        }
    }
}
