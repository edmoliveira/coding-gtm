namespace Gtm.Business.Domain.Managers.ProductPrice.Save
{
    /// <summary>
    /// ProductPrice Manager "Save"
    /// </summary>
    public interface ISaveManager
    {
        #region Methods

        /// <summary>
        /// Handles the Save and asynchronously using Task.
        /// </summary>
        /// <param name="request">Request data</param>
        /// <returns>Represents an asynchronous operation.</returns>
        Task HandleAsync(SaveRequest request);

        #endregion
    }
}
