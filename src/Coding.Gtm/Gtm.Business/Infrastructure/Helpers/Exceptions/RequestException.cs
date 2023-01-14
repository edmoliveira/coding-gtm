﻿using FluentValidation.Results;
using Newtonsoft.Json;
using System.Net;

namespace Gtm.Business.Infrastructure.Helpers.Exceptions
{
    /// <summary>
    /// Represents errors that occur during application execution (Request Data).
    /// </summary>
    public class RequestException : Exception
    {
        #region Properties

        /// <summary>
        /// Contains the values of status codes defined for HTTP
        /// </summary>
        public HttpStatusCode StatusCode { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the Gtm.Business.Infrastructure.Helpers.Exceptions.RequestException class with a specific message that describes the current exception.
        /// </summary>
        /// <param name="statusCode">Contains the values of status codes defined for HTTP</param>
        /// <param name="message">The message that describes the error.</param>
        public RequestException(HttpStatusCode statusCode, string message) : base(message)
        {
            StatusCode = statusCode;
        }

        /// <summary>
        /// Initializes a new instance of the Gtm.Business.Infrastructure.Exceptions.RequestException class with a specific messages that describes the current exception.
        /// </summary>
        /// <param name="statusCode">Contains the values of status codes defined for HTTP</param>
        /// <param name="messageList">Message collection.</param>
        public RequestException(HttpStatusCode statusCode, IList<string> messageList) : base(TransformMessageListToMessage(messageList))
        {
            StatusCode = statusCode;
        }

        /// <summary>
        /// Initializes a new instance of the Gtm.Business.Infrastructure.Exceptions.RequestException class with a specific messages that describes the current exception.
        /// </summary>
        /// <param name="statusCode">Contains the values of status codes defined for HTTP</param>
        /// <param name="errors">List with validation failure.</param>
        public RequestException(HttpStatusCode statusCode, IList<ValidationFailure> errors) : base(TransformValidationFailureTDictionary(errors))
        {
            StatusCode = statusCode;
        }

        /// <summary>
        /// Initializes a new instance of the Gtm.Business.Infrastructure.Exceptions.RequestException class with a specific messages that describes the current exception and an inner exception.
        /// </summary>
        /// <param name="statusCode">Contains the values of status codes defined for HTTP</param>
        /// <param name="messageList">Message collection.</param>
        /// <param name="inner">The inner exception.</param>
        public RequestException(HttpStatusCode statusCode, List<string> messageList, Exception inner) : base(TransformMessageListToMessage(messageList), inner)
        {
            StatusCode = statusCode;
        }

        #endregion

        #region Methods 

        #region private

        /// <summary>
        /// Transforms exception messages to into a single string
        /// </summary>
        /// <param name="messageList">A list of exception messages.</param>
        /// <returns>Exception messages concatenated into a single string</returns>
        private static string TransformMessageListToMessage(IList<string> messageList)
        {
            return messageList.Aggregate((current, value) => $"{current}\r\n{value}");
        }

        /// <summary>
        /// Transforms a list with validation failure to into a single string
        /// </summary>
        /// <param name="errors">List with validation failure.</param>
        /// <returns>Returns the list with validation failure concatenated into a single string</returns>
        private static string TransformValidationFailureTDictionary(IList<ValidationFailure> errors)
        {
            var dictionary = new Dictionary<string, IList<string>>();

            foreach (var propertyName in errors.GroupBy(c => c.PropertyName))
            {
                dictionary.Add(propertyName.Key,
                    errors
                        .Where(c => c.PropertyName == propertyName.Key)
                        .Select(c => c.ErrorMessage)
                        .ToList()
                );
            }

            return JsonConvert.SerializeObject(new { errors = dictionary });
        }

        #endregion

        #endregion
    }
}
