namespace Gtm.Business.Infrastructure.Helpers.Interfaces
{
    /// <summary>
    /// Database
    /// </summary>
    internal interface IDatabase
    {
        #region Properties

        /// <summary>
        /// Country table path
        /// </summary>
        string Country { get; }
        /// <summary>
        /// Product table path
        /// </summary>
        string Product { get; }
        /// <summary>
        /// ProductPrice table path
        /// </summary>
        string ProductPrice { get; }
        /// <summary>
        /// User table path
        /// </summary>
        string User { get; }

        #endregion
    }
}
