using Acr.UserDialogs;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using ZXing.Net.Mobile.Forms;

namespace coffeefrontend
{
    public class QRCodeScanPage : ZXingScannerPage
    {
        private static Grid qrcodeAssistOverlay = qrcodePlaceAssist();
        public QRCodeScanPage() : base(customOverlay: qrcodeAssistOverlay)
        {
            doQRCodeScanning();
        }

        public void doQRCodeScanning()
        {
            this.OnScanResult += (result) => {
                this.IsScanning = false;

                Console.WriteLine(result.Text);
                Device.BeginInvokeOnMainThread(async () =>
                {
                    using (UserDialogs.Instance.Loading("Processing Scan...", null, null, true, MaskType.Black))
                    {
                        //await Navigation.PopAsync();
                        (string error, OrderResp orderResp) = await App.Manager.GetLastestOrderTask(Application.Current.Properties["coffee_token"].ToString(), result.Text);
                        if (error != null)
                        {
                            await Application.Current.MainPage.DisplayAlert("Alert", "Cannot get order", "Close");
                        }
                        else
                        {
                            await Navigation.PushAsync(new UpdateOrderPage(new UpdatePageViewModel(orderResp.data, orderResp.guid)));
                        }

                    }
                });
            };
        }

        private static Grid qrcodePlaceAssist()
        {
            int ratio = 5;
            Grid tempGrid = new Grid();
            tempGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(2, GridUnitType.Star) });
            tempGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(ratio, GridUnitType.Star) });
            tempGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(2, GridUnitType.Star) });
            tempGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            tempGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(ratio, GridUnitType.Star) });
            tempGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });


            for(int i = 0; i < 3; i++)
            {
                for(int j = 0; j < 3; j++)
                {
                    if(i == 1 && j == 1)
                    {
                        continue;
                    }
                    BoxView boxView = new BoxView
                    {
                        Color = new Color(0, 0, 0, 0.4),
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        VerticalOptions = LayoutOptions.FillAndExpand,
                    };
                    tempGrid.Children.Add(boxView, j, i);
                }
            }

            return tempGrid;
        }
    }
}
