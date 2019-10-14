using System;
using System.Linq;
using System.Reactive.Linq;
using System.Windows.Input;
using DynamicData.Binding;
using Plugin.BluetoothLE;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Xamarin.Forms;

namespace BLE_A2
{
    class AdapterViewModel : ReactiveObject
    {
        IAdapter adapter=CrossBleAdapter.Current;
        IDisposable scan;

        public ICommand ScanToggle { get; private set; }

        [Reactive] public bool IsScanning { get; private set; }
        [Reactive] public int connected_devices { get; private set; }
        public IObservableCollection<ScanResultViewModel> Devices { get; }
        public AdapterViewModel()
        {

            //this.connected_devices = 10;
            this.ScanToggle = ReactiveCommand.Create(
                () =>
                {
                    if (!IsScanning)
                    { 
                        
                        this.IsScanning = true;
                        this.scan = adapter
                            .Scan()
                            .Buffer(TimeSpan.FromSeconds(1))
                            .ObserveOn(RxApp.MainThreadScheduler)
                            .Subscribe(
                                scanResult =>
                                {
                                    foreach (var result in scanResult)
                                    {
                                        if (result.Device.Name == "Nordic_Blinky"){
                                            this.connected_devices = 1;
                                        }
                                    }
                                }
                            );
                        //this.connected_devices = Devices.Count();

                    }
                    else
                    {
                        this.scan?.Dispose();
                        //this.Devices.Clear();
                        this.IsScanning = false;
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

        //public override void OnAppearing()
        //{
        //    base.OnAppearing();
        //    this.IsScanning = false;
        //}

    }
}
