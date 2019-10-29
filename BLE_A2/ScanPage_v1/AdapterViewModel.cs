using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Windows.Input;
using BLE_A2.Infrastructure;
using DynamicData.Binding;
using Plugin.BluetoothLE;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Xamarin.Forms;

namespace BLE_A2.ScanPage
{
    class AdapterViewModel : ViewModel
    {
        //readonly IAdapterScanner adapterScanner;
        IAdapter adapter = CrossBleAdapter.Current;
        IDisposable scan;

        public ICommand ScanToggle { get; private set; }
        private ICommand FindAdapter { get; }

        [Reactive] public bool IsScanning { get; private set; }
        [Reactive] public string adapter_is_scanning { get; private set; }
        [Reactive] public string connected_devices { get; private set; }

        [Reactive] public string adapter_status { get; private set; }
        public ObservableCollection<ScanResultViewModel> Devices { get; }

        public override void OnAppearing()
        {
            //initial assignment of values
            
            this.IsScanning = false;
            
            base.OnAppearing();
        }

        public AdapterViewModel()
        {
            Devices = new ObservableCollection<ScanResultViewModel>();
            //this.connected_devices = this.adapter.IsScanning.ToString();

            //this.FindAdapter = ReactiveCommand.Create(
            //    () =>
            //    {
            //        adapterScanner
            //            .FindAdapters()
            //            .ObserveOn(RxApp.MainThreadScheduler)
            //            .Subscribe(
            //                async () =>
            //                {

            //                }
            //            )
            //    }
            //    );
            //this.connected_devices = 10;
            //this.adapter_is_scanning = this.adapter.IsScanning.ToString();
            /*this.adapter_is_scanning = this.adapter.IsScanning.ToString();
            this.adapter_status = this.adapter.Status.ToString();*/
            this.ScanToggle = ReactiveCommand.Create(
                () =>
                {
                    if (!IsScanning) //when (Press to scan)
                    {
                        //this.connected_devices = this.adapter.Status.ToString();
                        //this.adapter.SetAdapterState(true);
                        //this.adapter_status = this.adapter.Status.ToString();
                        this.IsScanning = true;
                        this.scan = this.adapter.Scan().Buffer(TimeSpan.FromSeconds(0.2)).ObserveOn(RxApp.MainThreadScheduler).Subscribe(
                            results =>
                            {
                               /* this.connected_devices = results.Count().ToString();
                                this.adapter_is_scanning = this.adapter.IsScanning.ToString();
                                this.adapter_status = this.adapter.Status.ToString();*/

                                foreach (var r in results)
                                {
                                    //dev is a scanresultviewmodel object with the name nordic_blinky
                                    if (r.Device.Name == "Nordic_Blinky" || r.Device.Name=="iBBQ"){
                                        var dev = this.Devices.FirstOrDefault(x =>
                                            x.Uuid.Equals(r.Device.Uuid)
                                        );

                                        if (dev == null) {
                                            dev = new ScanResultViewModel();
                                            dev.TrySet(r);
                                            Devices.Add(dev);
                                            
                                        }
                                        else {
                                            dev.TrySet(r);
                                        };
                                        /*Devices.Add(dev);*/

                                    };
                                    
                                    
                                    //if there is no nordic blinky in Devices
                                    //add to it
                                    /*if (dev == null && r.Device.Name == "Nordic_Blinky") {
                                        dev.TrySet(r);
                                        Devices.Add(dev);
                                    }*/

                                }
                                //if (result.Device.Name == "Nordic_Blinky")
                                //{
                                //    this.connected_devices += 1;
                                //}
                            }
                        ).DisposeWith(this.DeactivateWith);
                        //this.scan = this.adapter
                        //    .Scan()
                        //    .Buffer(TimeSpan.FromSeconds(1))
                        //    .ObserveOn(RxApp.MainThreadScheduler)
                        //    .Subscribe(
                        //        scanResult =>
                        //        {
                        //            //if (scanResult[0].Device.Name == "Nordic_Blinky"){
                        //            //    this.connected_devices += 1;
                        //            //}
                        //            ////this.connected_devices = 3;
                        //            ////this.connected_devices += (1+scanResult.Count());
                        //            //foreach (var result in scanResult)
                        //            //{
                        //            //    this.connected_devices = 9;
                        //            //    if (result.Device.Name == "Nordic_Blinky")
                        //            //    {

                        //            //        //this.connected_devices = 1;
                        //            //    }
                        //            //}
                        //        }
                        //    ).DisposeWith(this.DeactivateWith);
                            //.DisposeWith(this.scan); ;
                        //this.connected_devices = Devices.Count();

                    }
                    else
                    {
                        this.scan?.Dispose();
                        //this.Devices.Clear();
                        this.IsScanning = false;
                        /*this.adapter_is_scanning = this.adapter.IsScanning.ToString();
                        this.adapter_status = this.adapter.Status.ToString();*/
                        Devices.Clear();

                    }
                }


             );
        }

        

    }
}
