using Gtm.Business.Domain.Managers.ProductPrice.Models;
using Gtm.Business.Infrastructure.Helpers.Exceptions;
using Gtm.Business.Infrastructure.Helpers.Extensions;
using Gtm.Business.Infrastructure.Repositories.Country;
using Gtm.Business.Infrastructure.Repositories.Product;
using Gtm.Business.Infrastructure.Repositories.ProductPrice;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Gtm.Business.Domain.Managers.ProductPrice.Read
{
    /// <summary>
    /// ProductPrice Manager
    /// </summary>
    internal sealed class ReadManager : IReadManager
    {
        #region Fields

        /// <summary>
        /// ProductPrice Repository
        /// </summary>
        private readonly IProductPriceRepository _repository;
        /// <summary>
        /// Product Repository
        /// </summary>
        private readonly IProductRepository _productRepository;
        /// <summary>
        /// Country Repository
        /// </summary>
        private readonly ICountryRepository _countryRepository;
        /// <summary>
        /// Log
        /// </summary>
        private readonly ILogger<ReadManager> _logger;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the Gtm.Business.Domain.Managers.ProductPrice.Read.ReadManager class.
        /// </summary>
        /// <param name="repository">ProductPrice Repository</param>
        /// <param name="productRepository">Product Repository</param>
        /// <param name="countryRepository">Country Repository</param>
        /// <param name="logger">Log</param>
        public ReadManager(
            IProductPriceRepository repository,
            IProductRepository productRepository,
            ICountryRepository countryRepository,
            ILogger<ReadManager> logger
        )
        {
            _repository = repository;
            _productRepository = productRepository;
            _countryRepository = countryRepository;
            _logger = logger;
        }

        #endregion

        #region Methods

        #region public

        /// <summary>
        /// Handles the ReadAll and asynchronously using Task.
        /// </summary>
        /// <param name="request">Request data</param>
        /// <returns>
        /// Task: Represents an asynchronous operation. 
        /// Response data.
        /// </returns>
        public async Task<ReadResponse> HandleAsync(ReadRequest request)
        {
            string methodName = nameof(HandleAsync);

            _logger.LogBeginInformation(methodName);

            var prices = await _repository.ReadAsync().ConfigureAwait(false);
            var products = await _productRepository.ReadAsync().ConfigureAwait(false);
            var countries = await _countryRepository.ReadAsync().ConfigureAwait(false);

            var productPrice = prices.FirstOrDefault(u => u.ProductId == request.ProductId);

            if (productPrice == null)
            {
                throw new RequestException(HttpStatusCode.NotFound, "Not Found!");
            }

            _logger.LogEndInformation(methodName);

            return new ReadResponse
            {
                ProductPrice = new ProductPriceModel
                {
                    ProductId = request.ProductId,
                    ProductName = products.FirstOrDefault(p => p.Id == request.ProductId)?.Description,
                    Countries = countries.Select(c => CreateCountryProductPrice(
                        c,
                        productPrice
                    ))
                }
            };
        }

        #endregion

        #region private

        /// <summary>
        /// Creates the model "CountryProductPrice"
        /// </summary>
        /// <param name="country">Country entity</param>
        /// <param name="productPrice">ProductPrice entity</param>
        /// <returns>Returns the model</returns>
        private static CountryProductPriceModel CreateCountryProductPrice(CountryEntity country, ProductPriceEntity productPrice)
        {
            var countryRate = productPrice.Rates.FirstOrDefault(r => r.CountryId == country.Id);

            return new CountryProductPriceModel
            {
                CountryId = country.Id,
                CountrytName = country.Name,
                IsSaved = countryRate != null,
                Rates = country.Rates.Select(r => new RateCountryProductPriceModel
                {
                    Rate = r,
                    IsSaved = r == countryRate?.Rate
                })
            };
        }

        #endregion

        #endregion
    }
}
