using Gtm.Business.Domain.Managers.ProductPrice.Models;

namespace Gtm.Business.Domain.Managers.ProductPrice.Read
{
    /// <summary>
    /// Response data.
    /// </summary>
    public sealed class ReadResponse
    {
        #region Properties

        /// <summary>
        /// Product prices
        /// </summary>
        public ProductPriceModel ProductPrice { get; set; }

        #endregion
    }
}
