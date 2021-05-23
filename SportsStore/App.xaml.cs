using SportsStore.Server;
using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows;
using static System.Net.Mime.MediaTypeNames;

namespace SportsStore {
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : System.Windows.Application {
        protected override void OnStartup(StartupEventArgs e) {
            base.OnStartup(e);
            ServerOperations.ConnectToServer();
        }
    }
}
