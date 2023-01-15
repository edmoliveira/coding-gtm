using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Gtm.Business.Infrastructure.Helpers.Filters
{
    /// <summary>
    /// Header parameter attribute's operation filter
    /// </summary>
    public sealed class HeaderParameterOperationFilter : IOperationFilter
    {
        #region Methods

        #region public

        /// <summary>
        /// Applies the filter
        /// </summary>
        /// <param name="operation">Operation Object.</param>
        /// <param name="context">OperationFilterContext</param>
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var filterPipeline = context.ApiDescription.ActionDescriptor.FilterDescriptors;
            var isAuthorized = filterPipeline.Select(filterInfo => filterInfo.Filter).Any(filter => filter is AuthorizeFilter);

            if (operation.Parameters == null)
                operation.Parameters = new List<OpenApiParameter>();

            if (isAuthorized)
            {
                operation.Security = new List<OpenApiSecurityRequirement>
                {
                    new OpenApiSecurityRequirement()
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Description = "JWT Authorization header using the Bearer scheme.",
                                Name = "Authorization",
                                Type = SecuritySchemeType.Http,
                                In = ParameterLocation.Header,
                                Scheme = "bearer",
                                Reference = new OpenApiReference {
                                    Type = ReferenceType.SecurityScheme, Id = "Bearer"
                                }
                            },
                            new List<string>()
                        }
                    }
                };
            }

            operation.Parameters.Add(new OpenApiParameter
            {
                Name = ApplicationResource.RequestIdHeaderKey,
                In = ParameterLocation.Header,
                Description = "Request id for all transaction in the platform.",
                Required = false,
                Schema = new OpenApiSchema
                {
                    Type = "string"
                }
            });
        }

        #endregion

        #endregion
    }
}
