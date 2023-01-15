using Gtm.Business.Infrastructure.Helpers.Extensions;
using Gtm.Business.Infrastructure.Helpers.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;

namespace Gtm.Business.Infrastructure.Repositories.Country
{
    /// <summary>
    /// Encapsulation of logic to access data sources.
    /// </summary>
    internal sealed class CountryRepository : BaseRepository, ICountryRepository
    {
        #region Fields 

        /// <summary>
        /// Application Configuration
        /// </summary>
        private readonly IAppConfig _appConfig;
        /// <summary>
        /// Log
        /// </summary>
        private readonly ILogger<CountryRepository> _logger;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the Gtm.Business.Infrastructure.Repositories.Country.CountryRepository class.
        /// </summary>
        /// <param name="webHostEnvironment">Provides information about the web hosting environment an application is running in.</param>
        public CountryRepository(
                IWebHostEnvironment webHostEnvironment,
                IAppConfig appConfig,
                ILogger<CountryRepository> logger) :
            base(webHostEnvironment)
        {
            _appConfig = appConfig;
            _logger = logger;
        }

        #endregion

        #region Methods 

        #region public

        /// <summary>
        /// Saves the countries in the file.
        /// </summary>
        /// <param name="countries">Country list</param>
        /// <returns>Represents an asynchronous operation. </returns>
        public async Task SaveAsync(IEnumerable<CountryEntity> countries)
        {
            string methodName = nameof(SaveAsync);

            _logger.LogBeginInformation(methodName);

            await WriteTextAsync(CombinePath(_appConfig.Database.Country), countries).ConfigureAwait(false);

            _logger.LogEndInformation(methodName);
        }

        /// <summary>
        /// Read countries from the file.
        /// </summary>
        /// <returns>A task that represents the asynchronous read operation, which wraps the list containing all countries in the file.</returns>
        public async Task<IEnumerable<CountryEntity>> ReadAsync()
        {
            string methodName = nameof(ReadAsync);

            _logger.LogBeginInformation(methodName);

            var users = await ReadTextAsync<IEnumerable<CountryEntity>>(
                    CombinePath(_appConfig.Database.Country)
                ).ConfigureAwait(false);

            _logger.LogEndInformation(methodName);

            return users;
        }

        #endregion

        #endregion
    }
}
