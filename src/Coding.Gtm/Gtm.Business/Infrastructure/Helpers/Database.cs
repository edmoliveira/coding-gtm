using Gtm.Business.Infrastructure.Helpers.Interfaces;

namespace Gtm.Business.Infrastructure.Helpers
{
    /// <summary>
    /// Database
    /// </summary>
    internal sealed class Database: IDatabase
    {
        #region Properties

        /// <summary>
        /// Country table path
        /// </summary>
        public string Country { get; set; }
        /// <summary>
        /// Product table path
        /// </summary>
        public string Product { get; set; }
        /// <summary>
        /// ProductPrice table path
        /// </summary>
        public string ProductPrice { get; set; }
        /// <summary>
        /// User table path
        /// </summary>
        public string User { get; set; }

        #endregion
    }
}
