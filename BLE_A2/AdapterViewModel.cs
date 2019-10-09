using System.Windows.Input;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Xamarin.Forms;

namespace BLE_A2
{
    class AdapterViewModel : ReactiveObject
    {
        public ICommand ScanToggle { get; private set; }
        [Reactive] public bool IsScanning { get; private set; }


        public AdapterViewModel()
        {
            

            this.ScanToggle = ReactiveCommand.Create(
                () =>
                {
                    if (!IsScanning)
                    {
                        this.IsScanning = true;

                    }
                    else
                    {
                        this.IsScanning = false;

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
