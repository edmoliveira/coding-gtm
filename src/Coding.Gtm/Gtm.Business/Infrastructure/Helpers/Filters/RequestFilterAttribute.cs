using Gtm.Business.Infrastructure.Helpers.Extensions;
using Gtm.Business.Infrastructure.Helpers.Interfaces;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using System.Security.Claims;

namespace Gtm.Business.Infrastructure.Helpers.Filters
{
    /// <summary>
    /// Request Filter.
    /// </summary>
    public sealed class RequestFilterAttribute : ActionFilterAttribute
    {
        #region Fields

        /// <summary>
        /// One instance throughout the application per request.
        /// </summary>
        private readonly ISetApplicationContext _applicationContext;
        /// <summary>
        /// Log
        /// </summary>
        private readonly ILogger<RequestFilterAttribute> _logger;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the  Gtm.Business.Infrastructure.Filters.LogFilterAttribute class.
        /// </summary>
        /// <param name="applicationContext">One instance throughout the application per request.</param>
        /// <param name="logger">Log</param>
        public RequestFilterAttribute(
            ISetApplicationContext applicationContext, 
            ILogger<RequestFilterAttribute> logger)
        {
            _applicationContext = applicationContext;
            _logger = logger;
        }

        #endregion

        #region Method public

        /// <summary>
        /// Called before the action method is invoked.
        /// </summary>
        /// <param name="context">Information about the current request and action</param>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.HttpContext.Request.Headers.TryGetValue(ApplicationResource.RequestIdHeaderKey, out StringValues requestId))
            {
                _applicationContext.SetRequestId(requestId.FirstOrDefault());
            }
            else
            {
                _applicationContext.SetRequestId(Guid.NewGuid().ToString("N"));
            }

            if (context.HttpContext.User.Identity is ClaimsIdentity claimsIdentity &&
                claimsIdentity.IsAuthenticated)
            {
                _applicationContext.SetLoggedUser(new LoggedUser
                {
                    Id = long.Parse(claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value),
                    Login = claimsIdentity.FindFirst(ClaimTypes.Name).Value
                });
            }
            else
            {
                _applicationContext.SetLoggedUser(new LoggedUser
                {
                    Id = -1,
                    Login = "Anonymous",
                });
            }

            _logger.DebugIsEnabled(() => string.Concat("Request.Query: ", JsonConvert.SerializeObject(context?.HttpContext?.Request?.Query)));
        }

        #endregion
    }
}
