﻿using System.Collections.Generic;
using Xamarin.Forms;
using System.Threading.Tasks;
using System;

namespace coffeefrontend
{
    public class GetHistoryPageViewModel : BaseViewModel
    {
        public List<Order> OrderHistory { get; protected set; }

        public async Task<int> init(string guid)
        {
            (string error, List<Order> result) = await App.Manager.GetHistoryTask(Application.Current.Properties["coffee_token"].ToString(), guid);
            if (error != null)
            {
                await Application.Current.MainPage.DisplayAlert("Alert", "Cannot get history", "Close");
            }
            else
            {
                this.OrderHistory = result;
                foreach (var order in OrderHistory)
                {
                    order.lastChangedAtDatetime = Convert.ToDateTime(order.lastChangedAt).ToLocalTime();
                }
            }
            return 99;
        }
    }
}