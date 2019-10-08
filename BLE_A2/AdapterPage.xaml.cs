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
        public IAdapter adapter;
        public IDisposable scanner;
        public Boolean isScanning;
        public Int32 devCount;

        private ObservableCollection<Deviceinfo> _deviceList;
        public AdapterPage()
        {
            InitializeComponent();
            isScanning = false;
            _deviceList = new ObservableCollection<Deviceinfo>
            {
                new Deviceinfo{ Name="Device1"},
                new Deviceinfo{Name="Device2"}
            };
            listView.ItemsSource = _deviceList;
            devCount = 2;
        }
        private void Button1_Clicked(object sender, EventArgs e)
        {

            ((Button)sender).Text = "Adapter Status: " + CrossBleAdapter.Current.Status;
        }

        private void ScanButton_Clicked(object sender, EventArgs e)
        {
            if (isScanning)
            {
                scanner.Dispose();
                isScanning = false;
                ScanButton.Text = "Scan";
                devCount++;
                var myDeviceName = new Deviceinfo { Name = "Device" + devCount };
                _deviceList.Add(myDeviceName);
            }
            else
            {

                isScanning = true;

                scanner = CrossBleAdapter.Current
                    .Scan()
                    .Buffer(TimeSpan.FromSeconds(1))
                    .Subscribe(scanResult =>
                    {
                        // try something else
                        // this didn't work:  scanLabel.Text = scanResult.Device.Name;
                        foreach (var result in scanResult) //result.Device.Name
                        {
                            devCount++;
                            var myDeviceName = new Deviceinfo { Name = "Device" + devCount };
                            _deviceList.Add(myDeviceName);
                        }
                    });



                ScanButton.Text = "Stop Scanning";
            }


        }
    }


}