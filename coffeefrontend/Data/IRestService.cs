﻿using System.Collections.Generic;
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
        Task<(string, GrantAccessResp)> GrantAccess(string token, string guid, List<string> grantedUsers);
        Task<(string, List<Order>)> GetHistory(string token, string guid);
        Task<(string, OrderResp)> getLatestOrder(string token, string guid);
        Task<(string, string)> RevokeAccess(string token, string guid, string userToBeRevoked);
        Task<(string, AccessResp)> GetAccessInfo(string token, string guid);
    }
}
