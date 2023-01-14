using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gtm.Business.Domain.Managers.User.SignIn
{
    /// <summary>
    /// Request data
    /// </summary>
    public sealed class SignInRequest
    {
        #region Properties

        /// <summary>
        /// User login
        /// </summary>
        public string Login { get; set; }
        /// <summary>
        /// User password
        /// </summary>
        public string Password { get; set; }

        #endregion
    }
}
