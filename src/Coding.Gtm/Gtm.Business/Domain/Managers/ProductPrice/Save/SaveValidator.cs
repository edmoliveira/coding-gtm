using FluentValidation;
using Gtm.Business.Domain.Managers.ProductPrice.Models;

namespace Gtm.Business.Domain.Managers.ProductPrice.Save
{
    /// <summary>
    /// Object validator
    /// </summary>
    internal sealed class SaveValidator : AbstractValidator<SaveRequest>
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the Gtm.Business.Domain.Managers.ProductPrice.Save.SaveValidator class.
        /// </summary>
        public SaveValidator()
        {
            RuleFor(x => x.ProductId)
                .GreaterThan(0);

            RuleFor(x => x.Rates)
                .Must(x => x == null || x.Any());

            RuleForEach(x => x.Rates)
                .SetValidator(new RateValidator());
        }

        #endregion
    }

    /// <summary>
    /// Object validator
    /// </summary>
    sealed class RateValidator : AbstractValidator<RatePriceModel>
    {
        /// <summary>
        /// Initializes a new instance of the Gtm.Business.Domain.Managers.ProductPrice.Save.RateValidator class.
        /// </summary>
        public RateValidator()
        {
            RuleFor(x => x.CountryId)
                .GreaterThan(0);

            RuleFor(x => x.Price)
                .GreaterThan(0);

            RuleFor(x => x.Rate)
                .GreaterThan(0);
        }
    }
}
