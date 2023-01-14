using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gtm.Business.Infrastructure.Helpers
{
    /// <summary>
    /// A Microsoft.AspNetCore.Mvc.StatusCodeResult that when executed will produce a Internal Server Error (500) response.
    /// </summary>
    public class InternalServerErrorResult : StatusCodeResult
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the Gtm.Business.Infrastructure.Helpers.InternalServerErrorResult class.
        /// </summary>
        public InternalServerErrorResult() : base(StatusCodes.Status500InternalServerError)
        {

        }

        #endregion
    }
}
