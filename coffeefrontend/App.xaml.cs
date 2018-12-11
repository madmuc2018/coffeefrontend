using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace coffeefrontend
{
    public partial class App : Application
    {
        public static CoffeefrontendManager Manager { get; private set; }

        public App()
        {
            InitializeComponent();
            Manager = new CoffeefrontendManager(new RestService());
            MainPage = new LoginPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
