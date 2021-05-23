using SportsStore.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace SportsStore {
    /// <summary>
    /// Interaction logic for Registration.xaml
    /// </summary>
    public partial class RegistrationWindow : Window {
        public RegistrationWindow() {
            InitializeComponent();
            LoginViewModel loginViewModel = new LoginViewModel();
            DataContext = loginViewModel;
            loginViewModel.ClosingRequest += (sender, e) => this.Close();
        }

        private void BackToStartWindowBtn_Click(object sender, RoutedEventArgs e) {
            StartupWindow startupWindow = new StartupWindow();
            startupWindow.Show();
            Close();
        }

        private void passwordBox_PasswordChanged(object sender, RoutedEventArgs e) {
            LoginViewModel.Password = passwordBox.Password;
        }
    }
}
