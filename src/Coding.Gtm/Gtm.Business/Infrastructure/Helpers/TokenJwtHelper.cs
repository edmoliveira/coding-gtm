using Gtm.Business.Infrastructure.Helpers.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Gtm.Business.Infrastructure.Helpers
{
    /// <summary>
    /// Manages Json Web Token.
    /// </summary>
    internal class TokenJwtHelper : ITokenJwtHelper
    {
        #region Fields 

        /// <summary>
        /// Application Configuration
        /// </summary>
        private readonly IAppConfig _appConfig;

        #endregion

        #region Consctructors

        /// <summary>
        /// Initializes a new instance of the Gtm.Business.Infrastructure.Helpers.TokenJwtHelper class.
        /// </summary>
        /// <param name="appConfig">Application Configuration</param>
        /// <param name="handler">A Microsoft.IdentityModel.Tokens.SecurityTokenHandler designed for creating and validating Json Web Tokens</param>
        public TokenJwtHelper(IAppConfig appConfig)
        {
            _appConfig = appConfig;
        }

        #endregion

        #region Methods 

        #region public

        /// <summary>
        /// Creates a Json Web Token (JWT).
        /// </summary>
        /// <param name="claimsIdentity">Represents a claims-based identity.</param>
        /// <param name="expires">Token expiration.</param>
        /// <returns>Token in Compact Serialization Format.</returns>
        public string CreateToken(ClaimsIdentity claimsIdentity, DateTime expires)
        {
            JwtSecurityTokenHandler tokenHandler = new();

            SecurityTokenDescriptor tokenDescriptor = new()
            {
                Subject = claimsIdentity,
                Expires = expires,
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(ConvertStringToBytes(_appConfig.AuthTokenSecrect)),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };

            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        #endregion

        #region private

        /// <summary>
        /// Convert a string to a byte array containing the results of encoding the specified set of characters
        /// </summary>
        /// <param name="value">The string containing the characters to encode.</param>
        /// <returns>A byte array containing the results of encoding the specified set of characters</returns>
        private static byte[] ConvertStringToBytes(string value)
        {
            return Encoding.ASCII.GetBytes(value);
        }

        #endregion

        #endregion
    }
}
