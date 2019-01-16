using Acr.UserDialogs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Xamarin.Forms;

namespace coffeefrontend
{
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            using (UserDialogs.Instance.Loading("Getting Order...", null, null, true, MaskType.Black))
            {
                (string error, List<OrderResp> orders) = (await App.Manager.GetOrdersTask(Application.Current.Properties["coffee_token"].ToString()));

                if (error != null)
                {
                    await DisplayAlert("Alert", error, "Close");
                    return;
                }

                BindingContext = new HomePageViewModel(orders, new Command(NavigateToUpdateOrderPage), new Command(NavigateToGrantAccessPage), new Command(GenerateQRCode), new Command(NavigateToGetHistoryPage));
            }
        }

        private async void NavigateToUpdateOrderPage(object selectedOject)
        {
            var selectedOrderResp = selectedOject as OrderResp;
            await Navigation.PushAsync(new UpdateOrderPage(new UpdatePageViewModel(selectedOrderResp.data, selectedOrderResp.guid)));
        }

        private async void NavigateToGrantAccessPage(object selectedOject)
        {
            var selectedOrderResp = selectedOject as OrderResp;
            await Navigation.PushAsync(new GrantAccessPage(new GrantAccessPageViewModel(selectedOrderResp.data.id, selectedOrderResp.guid)));
        }

        private async void GenerateQRCode(object selectedOject)
        {
            var selectedOrderResp = selectedOject as OrderResp;
            await Navigation.PushAsync(new OrderQRCodePage(selectedOrderResp));
        }

        public async void asyncOpenQRCodeScanner(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new QRCodeScanPage());
        }
        
        private async void NavigateToGetHistoryPage(object selectedOject)
        {
            var selectedOrderResp = selectedOject as OrderResp;
            var viewModel = new GetHistoryPageViewModel();
            await viewModel.init(selectedOrderResp.guid);
            await Navigation.PushAsync(new GetHistoryPage(viewModel));
        }
    }
}
