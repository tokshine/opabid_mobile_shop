using System.Threading.Tasks;
using Shivonet.MobileShop.Core.Models;

namespace Shivonet.MobileShop.Core.Contracts.Services.Data
{
    public interface IContactDataService
    {
        Task<ContactInfo> AddContactInfo(ContactInfo contactInfo);
    }
}
