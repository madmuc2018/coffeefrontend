using System;
using System.Collections.Generic;
using System.Diagnostics;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;
using ZXing.QrCode;
using ZXing.Common;

namespace coffeefrontend
{
    public partial class OrderDetailsPage : ContentPage
    {
        SKBitmap skbmp;

        public OrderDetailsPage(OrderResp orderResp)
        {
            InitializeComponent();
            BindingContext = orderResp;

            if (orderResp != null)
            {
                orderResp.data.id = "test";
                string tempStr = "www.google.com";


                QRCodeWriter qrwriter = new QRCodeWriter();
                BitMatrix bmqr = qrwriter.encode(tempStr, ZXing.BarcodeFormat.QR_CODE, 750, 750);


                /*
                                IBarcodeWriter writer = new BarcodeWriter
                                { Format = BarcodeFormat.QR_CODE };
                                var result = writer.Write("Hello");
                                var barcodeBitmap = new Bitmap(result);
                                pictureBox1.Image = barcodeBitmap;
                                */
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

                /*= new ZXingBarcodeImageView
            {
                BarcodeFormat = BarcodeFormat.QR_CODE,
                BarcodeOptions = new QrCodeEncodingOptions
                {
                    Height = 50,
                    Width = 50
                },
                BarcodeValue = tempStr,
            };
            Content = new StackLayout
            {
                Children = {
                    qrcode
                },
                BackgroundColor = Xamarin.Forms.Color.Black
            };
            */
            }

        }

        public void OnCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs args)
        {

            if (skbmp != null)
            {
                SKImageInfo info = args.Info;
                SKSurface surface = args.Surface;
                SKCanvas canvas = surface.Canvas;

                canvas.Clear();

                // uniform scaled qr code
                float scale = Math.Min((float)info.Width / skbmp.Width,
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
