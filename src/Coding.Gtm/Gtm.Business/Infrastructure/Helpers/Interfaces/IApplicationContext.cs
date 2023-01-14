namespace Gtm.Business.Infrastructure.Helpers.Interfaces
{
    /// <summary>
    /// Class to be used on one instance throughout the application per request
    /// </summary>
    internal interface IApplicationContext
    {
        #region Properties

        /// <summary>
        /// Request id for all transaction in the platform.
        /// </summary>
        string RequestId { get; }
        /// <summary>
        /// Logged in user.
        /// </summary>
        LoggedUser LoggedUser { get; }

        #endregion
    }

    /// <summary>
    /// Class to be used on one instance throughout the application per request
    /// </summary>
    public interface ISetApplicationContext
    {
        #region Methods

        /// <summary>
        /// Sets the current logical context item to the specified value.
        /// </summary>
        /// <param name="value">Value of the Request id.</param>
        void SetRequestId(string value);
        /// <summary>
        /// Sets the logged in user.
        /// </summary>
        /// <param name="loggedUser">Logged in user.</param>
        void SetLoggedUser(LoggedUser loggedUser);

        #endregion
    }
}
