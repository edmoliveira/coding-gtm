using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gtm.Business.Domain.Managers.User.SignIn
{
    /// <summary>
    /// User Login Manager
    /// </summary>
    public interface ISignInManager
    {
        #region Methods

        /// <summary>
        /// Handles the sign in and asynchronously using Task.
        /// </summary>
        /// <param name="request">Request data.</param>
        /// <returns>
        /// Task: Represents an asynchronous operation. 
        /// Response data.
        /// </returns>
        Task<SignInResponse> HandleAsync(SignInRequest request);

        #endregion
    }
}
