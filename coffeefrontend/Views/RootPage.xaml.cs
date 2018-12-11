using System;
using System.Diagnostics;
using System.Collections.Generic;

using Xamarin.Forms;

namespace coffeefrontend
{
    public partial class RootPage : MasterDetailPage
    {
        public RootPage()
        {
            try
            {
                Debug.WriteLine(Application.Current.Properties["coffee_token"]);
                InitializeComponent();

                menuPage.listView.ItemSelected += OnItemSelected;

                if (Device.RuntimePlatform == Device.UWP)
                {
                    MasterBehavior = MasterBehavior.Popover;
                }
            }
            catch (KeyNotFoundException)
            {
                Application.Current.MainPage = new LoginPage();
            }
        }

        void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as MenuPageItem;
            if (item != null)
            {
                Detail = new NavigationPage((Page)Activator.CreateInstance(item.TargetType));
                menuPage.listView.SelectedItem = null;
                IsPresented = false;
            }
        }
    }
}
