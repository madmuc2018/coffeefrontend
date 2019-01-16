using System.Windows.Input;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Threading.Tasks;
using System;
using Acr.UserDialogs;

namespace coffeefrontend
{
    public class GetHistoryPageViewModel : BaseViewModel
    {
        public List<Order> OrderHistory { get; protected set; }

        public async Task<int> init(string guid)
        {
            using (UserDialogs.Instance.Loading("Loading History...", null, null, true, MaskType.Black))
            {
                (string error, List<Order> result) = await App.Manager.GetHistoryTask(Application.Current.Properties["coffee_token"].ToString(), guid);
                if (error != null)
                {
                    await Application.Current.MainPage.DisplayAlert("Alert", "Cannot get history", "Close");
                }
                else
                {
                    this.OrderHistory = result;
                }
            }
            return 99;
        }
    }
}