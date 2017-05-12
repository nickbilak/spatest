using System;
using System.Configuration;
using System.Diagnostics.CodeAnalysis;
using NUnit.Framework;
using Vacations.Business.Services.Contracts;
using Vacations.Business.Services.Implementations;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using CollectionAssert = Microsoft.VisualStudio.TestTools.UnitTesting.CollectionAssert;

namespace Vacations.Business.Tests.Services.Implementations
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    public class EncryptServiceTest
    {
        private readonly IEncryptService _encryptService = new EncryptService();


        /// <summary>
        /// SHA1 hash of text test
        /// </summary>
        [Test]
        public void Sha1HashTest()
        {
            // Arrange
            const string text = "test";
            byte[] expected = { 135, 248, 237, 145, 87, 18, 95, 252, 77, 169, 224, 106, 123, 128, 17, 173, 128, 165, 63, 225 };

            // Act
            var result = _encryptService.Sha1Hash(text);

            // Assert
            CollectionAssert.AreEqual(expected, result);
        }

        /// <summary>
        /// Sha1Hash Wrong Parameter test
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Sha1Hash_WrongParameterTest()
        {
            // Arrange
            const string text = "";
            // Act
            _encryptService.Sha1Hash(text);
        }

        /// <summary>
        /// Encrypt Wrong Parameter test
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Encrypt_WrongParameterTest()
        {
            // Arrange
            const string clearText = "";
            const string encryptKey = "asdsd";
            // Act
            _encryptService.Encrypt(clearText, encryptKey);
        }

        /// <summary>
        /// Decrypt Wrong Parameter test
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Decrypt_WrongParameterTest()
        {
            // Arrange
            const string text = "";
            const string encryptKey = "asdsd";
            // Act
            _encryptService.Decrypt(text, encryptKey);
        }

        /// <summary>
        /// Encrypt text test
        /// </summary>
        [Test]
        public void EncryptTest()
        {
            // Arrange
            const string clearText = "clear";
            const string expected = "7k8CcAogpuI1";

            // Act
            var result = _encryptService.Encrypt(clearText, ConfigurationManager.AppSettings["encryptionKey"]);

            // Assert
            Assert.AreEqual(expected, result);
        }

        /// <summary>
        /// Decrypt text test
        /// </summary>
        [Test]
        public void DecryptTest()
        {
            // Arrange
            const string encryptedText = "7k8CcAogpuI1";
            const string expected = "clear";

            // Act
            var result = _encryptService.Decrypt(encryptedText, ConfigurationManager.AppSettings["encryptionKey"]);

            // Assert
            Assert.AreEqual(expected, result);
        }

    }
}
