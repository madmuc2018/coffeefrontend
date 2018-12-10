using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace coffeefrontend.Views
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            Console.WriteLine("asdasdasd");
        }

        private (bool, string, string) getUsernameAndPassword() 
        {
            string username = usernameEntry.Text;
            string password = passwordEntry.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                DisplayAlert("Alert", "Please input username and password", "Close");
                return (false, "", "");
            }
            return (true,username,password);
        }

        public void OnRegisterButtonClicked(object sender, EventArgs e)
        {
           (bool valid, string username, string password) = getUsernameAndPassword();

           if(valid)
            {
                Application.Current.MainPage = new MainPage();
            }
        }

        public void OnLoginButtonClicked(object sender, EventArgs e)
        {
            (bool valid, string username, string password) = getUsernameAndPassword();

            if (valid)
            {
                Application.Current.MainPage = new MainPage();
            }
        }
    }
}
