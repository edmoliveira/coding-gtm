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
        /// <summary>
        /// Price without VAT
        /// </summary>
        public decimal? PriceWithoutVAT { get; set; }
        /// <summary>
        /// Value Added Tax
        /// </summary>
        public int? VAT { get; set; }
        /// <summary>
        /// Price including VAT
        /// </summary>
        public decimal? PriceInclVAT { get; set; }

        #endregion
    }
}
