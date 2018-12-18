using System.Collections.Generic;
using System.Threading.Tasks;

namespace coffeefrontend
{
    public interface IRestService
    {
        Task<(string, string)> Register(string username, string password, string role);
        Task<(string, string)> Login(string username, string password);
        Task<(string, List<OrderResp>)> GetOrders(string token);
        Task<(string, string)> AddOrder(string token, Order order);
        Task<(string, string)> UpdateOrder(string token, string guid, Order order);
        Task<(string, string)> GrantAccess(string token, string guid, List<string> grantedUsers);
    }
}
