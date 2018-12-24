using Acr.UserDialogs;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace coffeefrontend
{
    public class LoginPageViewModel : BaseViewModel
    {
        private RegisterBody credentials;
        public ICommand RegisterCommand { protected set; get; }
        public ICommand LoginCommand { protected set; get; }

        public LoginPageViewModel()
        {
            credentials = new RegisterBody();

            
            RegisterCommand = new Command(async () =>
            { 
                using (UserDialogs.Instance.Loading("Registing...", null, null, true, MaskType.Black))
                {
                    (string error, string result) = await App.Manager.RegisterTask(credentials.username, credentials.password, credentials.role);
                    if (error != null)
                        await Application.Current.MainPage.DisplayAlert("Alert", "Cannot register", "Close");
                    else
                        await Application.Current.MainPage.DisplayAlert("Result", "You can now login", "Close");
                }
            });
            


            LoginCommand = new Command(async () =>
            {
                Application.Current.Properties["coffee_token"] = "token";
                await Application.Current.SavePropertiesAsync();
                Application.Current.MainPage = new RootPage();
                /*
                using (UserDialogs.Instance.Loading("Login Now...", null, null, true, MaskType.Black))
                {

                     //just for teset

                    
                    (string error, string token) = await App.Manager.LoginTask(credentials.username, credentials.password);
                    if (error != null)
                        await Application.Current.MainPage.DisplayAlert("Alert", "Cannot login", "Close");
                    else
                    {
                        //Can use neither Xamarin Auth or Xamarin Essentials Secure storage here because requires Dev ID
                        Application.Current.Properties["coffee_token"] = token;
                        await Application.Current.SavePropertiesAsync();
                        Application.Current.MainPage = new RootPage();
                    }
                   
                } */
            });

        }

        public RegisterBody Credentials
        {
            get => credentials;
            set
            {
                if (!credentials.Equals(value))
                {
                    credentials = value;
                    OnPropertyChanged();
                }
            }
        }
    }
}