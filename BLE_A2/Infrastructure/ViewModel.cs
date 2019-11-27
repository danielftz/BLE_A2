using Prism.Navigation;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Text;

namespace BLE_A2.Infrastructure
{
    public abstract class ViewModel : ReactiveObject
    {
        CompositeDisposable deactivateWith;
        protected CompositeDisposable DeactivateWith
        {
            get
            {
                if (this.deactivateWith == null)
                    this.deactivateWith = new CompositeDisposable();
                return this.deactivateWith;
            }
        }
        public virtual void OnAppearing() { }
        public virtual void OnDisappearing() 
        {
            this.deactivateWith?.Dispose();
            this.deactivateWith = null;
        }

        public virtual void Destroy()
        {
            this.DestroyWith?.Dispose();
        }
        protected CompositeDisposable DestroyWith { get; } = new CompositeDisposable();

    }
}
