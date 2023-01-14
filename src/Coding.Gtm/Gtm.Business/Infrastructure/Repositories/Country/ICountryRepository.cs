namespace Gtm.Business.Infrastructure.Repositories.Country
{
    /// <summary>
    /// Encapsulation of logic to access data sources.
    /// </summary>
    internal interface ICountryRepository
    {
        #region Methods 

        /// <summary>
        /// Saves the countries in the file.
        /// </summary>
        /// <param name="countries">Country list</param>
        /// <returns>Represents an asynchronous operation. </returns>
        Task SaveAsync(IEnumerable<CountryEntity> countries);

        /// <summary>
        /// Read countries from the file.
        /// </summary>
        /// <returns>A task that represents the asynchronous read operation, which wraps the list containing all countries in the file.</returns>
        Task<IEnumerable<CountryEntity>> ReadAsync();

        #endregion
    }
}
