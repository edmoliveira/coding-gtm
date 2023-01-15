namespace Gtm.Business.Domain.Managers.Product.ReadAll
{
    /// <summary>
    /// Product Manager
    /// </summary>
    public interface IReadAllManager
    {
        #region Methods

        /// <summary>
        /// Handles the ReadAll and asynchronously using Task.
        /// </summary>
        /// <returns>
        /// Task: Represents an asynchronous operation. 
        /// Response data.
        /// </returns>
        Task<ReadAllResponse> HandleAsync();

        #endregion
    }
}
