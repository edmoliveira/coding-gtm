namespace Gtm.Business.Infrastructure.Repositories.User
{
    /// <summary>
    /// Mapping to user file.
    /// </summary>
    internal sealed class UserEntity
    {
        #region Properties

        /// <summary>
        /// User id
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// User login
        /// </summary>
        public string Login { get; set; }
        /// <summary>
        /// User name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// User password
        /// </summary>
        public string Password { get; set; }

        #endregion
    }
}
