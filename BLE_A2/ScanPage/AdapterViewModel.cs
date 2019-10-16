using System;
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
        IAdapter adapter;
        IDisposable scan;

        public ICommand ScanToggle { get; private set; }

        [Reactive] public bool IsScanning { get; private set; }
        [Reactive] public int connected_devices { get; private set; }
        public IObservableCollection<ScanResultViewModel> Devices { get; }

        public override void OnAppearing()
        {
            //initial assignment of values
            
            this.IsScanning = false;
            this.connected_devices = 0;
            base.OnAppearing();
        }

        public AdapterViewModel()
        {

            //this.connected_devices = 10;
            this.ScanToggle = ReactiveCommand.Create(
                () =>
                {
                    if (!IsScanning)
                    {
                        
                        this.IsScanning = true;
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
                        this.connected_devices = 0;
                        //this.scan = adapter
                        //.Scan()
                        //.Buffer(TimeSpan.FromSeconds(1))
                        //.ObserveOn(RxApp.MainThreadScheduler)
                        //.Subscribe(
                        //    scanResult => {
                                
                                //foreach (var result in scanResult)
                                //{
                                //        var dev = this.Devices.FirstOrDefault(x => x.Uuid.Equals(result.Device.Uuid));

                                //        if (dev != null)
                                //        {
                                //            dev.TrySet(result);
                                //        }
                                //        else
                                //        {
                                //            dev = new ScanResultViewModel();
                                //            dev.TrySet(result);
                                //            Devices.Add(dev);
                                //        }

                                //    }
                                
                            //});

                    }
                }


             );
        }

        

    }
}
