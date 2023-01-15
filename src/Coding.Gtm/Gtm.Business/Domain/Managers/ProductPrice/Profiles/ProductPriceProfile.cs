using AutoMapper;
using Gtm.Business.Domain.Managers.ProductPrice.Models;
using Gtm.Business.Infrastructure.Repositories.ProductPrice;

namespace Gtm.Business.Domain.Managers.ProductPrice.Profiles
{
    /// <summary>
    /// Configuration for maps.
    /// </summary>
    internal sealed class ProductPriceProfile : Profile
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the Gtm.Business.Domain.Managers.ProductPrice.Profiles.ProductPriceMapper class.
        /// </summary>
        public ProductPriceProfile()
        {
            CreateMap<RatePriceModel, RateProductPriceEntity>()
                .ForMember(dest => dest.CountryId, opt => opt.MapFrom(src => src.CountryId))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
                .ForMember(dest => dest.Rate, opt => opt.MapFrom(src => src.Rate));
        }

        #endregion
    }
}
