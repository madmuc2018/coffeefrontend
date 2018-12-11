using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace coffeefrontend
{
    public class CoffeefrontendManager
    {
        IRestService restService;

        public CoffeefrontendManager(IRestService service)
        {
            restService = service;
        }

        public Task<(string, string)> LoginTask(string username, string password)
        {
            return restService.Login(username, password);
        }

        public Task<(string, string)> RegisterTask(string username, string password, string role)
        {
            return restService.Register(username, password, role);
        }

        public Task<(string, string)> PostOrderTask(string token, Order order)
        {
            return restService.PostOrder(token, order);
        }

        public Task<(string, List<OrderResp>)> GetAllOrdersTask(string token)
        {
            return restService.GetAllOrders(token);
        }
    }
}
