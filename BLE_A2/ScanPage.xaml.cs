using Plugin.BluetoothLE;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BLE_A2
{
    [DesignTimeVisible(false)]
    public partial class ScanPage : ContentPage, IViewFor<ScanPageViewModel>
    {
        readonly CompositeDisposable _bindingsDisposable = new CompositeDisposable();
        public ScanPage()
        {
            InitializeComponent();
            
        }
        //protected override void OnAppearing() {
        //    base.OnAppearing();
        //    this.BindCommand(ViewModel, vm => vm., v => v.).DisposeWith(_bindingsDisposable);
        //}

        protected override void OnAppearing() {
            base.OnAppearing();
            
            this.OneWayBind(ViewModel, vm => vm.Devices, v=>v.Devices.ItemsSource).DisposeWith(_bindingsDisposable);
            Devices.Events().ItemSelected.Subscribe(e => ViewModel.SelectDevice.Execute((ScanResultViewModel)e.SelectedItem)).DisposeWith(_bindingsDisposable);
            BindingContext = ViewModel;
        }

        protected override void OnDisappearing() {
            base.OnDisappearing();
            _bindingsDisposable.Clear();
        }

        public ScanPageViewModel ViewModel { get; set; }

        object IViewFor.ViewModel {
            get { return ViewModel; }
            set { ViewModel = (ScanPageViewModel)value; }
        }

    }


}