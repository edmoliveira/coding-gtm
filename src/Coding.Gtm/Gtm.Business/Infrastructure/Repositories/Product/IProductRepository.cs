namespace Gtm.Business.Infrastructure.Repositories.Product
{
    /// <summary>
    /// Encapsulation of logic to access data sources.
    /// </summary>
    internal interface IProductRepository
    {
        #region Methods 

        /// <summary>
        /// Saves the products in the file.
        /// </summary>
        /// <param name="products">Product list</param>
        /// <returns>Represents an asynchronous operation. </returns>
        Task SaveAsync(IEnumerable<ProductEntity> products);

        /// <summary>
        /// Read products from the file.
        /// </summary>
        /// <returns>A task that represents the asynchronous read operation, which wraps the list containing all products in the file.</returns>
        Task<IEnumerable<ProductEntity>> ReadAsync();

        #endregion
    }
}
