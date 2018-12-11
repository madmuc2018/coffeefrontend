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

            (string error, string result) = await App.Manager.RegisterTask(username, password, role);

            if (error != null)
            {
                await DisplayAlert("Alert", error, "Close");
                return;
            }

            await DisplayAlert("Status", result, "Close");
        }

        public async void OnLoginButtonClicked(object sender, EventArgs e)
        {
            (bool valid, string username, string password, string role) = getUsernameAndPassword();

            if (!valid)
            {
                return;
            }

            (string error, string token) = await App.Manager.LoginTask(username, password);

            if (error != null)
            {
                await DisplayAlert("Alert", error, "Close");
                return;
            }

            //Can use neither Xamarin Auth or Xamarin Essentials Secure storage here because requires Dev ID
            Application.Current.Properties["coffee_token"] = token;
            await Application.Current.SavePropertiesAsync();
            Application.Current.MainPage = new RootPage();
        }
    }
}
