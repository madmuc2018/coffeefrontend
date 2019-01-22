using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;
using Acr.UserDialogs;

namespace coffeefrontend
{
    public class RestService : IRestService
    {
        HttpClient client;

        static string url = "https://13027d48.ngrok.io/v1";

        public RestService()
        {
            client = new HttpClient();
            client.MaxResponseContentBufferSize = 256000;
        }

        private async Task<(string, SuccessType)> doSendRequest<ErrorType, SuccessType>(string endpoint, string requestName, HttpMethod httpMethod, string token = null, object requestBody = null, [CallerMemberName] string caller = "Unknown method") where ErrorType : ToStringBase
        {
            var uri = new Uri($"{url}{endpoint}");
            HttpResponseMessage response = null;
            using (UserDialogs.Instance.Loading(requestName, null, null, true, MaskType.Black))
            {
                try
                {
                    var req = new HttpRequestMessage(httpMethod, uri);
                    if (token != null)
                        req.Headers.Add("Authorization", $"Bearer {token}");
                    if (requestBody != null)
                        req.Content = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");
                    response = await client.SendAsync(req);
                    string resultString = (await response.Content.ReadAsStringAsync()).ToString();
                    if (!response.IsSuccessStatusCode)
                    {
                        string errorString = JsonConvert.DeserializeObject<ErrorType>(resultString).ToString().Trim();
                        Debug.WriteLine(@"             ERROR {0} {1} {2}", response, resultString, errorString);
                        return (errorString.Length > 0 ? errorString : $"Cannot {caller}", default(SuccessType));
                    }
                    SuccessType result = JsonConvert.DeserializeObject<SuccessType>(typeof(SuccessType).Equals(typeof(IgnoreResponseContent)) ? $"{{\"message\": \"{caller} succeeded\"}}" : resultString);
                    return (null, result);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(@"             ERROR {0}", ex.Message);
                    return ($"Unexpected error from {caller}", default(SuccessType));
                }
            }
        }

        public async Task<(string, string)> Register(string u, string p, string r)
        {
            (string error, IgnoreResponseContent resp) = await doSendRequest<ErrorResp, IgnoreResponseContent>("/user/register", "Registering", HttpMethod.Post, null, new RegisterBody { username = u, password = p, role = r });
            return (error, error ?? resp.message);
        }

        public async Task<(string, string)> Login(string u, string p)
        {
            (string error, LoginResponse resp) = await doSendRequest<ErrorResp, LoginResponse>("/user/login", "Logging in", HttpMethod.Post, null, new LoginBody { username = u, password = p });
            return (error, error ?? resp.token);
        }

        public async Task<(string, List<OrderResp>)> GetOrders(string token)
        {
            return await doSendRequest<ErrorResp, List<OrderResp>>("/fs", "Retrieving orders", HttpMethod.Get, token);
        }

        public async Task<(string, string)> AddOrder(string token, Order order)
        {
            (string error, IgnoreResponseContent resp) = await doSendRequest<ErrorResp, IgnoreResponseContent>("/fs", "Adding order", HttpMethod.Post, token, order);
            return (error, error ?? resp.message);
        }

        public async Task<(string, string)> UpdateOrder(string token, string guid, Order order)
        {
            (string error, IgnoreResponseContent resp) = await doSendRequest<ErrorResp, IgnoreResponseContent>($"/fs/{guid}", "Updating order", HttpMethod.Put, token, order);
            return (error, error ?? resp.message);
        }

        public async Task<(string, string)> GrantAccess(string token, string guid, List<string> gus)
        {
            (string error, IgnoreResponseContent resp) = await doSendRequest<ErrorResp, IgnoreResponseContent>($"/fs/{guid}/grant", "Granting access", HttpMethod.Put, token, new GrantAccessBody { grantedUsers = gus });
            return (error, error ?? resp.message);
        }

        public async Task<(string, List<Order>)> GetHistory(string token, string guid)
        {
            return await doSendRequest<ErrorResp, List<Order>>($"/fs/{guid}/trace", "Retrieving history", HttpMethod.Get, token);
        }

        public async Task<(string, OrderResp)> getLatestOrder(string token, string guid)
        {
            return await doSendRequest<ErrorResp, OrderResp>($"/fs/{guid}/latest", "Retrieving latest version of order", HttpMethod.Get, token);
        }
    }

    class LoginResponse
    {
        public string token { get; set; }
    }

    class IgnoreResponseContent
    {
        public string message { get; set; }
    }

    class GrantAccessBody
    {
        public List<string> grantedUsers { get; set; }
    }
}