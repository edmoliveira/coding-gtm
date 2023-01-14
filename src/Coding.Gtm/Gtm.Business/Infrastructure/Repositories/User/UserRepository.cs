using Gtm.Business.Infrastructure.Helpers.Extensions;
using Gtm.Business.Infrastructure.Helpers.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;

namespace Gtm.Business.Infrastructure.Repositories.User
{
    /// <summary>
    /// Encapsulation of logic to access data sources.
    /// </summary>
    internal sealed class UserRepository : BaseRepository, IUserRepository
    {
        #region Fields 

        /// <summary>
        /// Application Configuration
        /// </summary>
        private readonly IAppConfig _appConfig;
        /// <summary>
        /// Log
        /// </summary>
        private readonly ILogger<UserRepository> _logger;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the Gtm.Business.Infrastructure.Repositories.User.UserRepository class.
        /// </summary>
        /// <param name="webHostEnvironment">Provides information about the web hosting environment an application is running in.</param>
        public UserRepository(
                IWebHostEnvironment webHostEnvironment,
                IAppConfig appConfig,
                ILogger<UserRepository> logger) :
            base(webHostEnvironment)
        {
            _appConfig = appConfig;
            _logger = logger;
        }

        #endregion

        #region Methods 

        #region public

        /// <summary>
        /// Saves the users in the file.
        /// </summary>
        /// <param name="users">User list</param>
        /// <returns>Represents an asynchronous operation. </returns>
        public async Task SaveAsync(IEnumerable<UserEntity> users)
        {
            string methodName = nameof(SaveAsync);

            _logger.LogBeginInformation(methodName);

            await WriteTextAsync(CombinePath(_appConfig.Database.User), users).ConfigureAwait(false);

            _logger.LogEndInformation(methodName);
        }

        /// <summary>
        /// Read users from the file.
        /// </summary>
        /// <returns>A task that represents the asynchronous read operation, which wraps the list containing all users in the file.</returns>
        public async Task<IEnumerable<UserEntity>> ReadAsync()
        {
            string methodName = nameof(ReadAsync);

            _logger.LogBeginInformation(methodName);

            var users = await ReadTextAsync<IEnumerable<UserEntity>>(
                    CombinePath(_appConfig.Database.User)
                ).ConfigureAwait(false);

            _logger.LogEndInformation(methodName);

            return users;
        }

        #endregion

        #endregion
    }
}
