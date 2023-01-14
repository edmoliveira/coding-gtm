using Gtm.Business.Infrastructure.Helpers.Extensions;
using Gtm.Business.Infrastructure.Helpers.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;

namespace Gtm.Business.Infrastructure.Repositories.ProductPrice
{
    /// <summary>
    /// Encapsulation of logic to access data sources.
    /// </summary>
    internal sealed class ProductPriceRepository : BaseRepository, IProductPriceRepository
    {
        #region Fields 

        /// <summary>
        /// Application Configuration
        /// </summary>
        private readonly IAppConfig _appConfig;
        /// <summary>
        /// Log
        /// </summary>
        private readonly ILogger<ProductPriceRepository> _logger;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the Gtm.Business.Infrastructure.Repositories.ProductPrice.ProductPriceRepository class.
        /// </summary>
        /// <param name="webHostEnvironment">Provides information about the web hosting environment an application is running in.</param>
        public ProductPriceRepository(
                IWebHostEnvironment webHostEnvironment,
                IAppConfig appConfig,
                ILogger<ProductPriceRepository> logger) :
            base(webHostEnvironment)
        {
            _appConfig = appConfig;
            _logger = logger;
        }

        #endregion

        #region Methods 

        #region public

        /// <summary>
        /// Saves products prices in the file.
        /// </summary>
        /// <param name="prices">product price list</param>
        /// <returns>Represents an asynchronous operation. </returns>
        public async Task SaveAsync(IEnumerable<ProductPriceEntity> prices)
        {
            string methodName = nameof(SaveAsync);

            _logger.LogBeginInformation(methodName);

            await WriteTextAsync(CombinePath(_appConfig.Database.ProductPrice), prices).ConfigureAwait(false);

            _logger.LogEndInformation(methodName);
        }

        /// <summary>
        /// Read products prices from the file.
        /// </summary>
        /// <returns>A task that represents the asynchronous read operation, which wraps the list containing all products prices in the file.</returns>
        public async Task<IEnumerable<ProductPriceEntity>> ReadAsync()
        {
            string methodName = nameof(ReadAsync);

            _logger.LogBeginInformation(methodName);

            var users = await ReadTextAsync<IEnumerable<ProductPriceEntity>>(
                    CombinePath(_appConfig.Database.ProductPrice)
                ).ConfigureAwait(false);

            _logger.LogEndInformation(methodName);

            return users;
        }

        #endregion

        #endregion
    }
}
