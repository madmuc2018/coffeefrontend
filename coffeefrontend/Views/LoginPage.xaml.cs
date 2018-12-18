using Xamarin.Forms;

namespace coffeefrontend
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        //private (bool, string, string, string) getUsernameAndPassword() 
        //{
        //    string username = usernameEntry.Text;
        //    string password = passwordEntry.Text;
        //    string role = roleEntry.Text;

        //    if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(role))
        //    {
        //        DisplayAlert("Alert", "Please input username and password", "Close");
        //        return (false, "", "", "");
        //    }
        //    return (true,username,password,role);
        //}
    }
}
