using Gtm.Business.Domain.Managers.ProductPrice.Models;

namespace Gtm.Business.Domain.Managers.ProductPrice.Save
{
    /// <summary>
    /// Request data
    /// </summary>
    public sealed class SaveRequest
    {
        #region Properties

        /// <summary>
        /// Product id
        /// </summary>
        public long ProductId { get; set; }
        /// <summary>
        /// Rates
        /// </summary>
        public IEnumerable<RatePriceModel> Rates { get; set; }

        #endregion
    }
}
