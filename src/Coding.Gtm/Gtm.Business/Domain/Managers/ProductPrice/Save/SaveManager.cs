using AutoMapper;
using Gtm.Business.Infrastructure.Helpers.Exceptions;
using Gtm.Business.Infrastructure.Helpers.Extensions;
using Gtm.Business.Infrastructure.Repositories.Product;
using Gtm.Business.Infrastructure.Repositories.ProductPrice;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Gtm.Business.Domain.Managers.ProductPrice.Save
{
    /// <summary>
    /// ProductPrice Manager "Save"
    /// </summary>
    internal sealed class SaveManager : ISaveManager
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
        /// Data Mapper 
        /// </summary>
        private readonly IMapper _mapper;
        /// <summary>
        /// Log
        /// </summary>
        private readonly ILogger<SaveManager> _logger;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the Gtm.Business.Domain.Managers.ProductPrice.Save.SaveManager class.
        /// </summary>
        /// <param name="repository">ProductPrice Repository</param>
        /// <param name="productRepository">Product Repository</param>
        /// <param name="mapper">Data Mapper</param>
        /// <param name="logger">Log</param>
        public SaveManager(
            IProductPriceRepository repository,
            IProductRepository productRepository,
            IMapper mapper,
            ILogger<SaveManager> logger
        )
        {
            _repository = repository;
            _productRepository = productRepository;
            _mapper = mapper;
            _logger = logger;
        }

        #endregion

        #region Methods

        #region public

        /// <summary>
        /// Handles the Save and asynchronously using Task.
        /// </summary>
        /// <param name="request">Request data</param>
        /// <returns>Represents an asynchronous operation.</returns>
        public async Task HandleAsync(SaveRequest request)
        {
            string methodName = nameof(HandleAsync);

            _logger.LogBeginInformation(methodName);

            var prices = (await _repository.ReadAsync().ConfigureAwait(false)).ToList();

            var productPrice = prices.FirstOrDefault(u => u.ProductId == request.ProductId);

            if (productPrice == null)
            {
                var products = await _productRepository.ReadAsync().ConfigureAwait(false);

                if (!products.Any(p => p.Id == request.ProductId))
                {
                    throw new RequestException(HttpStatusCode.NotFound, "Product not found!");
                }

                productPrice = new ProductPriceEntity
                {
                    ProductId = request.ProductId,
                };

                prices.Add(productPrice);
            }

            productPrice.Rates = _mapper.Map<IEnumerable<RateProductPriceEntity>>(request.Rates);

            await _repository.SaveAsync(prices).ConfigureAwait(false);

            _logger.LogEndInformation(methodName);
        }

        #endregion

        #endregion
    }
}
