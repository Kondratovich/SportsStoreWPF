using SportsStore.Models;
using SportsStore.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
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
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : UserControl {
        MainViewModel mainViewModel;
        public Home(User currentUser) {
            InitializeComponent();
            mainViewModel = new MainViewModel("storeInfo");
            mainViewModel.CurrentUser = currentUser;
            DataContext = mainViewModel;
        }
    }
}
