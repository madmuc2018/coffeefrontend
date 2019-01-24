using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace coffeefrontend
{
    public class LoginPageViewModel : BaseViewModel
    {
        private RegisterBody registerCreds, loginCreds;
        public ICommand RegisterCommand { protected set; get; }
        public ICommand LoginCommand { protected set; get; }

        public LoginPageViewModel()
        {
            registerCreds = new RegisterBody();
            loginCreds = new RegisterBody();

            RegisterCommand = new Command(async () =>
            {
                (string error, string result) = await App.Manager.RegisterTask(registerCreds.username, registerCreds.password, registerCreds.role);
                if (error != null)
                    await Application.Current.MainPage.DisplayAlert("Alert", error, "Close");
                else
                {
                    LoginCreds = registerCreds;
                    await Application.Current.MainPage.DisplayAlert("Result", "You can now login", "Close");
                }
            });

            LoginCommand = new Command(async () =>
            {

                (string error, string token) = await App.Manager.LoginTask(loginCreds.username, loginCreds.password);
                if (error != null)
                    await Application.Current.MainPage.DisplayAlert("Alert", error, "Close");
                else
                {
                    //Can use neither Xamarin Auth or Xamarin Essentials Secure storage here because requires Dev ID
                    Application.Current.Properties["coffee_token"] = token;
                    await Application.Current.SavePropertiesAsync();
                    Application.Current.MainPage = new RootPage();
                }
            });

        }

        public RegisterBody RegisterCreds
        {
            get => registerCreds;
            set
            {
                if (!registerCreds.Equals(value))
                {
                    registerCreds = value;
                    OnPropertyChanged();
                }
            }
        }

        public RegisterBody LoginCreds
        {
            get => loginCreds;
            set
            {
                if (!loginCreds.Equals(value))
                {
                    loginCreds = value;
                    OnPropertyChanged();
                }
            }
        }
    }
}