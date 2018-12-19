using Xamarin.Forms;

namespace coffeefrontend
{
    public partial class UpdateOrderPage : ContentPage
    {
        public UpdateOrderPage(UpdatePageViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}