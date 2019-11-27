using BLE_A2.Infrastructure;
using Plugin.BluetoothLE;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Text;

namespace BLE_A2
{
    public class DevicePageViewModel : ViewModel, IRoutableViewModel {
        public string UrlPathSegment => "Device";

        public IScreen HostScreen { get; }

        public IDevice Blinky;

        public ObservableCollection<String> UUID_list { get; }
        public ObservableCollection<Guid> Service_UUID_list;
        //[Reactive] public string itemCount { get; set; }
        private string buttonString;
        //private int kk = 0;
        public string ButtonPressed {
            get => buttonString;
            set => this.RaiseAndSetIfChanged(ref buttonString, value);
        }
        [Reactive] public string IsButtonpressed {get;private set;}
        [Reactive] public string BatteryLevel { get; private set; }

        public override void OnAppearing() {
            //initial assignment of values

            //this.itemCount="0";
            //this.itemCount = UUID_list.Count().ToString();
            base.OnAppearing();
            

        }

        public override void OnDisappearing() {
            this.IsButtonpressed = "None";
            base.OnDisappearing();
            
        }
        public DevicePageViewModel(IScreen hostScreen, IDevice device) {
            //Debug.WriteLine("fuck");
            //Debug.WriteLine(device.Name);
            HostScreen = hostScreen;
            Blinky = device;
            this.IsButtonpressed = "None";
            this.Blinky.Connect();
            UUID_list = new ObservableCollection<string>();
            //this.itemCount = UUID_list.Count().ToString();
            this.Blinky
                .WhenAnyCharacteristicDiscovered()
                .ObserveOn(RxApp.MainThreadScheduler)
                .Subscribe(chs => {
                    try {
                        //Debug.WriteLine("bruh");
                        //Debug.WriteLine(chs.Uuid.ToString());
                        //Debug.WriteLine(chs.Service.Description);
                        //Debug.WriteLine(chs.Description);
                        var uuid = this.UUID_list.FirstOrDefault(x => x.Equals(chs.Uuid.ToString()));
                        if (uuid == null) {
                            //Debug.WriteLine("bruh");
                            UUID_list.Add(chs.Uuid.ToString());
                        }
                        //Console.Write(chs.Uuid.ToString());

                        if (chs.Uuid.ToString().ToUpper() == "00001524-1212-EFDE-1523-785FEABCD123") {
                            //Blinky button
                            //var ButtonService = new BlinkyButtonViewModel(chs);
                            //chs.Read().Timeout(TimeSpan.FromSeconds(2)).Subscribe(
                            //    result => {
                            //        //var final_read = Encoding.UTF8.GetString(result.Data, 0, result.Data.Length);
                            //        this.ButtonPressed = BitConverter.ToString(result.Data);
                            //        Debug.WriteLine(BitConverter.ToString(result.Data));

                            //        //Debug.WriteLine(final_read);
                            //    }

                            //);
                            
                            chs.RegisterAndNotify().Subscribe(
                                result => {
                                    //var final_read = Encoding.UTF8.GetString(result.Data, 0, result.Data.Length);

                                    //kk += 1;
                                    //this.ButtonPressed = BitConverter.ToString(result.Data);
                                    var op = BitConverter.ToString(result.Data);
                                    if (op == "01") this.IsButtonpressed = "Button Pressed";
                                    else this.IsButtonpressed = "Button Released";
                                 
                                    //this.ButtonPressed = kk.ToString();
                                    //Debug.WriteLine(this.ButtonPressed);
                                    //Debug.WriteLine(kk.ToString());
                                    //Debug.WriteLine(final_read);
                                }
                            );

                            
                        }
                        else if (chs.Uuid.ToString().ToUpper() == "00002A19-0000-1000-8000-00805F9B34FB") {
                            chs.RegisterAndNotify().Subscribe(
                                result => {
                                    //var final_read = Encoding.UTF8.GetString(result.Data, 0, result.Data.Length);

                                    //kk += 1;
                                    //this.ButtonPressed = BitConverter.ToString(result.Data);
                                    Debug.WriteLine(result.Data.Length);
                                    var bl = BitConverter.ToInt32(result.Data,1);
                                    Debug.WriteLine(bl.ToString());
                                    this.BatteryLevel = String.Format("Battery: {0}%",bl);
                                    
                                }
                            );

                        }

                    }
                    catch (Exception ex) {
                        Console.Write(ex);
                    }
                    //UUID_list.Add(chs.Uuid.ToString());
                    //Console.WriteLine(UUID_list.Count().ToString());




                })
                .DisposeWith(this.DeactivateWith);
        }
    }
}
