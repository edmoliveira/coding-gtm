namespace Gtm.Business.Infrastructure.Repositories.ProductPrice
{
    /// <summary>
    /// Encapsulation of logic to access data sources.
    /// </summary>
    internal interface IProductPriceRepository
    {
        #region Methods 

        /// <summary>
        /// Saves products prices in the file.
        /// </summary>
        /// <param name="prices">product price list</param>
        /// <returns>Represents an asynchronous operation. </returns>
        Task SaveAsync(IEnumerable<ProductPriceEntity> prices);

        /// <summary>
        /// Read products prices from the file.
        /// </summary>
        /// <returns>A task that represents the asynchronous read operation, which wraps the list containing all products prices in the file.</returns>
        Task<IEnumerable<ProductPriceEntity>> ReadAsync();

        #endregion
    }
}
