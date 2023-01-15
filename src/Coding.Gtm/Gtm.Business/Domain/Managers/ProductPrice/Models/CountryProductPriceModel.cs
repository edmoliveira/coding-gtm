namespace Gtm.Business.Domain.Managers.ProductPrice.Models
{
    /// <summary>
    /// Country model
    /// </summary>
    public sealed class CountryProductPriceModel
    {
        #region Properties

        /// <summary>
        /// Country id
        /// </summary>
        public long CountryId { get; set; }
        /// <summary>
        /// Country name
        /// </summary>
        public string CountrytName { get; set; }
        /// <summary>
        /// Indicates if it is saved 
        /// </summary>
        public bool IsSaved { get; set; }
        /// <summary>
        /// Rates
        /// </summary>
        public IEnumerable<RateCountryProductPriceModel> Rates { get; set; }

        #endregion
    }
}
