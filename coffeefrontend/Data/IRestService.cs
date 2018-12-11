using System.Collections.Generic;
using System.Threading.Tasks;

namespace coffeefrontend
{
    public interface IRestService
    {
        Task<(string, string)> Register(string username, string password, string role);
        Task<(string, string)> Login(string username, string password);
        Task<(string, List<OrderResp>)> GetAllOrders(string token);
        Task<(string, string)> PostOrder(string token, Order order);
    }
}
