using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gtm.Business.Infrastructure.Repositories.ProductPrice
{
    /// <summary>
    /// Mapping to ProductPrice file.
    /// </summary>
    internal sealed class ProductPriceEntity
    {
        #region Properties

        /// <summary>
        /// Product id
        /// </summary>
        public long ProductId { get; set; }
        /// <summary>
        /// Rates
        /// </summary>
        public IEnumerable<RateProductPriceEntity> Rates { get; set; }

        #endregion
    }
}
