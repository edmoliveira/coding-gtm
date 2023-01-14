﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gtm.Business.Infrastructure.Helpers
{
    /// <summary>
    /// An Microsoft.AspNetCore.Mvc.ObjectResult that when executed will produce a Internal Server Error (500) response.
    /// </summary>
    public class InternalServerErrorObjectResult : ObjectResult
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the  Gtm.Business.Infrastructure.Helpers.InternalServerErrorObjectResult class.
        /// </summary>
        /// <param name="value">The content to format into the entity body.</param>
        public InternalServerErrorObjectResult(object value) : base(value)
        {
            StatusCode = StatusCodes.Status500InternalServerError;
        }

        #endregion
    }
}
