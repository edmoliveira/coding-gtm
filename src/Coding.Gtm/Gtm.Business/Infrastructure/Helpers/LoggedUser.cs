namespace Gtm.Business.Infrastructure.Helpers
{
    /// <summary>
    /// Logged in user.
    /// </summary>
    public sealed class LoggedUser
    {
        #region Properties

        /// <summary>
        /// User identifier
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// User login
        /// </summary>
        public string Login { get; set; }

        #endregion
    }
}
