using System.Windows.Input;
using Xamarin.Forms;

namespace coffeefrontend
{
    public partial class MenuPage : ContentPage
    {
        public MenuPage(ICommand SelectPageCommand)
        {
            InitializeComponent();
            BindingContext = new MenuPageViewModel(SelectPageCommand);
        }
    }
}
