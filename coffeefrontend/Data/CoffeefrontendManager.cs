﻿using System.Collections.Generic;
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

        public Task<(string, string)> AddOrderTask(string token, Order order)
        {
            return restService.AddOrder(token, order);
        }

        public Task<(string, List<OrderResp>)> GetOrdersTask(string token)
        {
            return restService.GetOrders(token);
        }

        public Task<(string, string)> UpdateOrderTask(string token, string guid, Order order)
        {
            return restService.UpdateOrder(token, guid, order);
        }

        public Task<(string, GrantAccessResp)> GrantAccessTask(string token, string guid, List<string> grantedUsers)
        {
            return restService.GrantAccess(token, guid, grantedUsers);
        }

        public Task<(string, string)> RevokeAccessTask(string token, string guid, string userToBeRevoked)
        {
            return restService.RevokeAccess(token, guid, userToBeRevoked);
        }

        public Task<(string, List<Order>)> GetHistoryTask(string token, string guid)
        {
            return restService.GetHistory(token, guid);
        }

        public Task<(string, OrderResp)> GetLastestOrderTask(string token, string guid)
        {
            return restService.getLatestOrder(token, guid);
        }

        public Task<(string, AccessResp)> GetAccessInfoTask(string token, string guid)
        {
            return restService.GetAccessInfo(token, guid);
        }
    }
}
