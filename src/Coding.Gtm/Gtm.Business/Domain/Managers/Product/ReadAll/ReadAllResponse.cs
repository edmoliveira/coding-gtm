using Gtm.Business.Domain.Managers.Product.Models;

namespace Gtm.Business.Domain.Managers.Product.ReadAll
{
    /// <summary>
    /// Response data.
    /// </summary>
    public sealed class ReadAllResponse
    {
        #region Properties

        /// <summary>
        /// Products
        /// </summary>
        public IEnumerable<ProductModel> Products { get; set; }

        #endregion
    }
}
