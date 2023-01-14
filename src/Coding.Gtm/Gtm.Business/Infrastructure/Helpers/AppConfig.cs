using Gtm.Business.Infrastructure.Helpers.Interfaces;

namespace Gtm.Business.Infrastructure.Helpers
{
    /// <summary>
    /// Aplication configuration
    /// </summary>
    internal sealed class AppConfig : IAppConfig
    {
        #region Properties

        /// <summary>
        /// Specifies the name of the application.
        /// </summary>
        public string ApplicationName { get; set; }
        /// <summary>
        /// Specifies the title of the application.
        /// </summary>
        public string ApplicationTitle { get; set; }
        /// <summary>
        /// Authentication token secrect.
        /// </summary>
        public string AuthTokenSecrect { get; set; }
        /// <summary>
        /// Authentication Token expiration in minutes.
        /// </summary>
        public double AuthTokenExpireSeconds { get; set; }
        /// <summary>
        /// Array of Allowed Origins
        /// </summary>
        public string[] AllowedOrigins { get; set; }
        /// <summary>
        /// Array of Allowed Methods
        /// </summary>
        public string[] AllowedMethods { get; set; }
        /// <summary>
        /// Array of Allowed Headers
        /// </summary>
        public string[] AllowedHeaders { get; set; }
        /// <summary>
        /// Controls if the issuer will be validated during token validation.
        /// </summary>
        public bool ValidateIssuer { get; set; }
        /// <summary>
        /// Contains valid issuers that will be used to check against the token's issuer.
        /// </summary>
        public string[] ValidIssuers { get; set; }
        /// <summary>
        /// Controls if the audience will be validated during token validation.
        /// </summary>
        public bool ValidateAudience { get; set; }
        /// <summary>
        /// Contains valid audiences that will be used to check against the token's audience.
        /// </summary>
        public string[] ValidAudiences { get; set; }
        /// <summary>
        /// Swagger Documents
        /// </summary>
        public List<ConfigurationOpenApiInfo> Documents { get; set; }
        /// <summary>
        /// Swagger Documents
        /// </summary>
        IReadOnlyList<IConfigurationOpenApiInfo> IAppConfig.Documents { get { return Documents; } }
        /// <summary>
        /// A URL to the Terms of Service for the API. MUST be in the format of a URL.
        /// </summary>
        public string TermsOfServiceOpenApi { get; set; }
        /// <summary>
        /// The identifying name of the contact person/organization.
        /// </summary>
        public string NameOpenApiContact { get; set; }
        /// <summary>
        /// The URL pointing to the contact information. MUST be in the format of a URL.
        /// </summary>
        public string UrlOpenApiContact { get; set; }
        /// <summary>
        /// The email address of the contact person/organization. MUST be in the format of an email address.
        /// </summary>
        public string EmailOpenApiContact { get; set; }
        /// <summary>
        /// The license name used for the API.
        /// </summary>
        public string NameOpenApiLicense { get; set; }
        /// <summary>
        /// The URL pointing to the contact information. MUST be in the format of a URL.
        /// </summary>
        public string UrlOpenApiLicense { get; set; }
        /// <summary>
        /// Database
        /// </summary>
        public Database Database { get; set; }
        /// <summary>
        /// Database
        /// </summary>
        IDatabase IAppConfig.Database => Database;

        #endregion
    }
}
