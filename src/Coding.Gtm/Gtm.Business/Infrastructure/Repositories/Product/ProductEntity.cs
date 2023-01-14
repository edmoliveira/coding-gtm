namespace Gtm.Business.Infrastructure.Repositories.Product
{
    /// <summary>
    /// Mapping to Product file.
    /// </summary>
    internal sealed class ProductEntity
    {
        #region Properties

        /// <summary>
        /// Product id
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// Product description
        /// </summary>
        public string Description { get; set; }

        #endregion
    }
}
