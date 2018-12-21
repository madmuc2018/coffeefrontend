using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;

namespace coffeefrontend
{
    public partial class GetHistoryPage : ContentPage
    {
        public GetHistoryPage(GetHistoryPageViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}