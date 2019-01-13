﻿using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;

namespace coffeefrontend
{
    public class HomePageViewModel : BaseViewModel
    {
        public ICommand ToUpdate { protected set; get; }
        public ICommand ToGrantAccess { protected set; get; }
        public ICommand GenerateQRCode { protected set; get; }

        public ICommand QRScanner { protected set; get; }
        public List<OrderResp> OrderResps { get; }

        public HomePageViewModel(List<OrderResp> orders, Command toUpdate, Command toGrantAccess, Command generateQRCode)
        {
            this.OrderResps = orders;
            ToUpdate = toUpdate;
            ToGrantAccess = toGrantAccess;
            GenerateQRCode = generateQRCode;
        }
    }
}
