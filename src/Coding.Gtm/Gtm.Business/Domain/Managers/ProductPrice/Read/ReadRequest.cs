using System.ComponentModel.DataAnnotations;

namespace Gtm.Business.Domain.Managers.ProductPrice.Read
{
    /// <summary>
    /// Request data
    /// </summary>
    public sealed class ReadRequest
    {
        #region Properties

        /// <summary>
        /// Product id
        /// </summary>
        [Required]
        public long ProductId { get; set; }

        #endregion
    }
}
