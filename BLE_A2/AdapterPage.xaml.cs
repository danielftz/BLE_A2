using Plugin.BluetoothLE;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BLE_A2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AdapterPage : ContentPage
    {
        //public IAdapter adapter;
        //public IDisposable scanner;
        //public Boolean isScanning;

        //privat/*e ObservableCollect*/ion<Deviceinfo> _deviceList;
        public AdapterPage()
        {
            BindingContext = new AdapterViewModel();
            InitializeComponent();
            //isScanning = false;
            //_deviceList = new ObservableCollection<Deviceinfo>();
            //listView.ItemsSource = _deviceList;
        }
        //private void Status_Clicked(object sender, EventArgs e)
        //{

        //    ((Button)sender).Text = "Adapter Status: " + CrossBleAdapter.Current.Status;
        //}

        //private void ScanButton_Clicked(object sender, EventArgs e)
        //{
        //    if (isScanning)
        //    {
        //        scanner.Dispose();
        //        isScanning = false;
        //        ScanButton.Text = "Scan";
        //    }
        //    else
        //    {
        //        _deviceList.Clear();
        //        isScanning = true;

        //        scanner = CrossBleAdapter.Current.Scan().Subscribe(scanResult => {
        //            _deviceList.Add(new Deviceinfo { Name = scanResult.Device.Name });

        //        });


        //        ScanButton.Text = "Stop Scanning";
        //    }


        //}
    }


}