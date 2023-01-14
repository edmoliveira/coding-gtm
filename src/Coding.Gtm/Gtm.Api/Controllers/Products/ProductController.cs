using Gtm.Business.Infrastructure.Helpers.Controllers;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.Net;
using Gtm.Business.Domain.Product.ReadAll;
using Gtm.Business.Infrastructure.Helpers.Extensions;
using Newtonsoft.Json;
using Gtm.Business.Infrastructure.Helpers.Filters;

namespace Gtm.Api.Controllers.Products
{
    /// <summary>
    /// Product CRUD.
    /// </summary>
    [ApiController]
    [Produces("application/json")]
    [Route("api/titles")]
    public sealed class ProductController : CustomControllerBase
    {
        #region Fields

        /// <summary>
        /// Product Manager
        /// </summary>
        private readonly IReadAllManager _manager;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the Gtm.Api.Controllers.Products.ProductController class.
        /// </summary>
        /// <param name="manager">Product Manager.</param>
        /// <param name="logger">Log</param>
        public ProductController(IReadAllManager manager, ILogger<ProductController> logger) : base(logger)
        {
            _manager = manager;
        }

        #endregion

        #region Methods

        #region public

        /// <summary>
        /// Gets all registered products.
        /// </summary>
        /// <returns>IEnumerable<ReadAllResponse></returns>
        [ApiExplorerSettings(GroupName = Crud_GroupName)]
        [HttpGet()]
        [ApiVersion("1.0")]
        [Route("{v:apiVersion}")]
        [TypeFilter(typeof(RequestFilterAttribute))]
        [ProducesResponseType(typeof(ReadAllResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "Get All Products",
            Description = "Gets all registered products."
        )]
        public async Task<IActionResult> GetAllAsync()
        {
            return await TryActionResultAsync(async () =>
            {
                string methodName = nameof(GetAllAsync);

                Logger.LogInformation(GetMethodBeginMessage(methodName));

                ReadAllResponse response = await _manager.HandleAsync().ConfigureAwait(false);

                Logger.DebugIsEnabled(() => string.Concat("Response: ", JsonConvert.SerializeObject(response)));

                Logger.LogInformation(GetMethodEndMessage(methodName));

                return Ok(response);
            }).ConfigureAwait(false);
        }

        #endregion

        #endregion
    }
}
