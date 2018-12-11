using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace coffeefrontend
{
    public partial class NewOrderPage : ContentPage
    {
        public NewOrderPage()
        {
            InitializeComponent();
        }

        private bool AllFilled(string[] entries)
        {
            bool filled = false;
            foreach(String entry in entries)
            {
                filled = !string.IsNullOrEmpty(entry);   
            }
            return filled;
        }

        public async void OnPlaceOrderClicked(object sender, EventArgs e)
        {
            string id = idEntry.Text;
            string type = typeEntry.Text;
            string quantity = quantityEntry.Text;
            string from = fromEntry.Text;

            if (!AllFilled(new string[]{id,type,quantity,from}))
            {
                await DisplayAlert("Alert", "Please fill in all the fields", "Close");
                return;
            }

            (string error, string result) = await App.Manager.PostOrderTask(Application.Current.Properties["coffee_token"].ToString(), new Order(id, type, quantity, from, "Placed"));

            if (error != null)
            {
                await DisplayAlert("Alert", "Cannot include order", "Close");
                return;
            }

            await DisplayAlert("Result", "Order included", "Close");
        }
    }
}
