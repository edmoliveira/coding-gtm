using Gtm.Business.Infrastructure.Helpers.Interfaces;

namespace Gtm.Business.Infrastructure.Helpers
{
    /// <summary>
    /// Provides the metadata about the Open API.
    /// </summary>
    internal sealed class ConfigurationOpenApiInfo : IConfigurationOpenApiInfo
    {
        #region Properties 

        /// <summary>
        /// A URI-friendly name that uniquely identifies the document
        /// </summary>
        public string DocumentName { get; set; }
        /// <summary>
        /// The title of the application.
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// A short description of the application.
        /// </summary>
        public string Description { get; set; }

        #endregion
    }
}
