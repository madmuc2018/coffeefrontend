using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;

namespace coffeefrontend
{
    public class HomePageViewModel : BaseViewModel
    {
        public ICommand ToUpdate { protected set; get; }
        public ICommand ToGrantAccess { protected set; get; }
        public ICommand GenerateQRCode { protected set; get; }
        public ICommand GetHistory { get; }
        public ICommand FilterOrders { protected set; get; }
        private string filterer;
        private List<OrderResp> filteredOrderResps;

        public HomePageViewModel(List<OrderResp> orders, Command toUpdate, Command toGrantAccess, Command generateQRCode, Command getHistory)
        {
            ToUpdate = toUpdate;
            ToGrantAccess = toGrantAccess;
            GenerateQRCode = generateQRCode;
            GetHistory = getHistory;
            filterer = "";
            filteredOrderResps = new List<OrderResp>();
            FilteredOrderResps = ConstructFilteredOrdersList(orders, filterer);

            FilterOrders = new Command(() =>
            {
                FilteredOrderResps = ConstructFilteredOrdersList(orders, filterer);
            });
        }

        private List<OrderResp> ConstructFilteredOrdersList(List<OrderResp> o, string f)
        {
            List<OrderResp> temp = new List<OrderResp>();

            o.ForEach(x =>
            {
                if (f.Length == 0)
                {
                    temp.Add(x);
                }
                else
                {
                    if (x.data.id.ToLower().Contains(f.ToLower()))
                    {
                        temp.Add(x);
                    }
                }

            }
                );

            return temp;
        }

        public string Filterer
        {
            get => filterer;
            set
            {
                if (!filterer.Equals(value))
                {
                    filterer = value;
                    if (FilterOrders.CanExecute(null))
                        FilterOrders.Execute(null);
                    OnPropertyChanged();
                }
            }
        }

        public List<OrderResp> FilteredOrderResps
        {
            get => filteredOrderResps;
            set
            {
                if (!filteredOrderResps.Equals(value))
                {
                    filteredOrderResps = value;
                    OnPropertyChanged();
                }
            }
        }
    }
}
