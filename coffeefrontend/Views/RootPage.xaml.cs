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

                if (Device.RuntimePlatform == Device.UWP)
                {
                    MasterBehavior = MasterBehavior.Popover;
                }

                Master = new MenuPage(new Command(SelectPage));
            }
            catch (KeyNotFoundException)
            {
                Application.Current.MainPage = new LoginPage();
            }
        }

        private void SelectPage(object selectedOject)
        {
            if (selectedOject is MenuPageItem selectedPage)
            {
                Detail = new NavigationPage((Page)Activator.CreateInstance(selectedPage.TargetType));
                IsPresented = false;
            }
        }
    }
}
