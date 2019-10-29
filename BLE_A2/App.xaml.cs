using ReactiveUI;
using Splat;
using System;
using System.Reflection;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BLE_A2
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            var Navigator = new AppNvgController();
            //MainPage = new ScanPage.AdapterPage();
            MainPage = new FirstPage(Navigator.generateFirstPage());
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
