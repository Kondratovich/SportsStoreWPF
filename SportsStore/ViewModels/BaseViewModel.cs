using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace SportsStore.ViewModels {
    public class BaseViewModel : INotifyPropertyChanged {

        public event EventHandler ClosingRequest;
        protected void OnClosingRequest() {
            if (this.ClosingRequest != null) {
                this.ClosingRequest(this, EventArgs.Empty);
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string prop = "") {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
