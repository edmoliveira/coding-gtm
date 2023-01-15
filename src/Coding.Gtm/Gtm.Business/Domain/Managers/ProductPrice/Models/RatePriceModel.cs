namespace Gtm.Business.Domain.Managers.ProductPrice.Models
{
    /// <summary>
    /// Rate and Price model
    /// </summary>
    public sealed class RatePriceModel
    {
        #region Properties

        /// <summary>
        /// Country id
        /// </summary>
        public long CountryId { get; set; }
        /// <summary>
        /// Price
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// Rate
        /// </summary>
        public decimal Rate { get; set; }

        #endregion
    }
}
