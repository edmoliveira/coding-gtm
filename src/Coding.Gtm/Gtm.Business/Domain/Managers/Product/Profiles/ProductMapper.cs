using AutoMapper;
using Gtm.Business.Domain.Managers.Product.Models;
using Gtm.Business.Infrastructure.Repositories.Product;

namespace Gtm.Business.Domain.Managers.Product.Profiles
{
    /// <summary>
    /// Configuration for maps.
    /// </summary>
    internal sealed class ProductMapper : Profile
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the Gtm.Business.Domain.Managers.Product.Profiles.ProductMapper class.
        /// </summary>
        public ProductMapper()
        {
            CreateMap<ProductEntity, ProductModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));
        }

        #endregion
    }
}
