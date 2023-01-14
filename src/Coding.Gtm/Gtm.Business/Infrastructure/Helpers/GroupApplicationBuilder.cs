using Gtm.Business.Infrastructure.Helpers.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Gtm.Business.Infrastructure.Helpers
{
    /// <summary>
    /// Defines a class that provides the mechanisms to configure an application's request
    /// pipeline.
    /// </summary>
    public static class GroupApplicationBuilder
    {
        #region Methods 

        #region public

        /// <summary>
        /// A configuration group for an application's request.
        /// </summary>
        /// <param name="app">The web application used to configure the HTTP pipeline, and routes.</param>
        /// <param name="configuration">Represents a set of key/value application configuration properties.</param>
        /// <param name="env">Provides information about the web hosting environment an application is running in.</param>
        /// <param name="allowSpecificOrigins">Allow specific origins.</param>
        public static void UseGroup(this WebApplication app, IConfiguration configuration, IWebHostEnvironment env, string allowSpecificOrigins)
        {
            app.UseSwagger(c =>
            {
                c.RouteTemplate = "/api-docs/{documentName}/swagger.json";
            });

            app.UseSwaggerUI(c =>
            {
                IAppConfig appConfig = configuration.GetSection("AppConfig").Get<AppConfig>();

                foreach (IConfigurationOpenApiInfo item in appConfig.Documents)
                {
                    c.SwaggerEndpoint($"/api-docs/{item.DocumentName}/swagger.json", item.DocumentName);
                }

                c.RoutePrefix = string.Empty;
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage()
                    .UseCors(policy =>
                    policy
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials()
                        .SetIsOriginAllowed(hostName => true)
                   );
            }
            else
            {
                app.UseCors(allowSpecificOrigins);
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseStaticFiles();

            app.MapControllers();

            app.Run();
        }

        #endregion

        #endregion
    }
}
