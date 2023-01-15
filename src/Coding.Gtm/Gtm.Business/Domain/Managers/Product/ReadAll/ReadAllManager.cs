using AutoMapper;
using Gtm.Business.Domain.Managers.Product.Models;
using Gtm.Business.Infrastructure.Helpers.Extensions;
using Gtm.Business.Infrastructure.Repositories.Product;
using Microsoft.Extensions.Logging;

namespace Gtm.Business.Domain.Managers.Product.ReadAll
{
    /// <summary>
    /// Product Manager
    /// </summary>
    internal sealed class ReadAllManager : IReadAllManager
    {
        #region Fields

        /// <summary>
        /// Product Repository
        /// </summary>
        private readonly IProductRepository _repository;
        /// <summary>
        /// Data Mapper 
        /// </summary>
        private readonly IMapper _mapper;
        /// <summary>
        /// Log
        /// </summary>
        private readonly ILogger<ReadAllManager> _logger;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the Gtm.Business.Domain.Managers.Product.ReadAll.ReadAllManager class.
        /// </summary>
        /// <param name="repository">Product Repository</param>
        /// <param name="mapper">Data Mapper </param>
        /// <param name="logger">Log</param>
        public ReadAllManager(
            IProductRepository repository,
            IMapper mapper,
            ILogger<ReadAllManager> logger
        )
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        #endregion

        #region Methods

        #region public

        /// <summary>
        /// Handles the ReadAll and asynchronously using Task.
        /// </summary>
        /// <returns>
        /// Task: Represents an asynchronous operation. 
        /// Response data.
        /// </returns>
        public async Task<ReadAllResponse> HandleAsync()
        {
            string methodName = nameof(HandleAsync);

            _logger.LogBeginInformation(methodName);

            var products = await _repository.ReadAsync().ConfigureAwait(false);

            _logger.LogEndInformation(methodName);

            return new ReadAllResponse
            {
                Products = _mapper.Map<IEnumerable<ProductModel>>(products)
            };
        }

        #endregion

        #endregion
    }
}
