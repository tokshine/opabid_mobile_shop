using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Shivonet.MobileShop.Core.Contracts.Services.Data;
using Shivonet.MobileShop.Core.Contracts.Services.General;
using Shivonet.MobileShop.Core.ViewModels.Base;
using Xamarin.Forms;

namespace Shivonet.MobileShop.Core.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly ISettingsService _settingsService;

        private string _userName;
        private string _password;

        public LoginViewModel(IConnectionService connectionService, ISettingsService settingsService,
            INavigationService navigationService, 
            IAuthenticationService authenticationService, 
            IDialogService dialogService) 
            : base(connectionService, navigationService, dialogService)
        {
            _authenticationService = authenticationService;
            _settingsService = settingsService;
        }

        public ICommand LoginCommand => new Command(OnLogin);
        public ICommand RegisterCommand => new Command(OnRegister);

        [Required(ErrorMessage = "Username cannot be empty!")]
        [EmailAddress]
        public string UserName
        {
            get => _userName;
            set
            {
                // _userName = value;
                // OnPropertyChanged();
                ValidateProperty(value);
                SetProperty(ref _userName, value);
            }
        }

        bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (Object.Equals(storage, value))
                return false;

            storage = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged();
            }
        }


        protected override void ValidateProperty(object value, [CallerMemberName] string propertyName = null)
        {
            base.ValidateProperty(value, propertyName);

            OnPropertyChanged("IsSubmitEnabled");
        }

        public bool IsSubmitEnabled
        {
            get
            {
                return !HasErrors;
            }
        }


        private async void OnLogin()
        {
            IsBusy = true;
            if (_connectionService.IsConnected)
            {
                var authenticationResponse  = await _authenticationService.Authenticate(UserName, Password);

                if (authenticationResponse.IsAuthenticated)
                {
                    // we store the Id to know if the user is already logged in to the application
                    _settingsService.UserIdSetting = authenticationResponse.User.Id;
                    _settingsService.UserNameSetting = authenticationResponse.User.FirstName;

                    IsBusy = false;
                    await _navigationService.NavigateToAsync<MainViewModel>();
                }
            }
            else
            {
                await _dialogService.ShowDialog(
                    "This username/password combination isn't known", 
                    "Error logging you in",
                    "OK");
            }
        }

        private void OnRegister()
        {
            _navigationService.NavigateToAsync<RegistrationViewModel>();
        }
    }
}
