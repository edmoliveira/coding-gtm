using Gtm.Business.Domain.Managers.ProductPrice.Read;
using Gtm.Business.Domain.Managers.ProductPrice.Save;
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
        /// ProductPrice Manager "Read"
        /// </summary>
        private readonly IReadManager _readManager;
        /// <summary>
        /// ProductPrice Manager "Save"
        /// </summary>
        private readonly ISaveManager _saveManager;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the Gtm.Api.Controllers.Products.PricesController class.
        /// </summary>
        /// <param name="readManager">ProductPrice Manager "Read".</param>
        /// <param name="saveManager">ProductPrice Manager "Save"</param>
        /// <param name="logger">Log</param>
        public PricesController(
            IReadManager readManager,
            ISaveManager saveManager,
            ILogger<ProductController> logger) : base(logger)
        {
            _readManager = readManager;
            _saveManager = saveManager;
        }

        #endregion

        #region Methods

        #region public

        /// <summary>
        /// Gets all product prices.
        /// </summary>
        /// <returns>ReadResponse</returns>
        [ApiExplorerSettings(GroupName = Crud_GroupName)]
        [HttpGet()]
        [ApiVersion("1.0")]
        [Route("{v:apiVersion}")]
        [TypeFilter(typeof(RequestFilterAttribute))]
        [ProducesResponseType(typeof(ReadResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "Get All Prices",
            Description = "Gets all product prices."
        )]
        public async Task<IActionResult> GetAllAsync([FromQuery] ReadRequest request)
        {
            return await TryActionResultAsync(async () =>
            {
                string methodName = nameof(GetAllAsync);

                Logger.LogInformation(GetMethodBeginMessage(methodName));

                Logger.DebugIsEnabled(() => string.Concat("Request: ", JsonConvert.SerializeObject(request)));

                ReadResponse response = await _readManager.HandleAsync(request).ConfigureAwait(false);

                Logger.DebugIsEnabled(() => string.Concat("Response: ", JsonConvert.SerializeObject(response)));

                Logger.LogInformation(GetMethodEndMessage(methodName));

                return Ok(response);
            }).ConfigureAwait(false);
        }

        /// <summary>
        /// Saves all product prices.
        /// </summary>
        [ApiExplorerSettings(GroupName = Crud_GroupName)]
        [HttpPost()]
        [ApiVersion("1.0")]
        [Route("{v:apiVersion}")]
        [TypeFilter(typeof(RequestFilterAttribute))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "Save All Prices",
            Description = "Saves all product prices."
        )]
        public async Task<IActionResult> SaveAllAsync([FromBody] SaveRequest request)
        {
            return await TryActionResultAsync(async () =>
            {
                string methodName = nameof(SaveAllAsync);

                Logger.LogInformation(GetMethodBeginMessage(methodName));

                Logger.DebugIsEnabled(() => string.Concat("Request: ", JsonConvert.SerializeObject(request)));

                await _saveManager.HandleAsync(request).ConfigureAwait(false);

                Logger.LogInformation(GetMethodEndMessage(methodName));

                return Ok();
            }).ConfigureAwait(false);
        }

        #endregion

        #endregion
    }
}
