using System;
using System.Threading.Tasks;
using Shivonet.MobileShop.Core.Constants;
using Shivonet.MobileShop.Core.Contracts.Repository;
using Shivonet.MobileShop.Core.Contracts.Services.General;
using Shivonet.MobileShop.Core.Models;
using IAuthenticationService = Shivonet.MobileShop.Core.Contracts.Services.Data.IAuthenticationService;

namespace Shivonet.MobileShop.Core.Services.Data
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IGenericRepository _genericRepository;
        private readonly ISettingsService _settingsService;
        public AuthenticationService(IGenericRepository genericRepository, ISettingsService settingsService)
        {
            _settingsService = settingsService;
            _genericRepository = genericRepository;

        }

        public async Task<AuthenticationResponse> Register(string firstName, string lastName, string email, string userName, string password)
        {
            UriBuilder builder = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = ApiConstants.RegisterEndpoint
            };

            AuthenticationRequest authenticationRequest = new AuthenticationRequest()
            {
                Email = email,
                FirstName = firstName,
                LastName = lastName,
                UserName = userName,
                Password = password
            };

            return  await _genericRepository.PostAsync<AuthenticationRequest, AuthenticationResponse>(builder.ToString(),authenticationRequest);
        }

        public bool IsUserAuthenticated()
        {
            return !string.IsNullOrEmpty(_settingsService.UserIdSetting);
        }

        public async Task<AuthenticationResponse> Authenticate(string userName, string password)
        {
            UriBuilder builder = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = ApiConstants.AuthenticateEndpoint
            };

            AuthenticationRequest authenticationRequest = new AuthenticationRequest()
            {
                UserName = userName,
                Password = password
            };

            return await _genericRepository.PostAsync<AuthenticationRequest, AuthenticationResponse>(builder.ToString(), authenticationRequest);
        }
    }
}
