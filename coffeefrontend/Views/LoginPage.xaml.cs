using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using Xamarin.Forms;
using Newtonsoft.Json;
using System.Text;

namespace coffeefrontend
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        static string url = "https://513f497d.ngrok.io/data";

        private (bool, string, string, string) getUsernameAndPassword() 
        {
            string username = usernameEntry.Text;
            string password = passwordEntry.Text;
            string role = roleEntry.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(role))
            {
                DisplayAlert("Alert", "Please input username and password", "Close");
                return (false, "", "", "");
            }
            return (true,username,password,role);
        }

        public async void OnRegisterButtonClicked(object sender, EventArgs e)
        {
           (bool valid, string username, string password, string role) = getUsernameAndPassword();

            if (!valid)
            {
                return;
            }
            HttpClient client = new HttpClient();
            client.MaxResponseContentBufferSize = 256000;
            var uri = new Uri($"{url}/register");
            HttpResponseMessage response = null;
            try
            {
                var content = new StringContent(
                    $"{{\"username\": \"{username}\", \"password\": \"{password}\", \"role\": \"{role}\"}}",
                    Encoding.UTF8,
                    "application/json");
                response = await client.PostAsync(uri, content);
                if (!response.IsSuccessStatusCode)
                {
                    await DisplayAlert("Alert", "Cannot register", "Close");
                    Debug.WriteLine(@"             ERROR {0}", response);
                    return;
                }
                await DisplayAlert("Register", "Done, you can now login", "Close");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"             ERROR {0}", ex.Message);
            }
        }

        public async void OnLoginButtonClicked(object sender, EventArgs e)
        {
            (bool valid, string username, string password, string role) = getUsernameAndPassword();

            if (!valid)
            {
                return; 
            }

            HttpClient client = new HttpClient();
            client.MaxResponseContentBufferSize = 256000;
            var uri = new Uri($"{url}/login");
            HttpResponseMessage response = null;
            try
            {
                var content = new StringContent(
                    $"{{\"username\": \"{username}\", \"password\": \"{password}\"}}",
                    Encoding.UTF8,
                    "application/json");
                response = await client.PostAsync(uri, content);

                if (!response.IsSuccessStatusCode)
                {
                    await DisplayAlert("Alert", "Cannot Login", "Close");
                    Debug.WriteLine(@"             ERROR {0}", response);
                    return;
                }

                var loginResp = JsonConvert
                    .DeserializeObject<Dictionary<string, string>>(
                    (
                        await response.Content.ReadAsStringAsync()
                    )
                    .ToString());

                string token;

                if (!loginResp.TryGetValue("token", out token))
                {
                    await DisplayAlert("Alert", "Cannot parse login data", "Close");
                    return;
                }

                //Can use neither Xamarin Auth or Xamarin Essentials Secure storage here because requires Dev ID
                Application.Current.Properties["coffee_token"] = token;
                await Application.Current.SavePropertiesAsync();
                Application.Current.MainPage = new RootPage();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"             ERROR {0}", ex.Message);
            }
        }
    }
}
