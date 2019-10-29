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

namespace BLE_A2.ScanPage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AdapterPage : ContentPage
    {
        public AdapterPage()
        {
            BindingContext = new AdapterViewModel();
            InitializeComponent();
            
        }
        
    }


}