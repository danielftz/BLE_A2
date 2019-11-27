using ReactiveUI;
using ReactiveUI.XamForms;
using Splat;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace BLE_A2
{
    public class AppBootStrapper : ReactiveObject, IScreen
    {
        public RoutingState Router { get; private set; }
        public AppBootStrapper(IMutableDependencyResolver dependencyResolver = null, RoutingState router = null) {
            //BlobCache.ApplicationName = Configuration.ApplicationName;
            Router = router ?? new RoutingState();
            RegisterParts(dependencyResolver ?? Locator.CurrentMutable);
            Router.Navigate.Execute(new ScanPageViewModel(this));
        }

        private void RegisterParts(IMutableDependencyResolver dependencyResolver) {
            dependencyResolver.RegisterConstant(this, typeof(IScreen));
            dependencyResolver.Register(() => new ScanPage(), typeof(IViewFor<ScanPageViewModel>));
            dependencyResolver.Register(() => new DevicePage(), typeof(IViewFor<DevicePageViewModel>));

        }
        public Page CreateMainPage() {
            return new RoutedViewHost();
        }
    }
}
