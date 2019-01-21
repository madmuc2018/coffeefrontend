using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace coffeefrontend
{
    public class GrantAccessPageViewModel : BaseViewModel
    {
        public string OrderID { get; }
        private string guid;
        private string username;
        private string user;
        public List<string> accessList { get; protected set; }
        public ICommand SubmitCommand { protected set; get; }
        public ICommand SubmitRevokeCommand { protected set; get; }

        public async Task<int> init(string guid)
        {
            (string error, AccessResp result) = await App.Manager.GetAccessInfoTask(Application.Current.Properties["coffee_token"].ToString(), guid);
            if (error != null)
                await Application.Current.MainPage.DisplayAlert("Alert", "Cannot get list of users with access", "Close");
            else
            {
                AccessList = result.grantedUsers;
            }
            return 99;
        }

        public GrantAccessPageViewModel(string orderID, string guid)
        {
            this.OrderID = orderID;
            this.guid = guid;
            this.username = "";
            this.user = "";
            this.accessList = new List<string>();

            SubmitCommand = new Command(async () =>
            {
                (string error, string result) = await App.Manager.GrantAccessTask(Application.Current.Properties["coffee_token"].ToString(), guid, new List<string>(new string[] { username }));
                if (error != null)
                    await Application.Current.MainPage.DisplayAlert("Alert", error, "Close");
                else
                    await Application.Current.MainPage.DisplayAlert("Result", $"Access granted to {username}", "Close");
            });

            SubmitRevokeCommand = new Command(async () =>
            {
                (string error, string result) = await App.Manager.RevokeAccessTask(Application.Current.Properties["coffee_token"].ToString(), guid, user);
                if (error != null)
                    await Application.Current.MainPage.DisplayAlert("Alert", "Cannot revoke access", "Close");
                else
                {
                    await init(guid);
                    await Application.Current.MainPage.DisplayAlert("Result", $"Access revoked to {user}", "Close");
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

        public string User
        {
            get => user;
            set
            {
                if (!user.Equals(value))
                {
                    user = value;
                    OnPropertyChanged();
                }
            }
        }

        public List<string> AccessList
        {
            get => accessList;
            set
            {
                if (!accessList.Equals(value))
                {
                    accessList = value;
                    OnPropertyChanged();
                }
            }
        }
    }
}