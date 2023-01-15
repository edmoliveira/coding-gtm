using AutoMapper;
using Gtm.Business.Domain.Managers.Product.Profiles;

namespace Gtm.Business.Domain.Managers.Product
{
    /// <summary>
    /// Extension methods to add ProductManager mappers.
    /// </summary>
    internal static class ProductManagerMappers
    {
        #region Methods

        #region public

        /// <summary>
        /// Adds an existing profiles type. Profile will be instantiated and added to the configuration.
        /// </summary>
        /// <param name="services">Create a MapperConfiguration instance and initialize configuration via the constructor.</param>
        public static void AddProductManagerProfiles(this IMapperConfigurationExpression configuration)
        {
            configuration.AddProfile<ProductMapper>();
        }

        #endregion

        #endregion
    }
}
