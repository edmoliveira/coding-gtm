using System.Security.Claims;

namespace Gtm.Business.Infrastructure.Helpers.Interfaces
{
    /// <summary>
    /// Manages Json Web Token.
    /// </summary>
    internal interface ITokenJwtHelper
    {
        #region public

        /// <summary>
        /// Creates a Json Web Token (JWT).
        /// </summary>
        /// <param name="claimsIdentity">Represents a claims-based identity.</param>
        /// <param name="expires">Token expiration.</param>
        /// <returns>Token in Compact Serialization Format.</returns>
        string CreateToken(ClaimsIdentity claimsIdentity, DateTime expires);

        #endregion
    }
}
