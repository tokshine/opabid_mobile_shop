using Shivonet.MobileShop.API.Models;

namespace Shivonet.MobileShop.API.ViewModel
{
    public class AuthenticationResponse
    {
        public bool IsAuthenticated { get; set; }
        public User User { get; set; }
    }
}
