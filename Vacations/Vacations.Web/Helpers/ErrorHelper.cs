using System;
using System.Collections.Generic;
using Vacations.DataAccess.Models;

namespace Vacations.Web.Helpers
{
    public sealed class ErrorHelper
    {
        #region Fields

        /// <summary>
        /// Error helper field
        /// </summary>
        private static readonly Lazy<ErrorHelper> _errorHelper = new Lazy<ErrorHelper>(() => new ErrorHelper());


        /// <summary>
        /// Gets ErrorHelper static instance
        /// </summary>
        public static ErrorHelper GetInstance
        {
            get { return _errorHelper.Value; }
        }


        /// <summary>
        /// Private field to hold the dictionary of ErrorModel
        /// </summary>
        private readonly Dictionary<string, ErrorModel> _errorDictionary = new Dictionary<string, ErrorModel>
        {
            {
                "loginorpasswordincorrect", new ErrorModel
                {
                    Title = "Sorry, the username or password is incorrect",
                    SubTitle = "Try entering your information again.",
                    Text = "Please contact technical support at <a href=\"mailto:support@sometestcomp.com\">support@sometestcomp.com</a> for further enquiry."
                }
            },
            {
                "EmailAlreadyExist", new ErrorModel()
                {
                    Title = "Sorry, an user with the same email already exists.",
                    SubTitle = "",
                    Text = "An user with the same email already exists."
                }
            }

        };

        #endregion Fields


        /// <summary>
        /// Constructor
        /// </summary>
        private ErrorHelper()
        {

        }



        /// <summary>
        /// Get the ErrorModel from the dictionary
        /// </summary>
        /// <param name="key">Key of the ErrorModel within the dictionary</param>
        /// <returns>Return an ErrorModel</returns>
        public ErrorModel GetError(string key)
        {
            ErrorModel value;
            if (_errorDictionary.TryGetValue(key, out value))
            {
                return value;
            }
            return new ErrorModel
            {
                Title = "Sorry, something wrong happened.",
                SubTitle = "",
                Text = "Sorry, something wrong happened. Please contact technical support at <a href=\"mailto:support@sometestcomp.com\">support@sometestcomp.com</a> for further enquiry."
            };
        }

    }
}