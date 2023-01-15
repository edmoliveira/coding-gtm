using Gtm.Business.Domain.Managers.ProductPrice.Read;
using Gtm.Business.Infrastructure.Helpers.Controllers;
using Gtm.Business.Infrastructure.Helpers.Extensions;
using Gtm.Business.Infrastructure.Helpers.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;

namespace Gtm.Api.Controllers.Products
{
    /// <summary>
    /// Product CRUD.
    /// </summary>
    [ApiController]
    [Produces("application/json")]
    [Route("api/products/prices")]
    [Authorize()]
    public sealed class PricesController : CustomControllerBase
    {
        #region Fields

        /// <summary>
        /// ProductPrice Manager
        /// </summary>
        private readonly IReadManager _manager;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the Gtm.Api.Controllers.Products.PricesController class.
        /// </summary>
        /// <param name="manager">ProductPrice Manager.</param>
        /// <param name="logger">Log</param>
        public PricesController(IReadManager manager, ILogger<ProductController> logger) : base(logger)
        {
            _manager = manager;
        }

        #endregion

        #region Methods

        #region public

        /// <summary>
        /// Get all product prices in countries.
        /// </summary>
        /// <returns>ReadResponse</returns>
        [ApiExplorerSettings(GroupName = Crud_GroupName)]
        [HttpGet()]
        [ApiVersion("1.0")]
        [Route("{v:apiVersion}")]
        [TypeFilter(typeof(RequestFilterAttribute))]
        [ProducesResponseType(typeof(ReadResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "Get All Prices",
            Description = "Get all product prices in countries."
        )]
        public async Task<IActionResult> GetAllAsync([FromQuery] ReadRequest request)
        {
            return await TryActionResultAsync(async () =>
            {
                string methodName = nameof(GetAllAsync);

                Logger.LogInformation(GetMethodBeginMessage(methodName));

                Logger.DebugIsEnabled(() => string.Concat("Request: ", JsonConvert.SerializeObject(request)));

                ReadResponse response = await _manager.HandleAsync(request).ConfigureAwait(false);

                Logger.DebugIsEnabled(() => string.Concat("Response: ", JsonConvert.SerializeObject(response)));

                Logger.LogInformation(GetMethodEndMessage(methodName));

                return Ok(response);
            }).ConfigureAwait(false);
        }

        #endregion

        #endregion
    }
}
