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
    internal sealed class RateProductPriceEntity
    {
        #region Properties

        /// <summary>
        /// Country id
        /// </summary>
        public long CountryId { get; set; }
        /// <summary>
        /// Rate
        /// </summary>
        public decimal Rate { get; set; }

        #endregion
    }
}
