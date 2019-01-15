using SkiaSharp.Views.Forms;
using Xamarin.Forms;

namespace coffeefrontend
{
    public partial class OrderQRCodePage : ContentPage
    {
        OrderQRCodePageViewModel viewModel;

        public OrderQRCodePage(OrderResp orderResp)
        {
            InitializeComponent();
            BindingContext = this.viewModel = new OrderQRCodePageViewModel(orderResp);
        }

        public void OnCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs args)
        {
            viewModel.PaintSurface(args);
        }
    }
}