using SkiaSharp.Views.Forms;
using Xamarin.Forms;

namespace coffeefrontend
{
    public partial class OrderQRCodePage : ContentPage
    {
        OrderQRCodePageViewModel viewModel;

        public OrderQRCodePage(Order order)
        {
            InitializeComponent();
            BindingContext = this.viewModel = new OrderQRCodePageViewModel(order);
        }

        public void OnCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs args)
        {
            viewModel.PaintSurface(args);
        }
    }
}