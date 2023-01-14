using Gtm.Business.Infrastructure.Helpers.Interfaces;
using NLog;

namespace Gtm.Business.Infrastructure.Helpers
{
    /// <summary>
    /// Class to be used on one instance throughout the application per request
    /// </summary>
    internal sealed class ApplicationContext : IApplicationContext, ISetApplicationContext
    {
        #region Fields 

        /// <summary>
        /// The current logical context named item, as System.String.
        /// </summary>
        private readonly string _requestIdName = "requestId";

        #endregion

        #region Properties

        /// <summary>
        /// Request id for all transaction in the platform.
        /// </summary>
        public string RequestId
        {
            get
            {

                return ScopeContext.TryGetProperty(_requestIdName, out var value) ? value?.ToString() : string.Empty;
            }
        }

        /// <summary>
        /// Logged in user.
        /// </summary>
        public LoggedUser LoggedUser { get; private set; }

        #endregion

        #region Methods 

        #region public

        /// <summary>
        /// Sets the current logical context item to the specified value.
        /// </summary>
        /// <param name="value">Value of the Request id.</param>
        public void SetRequestId(string value)
        {
            ScopeContext.PushProperty(_requestIdName, value);
        }

        /// <summary>
        /// Sets the logged in user.
        /// </summary>
        /// <param name="loggedUser">Logged in user.</param>
        public void SetLoggedUser(LoggedUser loggedUser)
        {
            LoggedUser = loggedUser;
        }

        #endregion

        #endregion
    }
}
