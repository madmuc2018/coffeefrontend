using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace coffeefrontend
{
    public class RestService : IRestService
    {
        HttpClient client;

        static string url = "http://0.0.0.0:9000/data";

        public RestService()
        {
            client = new HttpClient();
            client.MaxResponseContentBufferSize = 256000;
        }

        public async Task<(string, string)> Login(string username, string password)
        {
            var uri = new Uri($"{url}/login");
            HttpResponseMessage response = null;
            string token;
            try
            {
                var content = new StringContent($"{{\"username\": \"{username}\", \"password\": \"{password}\"}}",Encoding.UTF8,"application/json");
                response = await client.PostAsync(uri, content);
                if (!response.IsSuccessStatusCode)
                {
                    Debug.WriteLine(@"             ERROR {0}", response);
                    return ("Cannot Login", null);
                }

                var loginResp = JsonConvert.DeserializeObject<Dictionary<string, string>>((await response.Content.ReadAsStringAsync()).ToString());

                if (!loginResp.TryGetValue("token", out token))
                {
                    return ("Cannot parse login data", null);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"             ERROR {0}", ex.Message);
                return ("Unexpected error login", null);
            }

            return (null, token);
        }

        public async Task<(string, string)> Register(string username, string password, string role)
        {
            var uri = new Uri($"{url}/register");
            HttpResponseMessage response = null;
            string result;
            try
            {
                var content = new StringContent($"{{\"username\": \"{username}\", \"password\": \"{password}\", \"role\": \"{role}\"}}",Encoding.UTF8,"application/json");
                response = await client.PostAsync(uri, content);
                if (!response.IsSuccessStatusCode)
                {
                    Debug.WriteLine(@"             ERROR {0}", response);
                    return ("Cannot register", null);
                }
                result = "Done, you can now login";
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"             ERROR {0}", ex.Message);
                return ("Unexpected error register", null);
            }

            return (null, result);
        }

        public async Task<(string, List<OrderResp>)> GetAllOrders(string token)
        {
            var uri = new Uri($"{url}/");
            HttpResponseMessage response = null;
            List <OrderResp> result;
            try
            {
                var req = new HttpRequestMessage(HttpMethod.Get, uri);
                req.Headers.Add("Authorization", $"Bearer {token}");
                response = await client.SendAsync(req);
                if (!response.IsSuccessStatusCode)
                {
                    Debug.WriteLine(@"             ERROR {0}", response);
                    return ("Cannot get all data", null);
                }
                var resultString = (await response.Content.ReadAsStringAsync()).ToString();
                result = JsonConvert.DeserializeObject<List<OrderResp>>(resultString);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"             ERROR {0}", ex.Message);
                return ("Unexpected error get all data", null);
            }

            return (null, result);
        }

        public async Task<(string, string)> PostOrder(string token, Order order)
        {
            var uri = new Uri($"{url}/");
            HttpResponseMessage response = null;
            string result;
            try
            {
                var req = new HttpRequestMessage(HttpMethod.Post, uri);
                req.Headers.Add("Authorization", $"Bearer {token}");
                req.Content = new StringContent(JsonConvert.SerializeObject(order), Encoding.UTF8, "application/json");
                response = await client.SendAsync(req);
                if (!response.IsSuccessStatusCode)
                {
                    Debug.WriteLine(@"             ERROR {0}", response);
                    return ("Cannot include order", null);
                }
                result = (await response.Content.ReadAsStringAsync()).ToString();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"             ERROR {0}", ex.Message);
                return ("Unexpected error include order", null);
            }

            return (null, result);
        }
    }
}
