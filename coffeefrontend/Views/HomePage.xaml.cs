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
            (string error, List<OrderResp> orders) = (await App.Manager.GetAllOrdersTask(Application.Current.Properties["coffee_token"].ToString()));
           
            if (error != null)
            { 
                await DisplayAlert("Alert", error, "Close");
                return;
            }

            var items = new Order[orders.Count];
            for(int i = 0;  i < orders.Count; i++)
            {
                items[i] = orders[i].data;
            }

            listView.ItemsSource = items;
        }

        void OnAddItemClicked(object sender, EventArgs e)
        {
        }

        void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
        }
    }
}
