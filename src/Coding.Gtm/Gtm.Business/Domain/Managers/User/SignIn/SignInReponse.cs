namespace Gtm.Business.Domain.Managers.User.SignIn
{
    /// <summary>
    /// Response data.
    /// </summary>
    public sealed class SignInResponse
    {
        #region Properties

        /// <summary>
        /// Token
        /// </summary>
        public string Token { get; set; }
        /// <summary>
        /// Authentication Token expiration in seconds.
        /// </summary>
        public double ExpireSeconds { get; set; }
        /// <summary>
        /// User name
        /// </summary>
        public string Name { get; set; }

        #endregion
    }
}
