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
            (string error, List<OrderResp> orders) = (await App.Manager.GetOrdersTask(Application.Current.Properties["coffee_token"].ToString()));
           
            if (error != null)
            { 
                await DisplayAlert("Alert", error, "Close");
                return;
            }
            
            BindingContext = new HomePageViewModel(orders, new Command(NavigateToUpdateOrderPage), new Command(NavigateToGrantAccessPage));
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

        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
            {
                return;
            }

            await Navigation.PushAsync(new OrderDetailsPage(e.Item as OrderResp));

            /*
            await DisplayAlert("Item Tapped"+ ((CoffeeOrder)((ListView)sender).SelectedItem).process, "An item was tapped.", "OK");
            */
            //Deselect Item
            ((ListView)sender).SelectedItem = null;
        }
    }
}
