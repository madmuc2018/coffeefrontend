using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;
using Acr.UserDialogs;

namespace coffeefrontend
{
    public class GrantAccessPageViewModel : BaseViewModel
    {
        public string OrderID { get; }
        private string guid;
        private string username;
        public ICommand SubmitCommand { protected set; get; }

        public GrantAccessPageViewModel(string orderID, string guid)
        {
            this.OrderID = orderID;
            this.guid = guid;
            this.username = "";

            SubmitCommand = new Command(async () =>
            {
                using (UserDialogs.Instance.Loading("Granting Access...", null, null, true, MaskType.Black))
                {
                    (string error, string result) = await App.Manager.GrantAccessTask(Application.Current.Properties["coffee_token"].ToString(), guid, new List<string>(new string[] { username }));
                    if (error != null)
                        await Application.Current.MainPage.DisplayAlert("Alert", "Cannot grant access", "Close");
                    else
                        await Application.Current.MainPage.DisplayAlert("Result", $"Access granted to {username}", "Close");
                }
            });

        }

        public string Username
        {
            get => username;
            set
            {
                if (!username.Equals(value))
                {
                    username = value;
                    OnPropertyChanged();
                }
            }
        }
    }
}
