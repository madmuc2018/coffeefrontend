using System.Collections.Generic;
using System.Windows.Input;

namespace coffeefrontend
{
    public class MenuPageViewModel : BaseViewModel
    {
        static readonly List<MenuPageItem> coffeePages = new List<MenuPageItem>()
        {
            new MenuPageItem("Home", typeof(HomePage)),
            new MenuPageItem("Include", typeof(NewOrderPage)),
            new MenuPageItem("QR Scanner", typeof(QRCodeScanPage)),
            new MenuPageItem("Logout", null)
        };

        public ICommand SelectPageCommand { protected set; get; }

        public MenuPageViewModel(ICommand SelectPageCommand)
        {
            this.SelectPageCommand = SelectPageCommand;
        }

        public List<MenuPageItem> CoffeePages
        {
            get => coffeePages;
        }
    }
}