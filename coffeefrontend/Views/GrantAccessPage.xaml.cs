using Xamarin.Forms;

namespace coffeefrontend
{
    public partial class GrantAccessPage : ContentPage
    {
        GrantAccessPageViewModel viewModel;
        public GrantAccessPage(GrantAccessPageViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = this.viewModel = viewModel;
        }
    }
}
