namespace Gtm.Business.Infrastructure.Repositories.Country
{
    /// <summary>
    /// Mapping to Country file.
    /// </summary>
    internal sealed class CountryEntity
    {
        #region Properties

        /// <summary>
        /// Country id
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// Country name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Country rates
        /// </summary>
        public decimal[] Rates { get; set; }

        #endregion
    }
}
