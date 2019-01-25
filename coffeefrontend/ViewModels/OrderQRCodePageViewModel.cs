using SkiaSharp;
using ZXing.QrCode;
using ZXing.Common;
using SkiaSharp.Views.Forms;
using Plugin.Screenshot;
using System.Windows.Input;
using Xamarin.Forms;
using System;
using Acr.UserDialogs;

namespace coffeefrontend
{
    public class OrderQRCodePageViewModel : BaseViewModel
    {
        private SKBitmap skbmp;
        public string OrderID { get; }
        public ICommand ScreenshotCommand { protected set; get; }

        public OrderQRCodePageViewModel(OrderResp orderResp)
        {
            OrderID = orderResp.data.id;
            BitMatrix bmqr = new QRCodeWriter().encode(orderResp.guid, ZXing.BarcodeFormat.QR_CODE, 750, 750);
            skbmp = new SKBitmap(bmqr.Width, bmqr.Height);
            for (int i = 0; i < bmqr.Width; i++)
            {
                for (int j = 0; j < bmqr.Height; j++)
                {
                    if (bmqr[i, j])
                    {
                        skbmp.SetPixel(i, j, new SKColor(0, 0, 0));
                    }
                    else
                    {
                        skbmp.SetPixel(i, j, new SKColor(255, 255, 255));
                    }
                }
            }

            ScreenshotCommand = new Command(async () =>
            {
                using (UserDialogs.Instance.Loading("Taking screenshot", null, null, true, MaskType.Black))
                {
                    try
                    {
                        string path = await CrossScreenshot.Current.CaptureAndSaveAsync();
                        await Application.Current.MainPage.DisplayAlert("Result", "Screenshot saved", "Close");
                    }
                    catch (Exception ex)
                    {
                        await Application.Current.MainPage.DisplayAlert("Result", ex.Message, "Close");
                    }
                }
            });
        }

        public void PaintSurface(SKPaintSurfaceEventArgs args)
        {
            if (skbmp != null)
            {
                SKImageInfo info = args.Info;
                SKSurface surface = args.Surface;
                SKCanvas canvas = surface.Canvas;

                canvas.Clear();

                // uniform scaled qr code
                float scale = System.Math.Min((float)info.Width / skbmp.Width,
                                       (float)info.Height / skbmp.Height);
                float x = (info.Width - scale * skbmp.Width) / 2;
                float y = (info.Height - scale * skbmp.Height) / 2;
                SKRect destRect = new SKRect(x, y, x + scale * skbmp.Width,
                                                   y + scale * skbmp.Height);

                canvas.DrawBitmap(skbmp, destRect);
            }
        }
    }
}