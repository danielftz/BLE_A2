using ReactiveUI;
using ReactiveUI.XamForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BLE_A2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DevicePage : ContentPage,IViewFor<DevicePageViewModel>
    {
        //readonly CompositeDisposable _bindingsDisposable = new CompositeDisposable();
        public DevicePage() {
            InitializeComponent();
            this.WhenActivated(disposable => {
                this.OneWayBind(ViewModel, vm => vm.UUID_list, v => v.UUID_list.ItemsSource).DisposeWith(disposable);
                //this.OneWayBind(ViewModel, vm => vm.ButtonPressed, v => v.buttonPressed.Text).DisposeWith(disposable);
            }
            );
           
        }
        protected override void OnAppearing() {
            base.OnAppearing();

            //this.OneWayBind(ViewModel, vm => vm.itemCount, v => v.ItemCount.Text).DisposeWith(_bindingsDisposable);
            //this.WhenAnyValue(x => x.ViewModel.itemCount).BindTo(this, view => view.ItemCount.Text);
            //BindingContext = ViewModel;
            BindingContext = ViewModel;
        }

        public DevicePageViewModel ViewModel { get; set; }
        object IViewFor.ViewModel {
            get { return ViewModel; }
            set { ViewModel = (DevicePageViewModel)value; }
        }
    }
}