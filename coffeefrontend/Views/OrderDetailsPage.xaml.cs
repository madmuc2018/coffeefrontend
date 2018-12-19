using SkiaSharp.Views.Forms;
using Xamarin.Forms;

namespace coffeefrontend
{
    public partial class OrderDetailsPage : ContentPage
    {
        OrderDetailsPageViewModel viewModel;

        public OrderDetailsPage(Order order)
        {
            InitializeComponent();
            BindingContext = this.viewModel = new OrderDetailsPageViewModel(order);
        }

        public void OnCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs args)
        {
            viewModel.PaintSurface(args);
        }
    }
}