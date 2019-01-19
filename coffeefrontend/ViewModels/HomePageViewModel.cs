using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;

namespace coffeefrontend
{
    public class HomePageViewModel : BaseViewModel
    {
        public ICommand ToQRCodeScanner { protected set; get; }
        public ICommand ToUpdate { protected set; get; }
        public ICommand ToGrantAccess { protected set; get; }
        public ICommand GenerateQRCode { protected set; get; }
        public ICommand GetHistory { get; }
        public ICommand QRScanner { protected set; get; }
        public List<OrderResp> OrderResps { get; }

        public HomePageViewModel(List<OrderResp> orders, Command toQRCodeScanner, Command toUpdate, Command toGrantAccess, Command generateQRCode, Command getHistory)
        {
            this.OrderResps = orders;
            ToQRCodeScanner = toQRCodeScanner;
            ToUpdate = toUpdate;
            ToGrantAccess = toGrantAccess;
            GenerateQRCode = generateQRCode;
            GetHistory = getHistory;
        }
    }
}
