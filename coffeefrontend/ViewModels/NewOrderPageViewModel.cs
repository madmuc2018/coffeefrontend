using System.Windows.Input;
using Xamarin.Forms;

namespace coffeefrontend
{
    public class NewOrderPageViewModel : BaseViewModel
    {
        private Order preSubmit, order;
        public ICommand SubmitCommand { protected set; get; }

        public NewOrderPageViewModel()
        {
            order = new Order();
            SubmitCommand = new Command(async () =>
            {
                if (preSubmit != null && preSubmit.Equals(order))
                {
                    await Application.Current.MainPage.DisplayAlert("Alert", "Order has been submitted", "Close");
                    return;
                }
                order.status = "placed";

                (string error, string result) = await App.Manager.AddOrderTask(Application.Current.Properties["coffee_token"].ToString(), order);
                if (error != null)
                    await Application.Current.MainPage.DisplayAlert("Alert", "Cannot include order", "Close");
                else
                {
                    preSubmit = order;
                    await Application.Current.MainPage.DisplayAlert("Result", "Order included", "Close");
                }
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