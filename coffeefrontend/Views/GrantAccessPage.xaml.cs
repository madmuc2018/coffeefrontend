using Xamarin.Forms;

namespace coffeefrontend
{
    public partial class GrantAccessPage : ContentPage
    {
        public GrantAccessPage(GrantAccessPageViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}