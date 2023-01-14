using Gtm.Business.Infrastructure.Helpers.Filters;
using Gtm.Business.Infrastructure.Helpers.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Gtm.Business.Infrastructure.Helpers
{
    /// <summary>
    /// Represents something that configures the JwtBearerOptions type.
    /// </summary>
    internal class ConfigureJwtBearerOptions : IConfigureNamedOptions<JwtBearerOptions>
    {
        #region Fields

        /// <summary>
        /// Application Configuration.
        /// </summary>
        private readonly IAppConfig _appConfig;
        /// <summary>
        /// Log
        /// </summary>
        private readonly ILogger<ConfigureJwtBearerOptions> _logger;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the  Gtm.Business.Infrastructure.Helpers.ConfigureJwtBearerOptions class.
        /// </summary>
        /// <param name="appConfig">Application Configuration.</param>
        /// <param name="logger">Log</param>
        public ConfigureJwtBearerOptions(
            IAppConfig appConfig,
            ILogger<ConfigureJwtBearerOptions> logger)
        {
            _appConfig = appConfig;
            _logger = logger;
        }

        #endregion

        #region Methods

        #region public

        /// <summary>
        /// Invoked to configure a TOptions instance.
        /// </summary>
        /// <param name="name">The name of the options instance being configured.</param>
        /// <param name="options">Options class provides information needed to control Bearer Authentication handler behavior</param>
        public void Configure(string name, JwtBearerOptions options)
        {
            if (name == JwtBearerDefaults.AuthenticationScheme)
            {
                options.RequireHttpsMetadata = true;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(ConvertStringToBytes(_appConfig.AuthTokenSecrect)),
                    ValidateIssuer = _appConfig.ValidateIssuer,
                    ValidIssuers = _appConfig.ValidIssuers,
                    ValidateAudience = _appConfig.ValidateAudience,
                    ValidAudiences = _appConfig.ValidAudiences,
                };
            }
        }

        /// <summary>
        /// Invoked to configure a TOptions instance.
        /// </summary>
        /// <param name="options">Options class provides information needed to control Bearer Authentication handler behavior</param>
        public void Configure(JwtBearerOptions options)
        {
            Configure(string.Empty, options);
        }

        #endregion

        #region private

        /// <summary>
        /// Convert a string to a byte array containing the results of encoding the specified set of characters
        /// </summary>
        /// <param name="value">The string containing the characters to encode.</param>
        /// <returns>A byte array containing the results of encoding the specified set of characters</returns>
        private static byte[] ConvertStringToBytes(string value)
        {
            return Encoding.ASCII.GetBytes(value);
        }

        #endregion

        #endregion
    }
}
