using Gtm.Business.Domain.Managers.User.SignIn;
using Gtm.Business.Infrastructure.Helpers.Controllers;
using Gtm.Business.Infrastructure.Helpers.Extensions;
using Gtm.Business.Infrastructure.Helpers.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;

namespace Gtm.Api.Controllers.Users
{
    /// <summary>
    /// Platform security.
    /// </summary>
    [ApiController]
    [Produces("application/json")]
    [Route("api/users/sign-in")]
    [AllowAnonymous]
    public class SignInController : CustomControllerBase
    {
        #region Fields private

        /// <summary>
        /// User Login Manager.
        /// </summary>
        private readonly ISignInManager _manager;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the Gtm.Api.Controllers.Users.SignInController class.
        /// </summary>
        /// <param name="manager">User Login Manager.</param>
        /// <param name="logger">Log</param>
        public SignInController(ISignInManager manager, ILogger<SignInController> logger) : base(logger)
        {
            _manager = manager;
        }

        #endregion

        #region Method public

        /// <summary>
        /// Login to the platform with credentials.
        /// </summary>
        /// <param name="request">Request data</param>
        /// <returns>SignInResponse</returns>
        [ApiExplorerSettings(GroupName = Security_GroupName)]
        [HttpPost()]
        [ApiVersion("1.0")]
        [Route("{v:apiVersion}")]
        [TypeFilter(typeof(RequestFilterAttribute))]
        [ProducesResponseType(typeof(SignInResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "Sign In",
            Description = "Login to the platform with credentials."
        )]
        public async Task<IActionResult> SignInAsync(SignInRequest request)
        {
            return await TryActionResultAsync(async () =>
            {
                string methodName = nameof(SignInAsync);

                Logger.LogInformation(GetMethodBeginMessage(methodName));

                Logger.DebugIsEnabled(() => string.Concat("Request: ", JsonConvert.SerializeObject(request)));

                SignInResponse response = await _manager.HandleAsync(request).ConfigureAwait(false);

                Logger.DebugIsEnabled(() => string.Concat("Response: ", JsonConvert.SerializeObject(response)));

                Logger.LogInformation(GetMethodEndMessage(methodName));

                return Ok(response);
            }).ConfigureAwait(false);
        }

        #endregion
    }
}
