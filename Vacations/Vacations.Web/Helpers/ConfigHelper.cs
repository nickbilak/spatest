using System;
using System.Configuration;
using System.Diagnostics.CodeAnalysis;

namespace Vacations.Web.Helpers
{
    [ExcludeFromCodeCoverage]
    public sealed class ConfigHelper
    {
        #region Fields

        /// <summary>
        /// Configuration helper field
        /// </summary>
        private static readonly Lazy<ConfigHelper> _configHelper = new Lazy<ConfigHelper>(() => new ConfigHelper());

        #endregion Fields


        #region Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        private ConfigHelper()
        {

        }


        #endregion Constructors


        #region Properties

        /// <summary>
        /// Gets ConfigHelper static instance
        /// </summary>
        public static ConfigHelper GetInstance
        {
            get { return _configHelper.Value; }
        }

        /// <summary>
        /// Retrieves default settings for encryptionKey
        /// </summary>
        public string EncryptionKey
        {
            get { return ConfigurationManager.AppSettings["encryptionKey"]; }
        }
        

        #endregion Properties


        #region Methods


        #endregion Methods
    }

}