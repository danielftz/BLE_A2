using ReactiveUI;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BLE_A2
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            RxApp.SuspensionHost.CreateNewAppState = () => new AppBootStrapper();
            RxApp.SuspensionHost.SetupDefaultSuspendResume();

            MainPage = RxApp.SuspensionHost
                        .GetAppState<AppBootStrapper>()
                        .CreateMainPage();
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
