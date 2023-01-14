using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gtm.Business.Domain.Product.Models;

namespace Gtm.Business.Domain.Product.ReadAll
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
