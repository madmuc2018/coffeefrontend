using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace coffeefrontend
{
    public partial class UpdateOrderPage : ContentPage
    {
        UpdatePageViewModel viewModel;

        public UpdateOrderPage(UpdatePageViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = this.viewModel = viewModel;
        }
    }
}