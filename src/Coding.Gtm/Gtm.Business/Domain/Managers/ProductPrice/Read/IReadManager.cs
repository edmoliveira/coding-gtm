namespace Gtm.Business.Domain.Managers.ProductPrice.Read
{
    /// <summary>
    /// ProductPrice Manager
    /// </summary>
    public interface IReadManager
    {
        #region Methods

        /// <summary>
        /// Handles the ReadAll and asynchronously using Task.
        /// </summary>
        /// <param name="request">Request data</param>
        /// <returns>
        /// Task: Represents an asynchronous operation. 
        /// Response data.
        /// </returns>
        Task<ReadResponse> HandleAsync(ReadRequest request);

        #endregion
    }
}
