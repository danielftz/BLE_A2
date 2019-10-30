using ReactiveUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace BLE_A2
{
    class DummyPageViewModel : IRoutableViewModel
    {
        public string UrlPathSegment => "Dummies";
        public IScreen HostScreen { get; set; }

        public DummyPageViewModel(IScreen hostScreen) {
            HostScreen = hostScreen;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public event PropertyChangingEventHandler PropertyChanging;

        public void RaisePropertyChanging(PropertyChangingEventArgs args) {
            throw new NotImplementedException();
        }

        public void RaisePropertyChanged(PropertyChangedEventArgs args) {
            throw new NotImplementedException();
        }
    }
}
