namespace Gtm.Business.Domain.Managers.ProductPrice.Models
{
    /// <summary>
    /// ProductPrice model
    /// </summary>
    public sealed class ProductPriceModel
    {
        #region Properties

        /// <summary>
        /// Product id
        /// </summary>
        public long ProductId { get; set; }
        /// <summary>
        /// Product name
        /// </summary>
        public string ProductName { get; set; }
        /// <summary>
        /// Countries
        /// </summary>
        public IEnumerable<CountryProductPriceModel> Countries { get; set; }

        #endregion
    }
}
