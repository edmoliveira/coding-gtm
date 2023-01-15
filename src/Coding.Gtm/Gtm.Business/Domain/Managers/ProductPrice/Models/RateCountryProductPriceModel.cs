namespace Gtm.Business.Domain.Managers.ProductPrice.Models
{
    /// <summary>
    /// Rate model
    /// </summary>
    public sealed class RateCountryProductPriceModel
    {
        #region Properties

        /// <summary>
        /// Country id
        /// </summary>
        public long CountryId { get; set; }
        /// <summary>
        /// Country rate
        /// </summary>
        public decimal Rate { get; set; }
        /// <summary>
        /// Indicates if it is saved 
        /// </summary>
        public bool IsSaved { get; set; }

        #endregion
    }
}
