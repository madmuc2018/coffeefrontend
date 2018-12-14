using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;

namespace coffeefrontend
{
    public class UpdatePageViewModel : INotifyPropertyChanged
    {
        private Order selectedOrder;
        public ICommand SubmitCommand { protected set; get; }

        public UpdatePageViewModel(Order SelectedOrder)
        {
            this.selectedOrder = SelectedOrder;
            SubmitCommand = new Command(() =>
            {
                Debug.WriteLine($"Submitted {selectedOrder.id} {selectedOrder.status}");
            });
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public Order SelectedOrder
        {
            get
            {
                return selectedOrder;
            }
            set
            {
                if (!selectedOrder.Equals(value))
                {
                    selectedOrder = value;
                    OnPropertyChanged();
                }
            }
        }
    }
}
