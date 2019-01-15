using Acr.UserDialogs;
using System.Windows.Input;
using Xamarin.Forms;

namespace coffeefrontend
{
    public class UpdatePageViewModel : BaseViewModel
    {
        private Order selectedOrder;
        private string guid;
        public ICommand SubmitCommand { protected set; get; }

        public UpdatePageViewModel(Order SelectedOrder, string guid)
        {
            this.selectedOrder = SelectedOrder;
            this.guid = guid;
            SubmitCommand = new Command(async () =>
            {
                using (UserDialogs.Instance.Loading("Updating Order...", null, null, true, MaskType.Black))
                {
                    (string error, string result) = await App.Manager.UpdateOrderTask(Application.Current.Properties["coffee_token"].ToString(), guid, selectedOrder);
                    if (error != null)
                        await Application.Current.MainPage.DisplayAlert("Alert", "Cannot update order", "Close");
                    else
                        await Application.Current.MainPage.DisplayAlert("Result", "Order updated", "Close");
                }
            });
        }

        public Order SelectedOrder
        {
            get => selectedOrder;
            set
            {
                if (!selectedOrder.Equals(value))
                {
                    selectedOrder = value;
                    OnPropertyChanged();
                }
            }
        }
    }
}
