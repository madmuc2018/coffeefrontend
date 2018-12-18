using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace coffeefrontend
{
    public class NewOrderPageViewModel : BaseViewModel
    {
        private Order order;
        public ICommand SubmitCommand { protected set; get; }

        public NewOrderPageViewModel()
        {
            SubmitCommand = new Command(async () =>
            {
                order.status = "placed";
                (string error, string result) = await App.Manager.AddOrderTask(Application.Current.Properties["coffee_token"].ToString(), order);
                if (error != null)
                    await Application.Current.MainPage.DisplayAlert("Alert", "Cannot include order", "Close");
                else
                    await Application.Current.MainPage.DisplayAlert("Result", "Order included", "Close");
            });
        }

        public Order NewOder
        {
            get => order;
            set
            {
                if (!order.Equals(value))
                {
                    order = value;
                    OnPropertyChanged();
                }
            }
        }
    }
}