namespace Gtm.Business.Infrastructure.Helpers.Interfaces
{
    /// <summary>
    /// Provides the metadata about the Open API.
    /// </summary>
    internal interface IConfigurationOpenApiInfo
    {
        #region Properties 

        /// <summary>
        /// A URI-friendly name that uniquely identifies the document
        /// </summary>
        string DocumentName { get; }
        /// <summary>
        /// The title of the application.
        /// </summary>
        string Title { get; }
        /// <summary>
        /// A short description of the application.
        /// </summary>
        string Description { get; }

        #endregion
    }
}
