namespace Gtm.Business.Infrastructure.Helpers.Interfaces
{
    /// <summary>
    /// Application Configuration
    /// </summary>
    internal interface IAppConfig
    {
        #region Properties

        /// <summary>
        /// Specifies the name of the application.
        /// </summary>
        string ApplicationName { get; }
        /// <summary>
        /// Specifies the title of the application.
        /// </summary>
        string ApplicationTitle { get; }
        /// <summary>
        /// Authentication token secrect.
        /// </summary>
        string AuthTokenSecrect { get; }
        /// <summary>
        /// Authentication Token expiration in minutes.
        /// </summary>
        double AuthTokenExpireSeconds { get; }
        /// <summary>
        /// Array of Allowed Origins
        /// </summary>
        string[] AllowedOrigins { get; }
        /// <summary>
        /// Array of Allowed Methods
        /// </summary>
        string[] AllowedMethods { get; }
        /// <summary>
        /// Array of Allowed Headers
        /// </summary>
        string[] AllowedHeaders { get; }
        /// <summary>
        /// Controls if the issuer will be validated during token validation.
        /// </summary>
        bool ValidateIssuer { get; }
        /// <summary>
        /// Contains valid issuers that will be used to check against the token's issuer.
        /// </summary>
        string[] ValidIssuers { get; }
        /// <summary>
        /// Controls if the audience will be validated during token validation.
        /// </summary>
        bool ValidateAudience { get; }
        /// <summary>
        /// Contains valid audiences that will be used to check against the token's audience.
        /// </summary>
        string[] ValidAudiences { get; }
        /// <summary>
        /// Swagger Documents
        /// </summary>
        IReadOnlyList<IConfigurationOpenApiInfo> Documents { get; }
        /// <summary>
        /// A URL to the Terms of Service for the API. MUST be in the format of a URL.
        /// </summary>
        string TermsOfServiceOpenApi { get; }
        /// <summary>
        /// The identifying name of the contact person/organization.
        /// </summary>
        string NameOpenApiContact { get; }
        /// <summary>
        /// The URL pointing to the contact information. MUST be in the format of a URL.
        /// </summary>
        string UrlOpenApiContact { get; }
        /// <summary>
        /// The email address of the contact person/organization. MUST be in the format of an email address.
        /// </summary>
        string EmailOpenApiContact { get; }
        /// <summary>
        /// The license name used for the API.
        /// </summary>
        string NameOpenApiLicense { get; }
        /// <summary>
        /// The URL pointing to the contact information. MUST be in the format of a URL.
        /// </summary>
        string UrlOpenApiLicense { get; }
        /// <summary>
        /// Database
        /// </summary>
        IDatabase Database { get; }

        #endregion
    }
}
