using AutoMapper;
using Gtm.Business.Domain.Managers.Product.Profiles;

namespace Gtm.Business.Domain.Managers.Product
{
    /// <summary>
    /// Extension methods to add Product mappers.
    /// </summary>
    internal static class ProductMappers
    {
        #region Methods

        #region public

        /// <summary>
        /// Adds an existing profiles type. Profile will be instantiated and added to the configuration.
        /// </summary>
        /// <param name="services">Create a MapperConfiguration instance and initialize configuration via the constructor.</param>
        public static void AddProductProfiles(this IMapperConfigurationExpression configuration)
        {
            configuration.AddProfile<ProductProfile>();
        }

        #endregion

        #endregion
    }
}
