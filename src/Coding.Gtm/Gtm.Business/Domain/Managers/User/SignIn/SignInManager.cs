using Gtm.Business.Infrastructure.Helpers;
using Gtm.Business.Infrastructure.Helpers.Exceptions;
using Gtm.Business.Infrastructure.Helpers.Extensions;
using Gtm.Business.Infrastructure.Helpers.Interfaces;
using Gtm.Business.Infrastructure.Repositories.User;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Security.Claims;

namespace Gtm.Business.Domain.Managers.User.SignIn
{
    /// <summary>
    /// User Login Manager
    /// </summary>
    internal sealed class SignInManager : ISignInManager
    {
        #region Fields

        /// <summary>
        /// Manages Json Web Token.
        /// </summary>
        private readonly ITokenJwtHelper _tokenJwt;
        /// <summary>
        /// User Repository
        /// </summary>
        private readonly IUserRepository _repository;
        /// <summary>
        /// Application Configuration
        /// </summary>
        private readonly IAppConfig _appConfig;
        /// <summary>
        /// Log
        /// </summary>
        private readonly ILogger<SignInManager> _logger;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the Gtm.Business.Domain.Managers.User.SignIn.SignInApplication class.
        /// </summary>
        /// <param name="_tokenJwt">Manages Json Web Token.</param>
        /// <param name="repository">User Repository</param>
        /// <param name="appConfig">Application Configuration</param>
        /// <param name="logger">Log</param>
        public SignInManager(
            ITokenJwtHelper tokenJwt,
            IUserRepository repository,
            IAppConfig appConfig,
            ILogger<SignInManager> logger
        )
        {
            _tokenJwt = tokenJwt;
            _repository = repository;
            _appConfig = appConfig;
            _logger = logger;
        }

        #endregion

        #region Methods

        #region public

        /// <summary>
        /// Handles the sign in and asynchronously using Task.
        /// </summary>
        /// <param name="request">Request data.</param>
        /// <returns>
        /// Task: Represents an asynchronous operation. 
        /// Response data.
        /// </returns>
        public async Task<SignInResponse> HandleAsync(SignInRequest request)
        {
            string methodName = nameof(HandleAsync);

            _logger.LogBeginInformation(methodName);

            var users = await _repository.ReadAsync().ConfigureAwait(false);

            var user = users.FirstOrDefault(u => u.Login == request.Login &&
                        Cryptography.Decrypt(u.Password, _appConfig.AuthTokenSecrect) == request.Password);

            if (user == null)
            {
                throw new RequestException(HttpStatusCode.Unauthorized, "Not authorized!");
            }

            DateTime expires = DateTime.UtcNow.AddSeconds(_appConfig.AuthTokenExpireSeconds);

            ClaimsIdentity claimsIdentity = new(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Login)
            });

            string token = _tokenJwt.CreateToken(claimsIdentity, expires);

            _logger.LogEndInformation(methodName);

            return new SignInResponse
            {
                Name = user.Name,
                ExpireSeconds = _appConfig.AuthTokenExpireSeconds,
                Token = token
            };
        }

        #endregion

        #endregion
    }
}
