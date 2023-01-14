using Gtm.Business.Infrastructure.Helpers.Extensions;
using Gtm.Business.Infrastructure.Helpers.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;

namespace Gtm.Business.Infrastructure.Repositories.Product
{
    /// <summary>
    /// Encapsulation of logic to access data sources.
    /// </summary>
    internal sealed class ProductRepository : BaseRepository, IProductRepository
    {
        #region Fields 

        /// <summary>
        /// Application Configuration
        /// </summary>
        private readonly IAppConfig _appConfig;
        /// <summary>
        /// Log
        /// </summary>
        private readonly ILogger<ProductRepository> _logger;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the Gtm.Business.Infrastructure.Repositories.Product.ProductRepository class.
        /// </summary>
        /// <param name="webHostEnvironment">Provides information about the web hosting environment an application is running in.</param>
        public ProductRepository(
                IWebHostEnvironment webHostEnvironment,
                IAppConfig appConfig,
                ILogger<ProductRepository> logger) :
            base(webHostEnvironment)
        {
            _appConfig = appConfig;
            _logger = logger;
        }

        #endregion

        #region Methods 

        #region public

        /// <summary>
        /// Saves the products in the file.
        /// </summary>
        /// <param name="products">Product list</param>
        /// <returns>Represents an asynchronous operation. </returns>
        public async Task SaveAsync(IEnumerable<ProductEntity> products)
        {
            string methodName = nameof(SaveAsync);

            _logger.LogBeginInformation(methodName);

            await WriteTextAsync(CombinePath(_appConfig.Database.Product), products).ConfigureAwait(false);

            _logger.LogEndInformation(methodName);
        }

        /// <summary>
        /// Read products from the file.
        /// </summary>
        /// <returns>A task that represents the asynchronous read operation, which wraps the list containing all products in the file.</returns>
        public async Task<IEnumerable<ProductEntity>> ReadAsync()
        {
            string methodName = nameof(ReadAsync);

            _logger.LogBeginInformation(methodName);

            var users = await ReadTextAsync<IEnumerable<ProductEntity>>(
                    CombinePath(_appConfig.Database.Product)
                ).ConfigureAwait(false);

            _logger.LogEndInformation(methodName);

            return users;
        }

        #endregion

        #endregion
    }
}
