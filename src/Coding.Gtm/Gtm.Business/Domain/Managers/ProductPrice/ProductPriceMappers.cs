using AutoMapper;
using Gtm.Business.Domain.Managers.ProductPrice.Profiles;

namespace Gtm.Business.Domain.Managers.ProductPrice
{
    /// <summary>
    /// Extension methods to add ProductPrice mappers.
    /// </summary>
    internal static class ProductPriceMappers
    {
        #region Methods

        #region public

        /// <summary>
        /// Adds an existing profiles type. Profile will be instantiated and added to the configuration.
        /// </summary>
        /// <param name="services">Create a MapperConfiguration instance and initialize configuration via the constructor.</param>
        public static void AddProductPriceProfiles(this IMapperConfigurationExpression configuration)
        {
            configuration.AddProfile<ProductPriceProfile>();
        }

        #endregion

        #endregion
    }
}
