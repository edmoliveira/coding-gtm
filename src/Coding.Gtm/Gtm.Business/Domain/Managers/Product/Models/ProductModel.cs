namespace Gtm.Business.Domain.Managers.Product.Models
{
    /// <summary>
    /// Product model
    /// </summary>
    public sealed class ProductModel
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
