using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;

namespace coffeefrontend
{
    public class RestService : IRestService
    {
        HttpClient client;

        static string url = "  http://ddb13c67.ngrok.io/data";

        public RestService()
        {
            client = new HttpClient();
            client.MaxResponseContentBufferSize = 256000;
        }

        private async Task<(string, T)> doSendRequest<T>(string endpoint, HttpMethod httpMethod, string token = null, object requestBody = null, [CallerMemberName] string caller = "Unknown method")
        {
            var uri = new Uri($"{url}{endpoint}");
            HttpResponseMessage response = null;
            try
            {
                var req = new HttpRequestMessage(httpMethod, uri);
                if (token != null)
                    req.Headers.Add("Authorization", $"Bearer {token}");
                if (requestBody != null)
                    req.Content = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");
                response = await client.SendAsync(req);
                if (!response.IsSuccessStatusCode)
                {
                    Debug.WriteLine(@"             ERROR {0}", response);
                    return ($"Cannot {caller}", default(T));
                }
                string resultString = (await response.Content.ReadAsStringAsync()).ToString();
                T result = JsonConvert.DeserializeObject<T>(typeof(T).Equals(typeof(IgnoreResponseContent)) ? $"{{\"message\": \"{caller} succeeded\"}}" : resultString);
                return (null, result);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"             ERROR {0}", ex.Message);
                return ($"Unexpected error from {caller}", default(T));
            }
        }

        public async Task<(string, string)> Register(string u, string p, string r)
        {
            (string error, IgnoreResponseContent resp) = await doSendRequest<IgnoreResponseContent>("/register", HttpMethod.Post, null, new RegisterBody { username = u, password = p, role = r });
            return (error, error ?? resp.message);
        }

        public async Task<(string, string)> Login(string u, string p)
        {
            (string error, LoginResponse resp) = await doSendRequest<LoginResponse>("/login", HttpMethod.Post, null, new LoginBody { username = u, password = p });
            return (error, error ?? resp.token);
        }

        public async Task<(string, List<OrderResp>)> GetOrders(string token)
        {
            return await doSendRequest<List<OrderResp>>("/", HttpMethod.Get, token);
        }

        public async Task<(string, string)> AddOrder(string token, Order order)
        {
            (string error, IgnoreResponseContent resp) = await doSendRequest<IgnoreResponseContent>("/", HttpMethod.Post, token, order);
            return (error, error ?? resp.message);
        }

        public async Task<(string, string)> UpdateOrder(string token, string guid, Order order)
        {
            (string error, IgnoreResponseContent resp) = await doSendRequest<IgnoreResponseContent>($"/{guid}", HttpMethod.Put, token, order);
            return (error, error ?? resp.message);
        }

        public async Task<(string, string)> GrantAccess(string token, string guid, List<string> gus)
        {
            (string error, IgnoreResponseContent resp) = await doSendRequest<IgnoreResponseContent>($"/{guid}/grant", HttpMethod.Put, token, new GrantAccessBody { grantedUsers = gus });
            return (error, error ?? resp.message);
        }

        public async Task<(string, List<Order>)> GetHistory(string token, string guid)
        {
            return await doSendRequest<List<Order>>($"/{guid}/trace", HttpMethod.Get, token);
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