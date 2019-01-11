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
            Console.WriteLine("create qrcode assist");
            doQRCodeScanning();
        }

        public void doQRCodeScanning()
        {
            QRCodeScanPage tempThis = this;
            this.OnScanResult += (result) => {
                this.IsScanning = false;


                Device.BeginInvokeOnMainThread(() =>
                {
                    Navigation.PopAsync();
                    DisplayAlert("Scanned Barcode", result.Text, "OK");
                });
            };
            //await Navigation.PushAsync(scanPage);
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
