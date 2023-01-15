namespace Gtm.Business.Infrastructure.Repositories.User
{
    /// <summary>
    /// Encapsulation of logic to access data sources.
    /// </summary>
    internal interface IUserRepository
    {
        #region Methods 

        /// <summary>
        /// Saves the users in the file.
        /// </summary>
        /// <param name="users">User list</param>
        /// <returns>Represents an asynchronous operation. </returns>
        Task SaveAsync(IEnumerable<UserEntity> users);

        /// <summary>
        /// Read users from the file.
        /// </summary>
        /// <returns>A task that represents the asynchronous read operation, which wraps the list containing all users in the file.</returns>
        Task<IEnumerable<UserEntity>> ReadAsync();

        #endregion
    }
}
