using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace coffeefrontend
{
    public partial class HomePage : ContentPage
    {
        class Order {
            public string id { get; }
            public string type { get; }
            public int quantity { get; }
            public string from { get; }
            public string status { get; }
            public Order(string id, string type, int quantity, string from, string status)
            {
                this.id = id;
                this.type = type;
                this.quantity = quantity;
                this.from = from;
                this.status = status;
            }
        }
        public HomePage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            listView.ItemsSource = new List<Order>(new Order[] { 
                new Order("1","mocha",100,"tom","placed"),
                new Order("2","mocha",100,"tom","placed"),
                new Order("3","mocha",100,"tom","placed"),
                new Order("4","mocha",100,"tom","placed"),
                new Order("5","mocha",100,"tom","placed")
                });
        }

        void OnAddItemClicked(object sender, EventArgs e)
        {
        }

        void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
        }
    }
}
