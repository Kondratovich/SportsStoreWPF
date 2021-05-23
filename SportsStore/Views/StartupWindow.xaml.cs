using SportsStore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SportsStore {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class StartupWindow : Window {
        public StartupWindow() {
            InitializeComponent();
            LoginViewModel loginViewModel = new LoginViewModel();
            DataContext = loginViewModel;
            loginViewModel.ClosingRequest += (sender, e) => this.Close();
        }

        private void GoToRigistrationBtn_Click(object sender, RoutedEventArgs e) {
            RegistrationWindow registrationForm = new RegistrationWindow();
            registrationForm.Show();
            Close();
        }

        private void passwordBox_PasswordChanged(object sender, RoutedEventArgs e) {
            LoginViewModel.Password = passwordBox.Password;
        }
    }
}
