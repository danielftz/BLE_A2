using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Windows.Input;
using BLE_A2.Infrastructure;
using DynamicData;
using DynamicData.Binding;
using Plugin.BluetoothLE;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Xamarin.Forms;

namespace BLE_A2
{
    public class ScanPageViewModel : ViewModel,IRoutableViewModel
    {

        public string UrlPathSegment => "Scan";

        public IScreen HostScreen { get; set; }


        //readonly IAdapterScanner adapterScanner;
        IAdapter adapter = CrossBleAdapter.Current;
        IDisposable scan;

        public ICommand ScanToggle { get; private set; }
        //public ICommand SelectDevice { get; private set; }
        
        public ReactiveCommand<ScanResultViewModel,Unit> SelectDevice { get; set; }
        

        [Reactive] public bool IsScanning { get; private set; }
        //[Reactive] public string adapter_is_scanning { get; private set; }
        //[Reactive] public string connected_devices { get; private set; }

        //[Reactive] public string adapter_status { get; private set; }
        public ObservableCollection<ScanResultViewModel> Devices { get; }


        //public SourceCache<ScanResultViewModel, Guid> Devices { get; set; }


        public override void OnAppearing()
        {
            //initial assignment of values
            
            this.IsScanning = false;
            
            base.OnAppearing();

        }

        public override void OnDisappearing() {
            base.OnDisappearing();
            this.IsScanning = false;
        }

        public ScanPageViewModel(IScreen hostScreen)
        {
            

            HostScreen = hostScreen;
            Devices = new ObservableCollection<ScanResultViewModel>();
            //Devices = new SourceCache<ScanResultViewModel, Guid>(t => t.Uuid);
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
                                    

                                }
                             
                            }
                        ).DisposeWith(this.DeactivateWith);
                       

                    }
                    else
                    {
                        this.scan?.Dispose();
                        this.IsScanning = false;
                        Devices.Clear();

                    }
                }


             );

            this.SelectDevice = ReactiveCommand.CreateFromObservable<ScanResultViewModel,Unit>(
                x => { 
                    HostScreen.Router.Navigate.Execute(new DevicePageViewModel(hostScreen,x.Device)).Subscribe();
                    return Observable.Return(Unit.Default);
                }
            );
        }

        

    }
}
