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
using System.Windows.Shapes;

namespace SportsStore {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        MainViewModel mainViewModel;
        public MainWindow(User currentUser) {
            InitializeComponent();
            mainViewModel = new MainViewModel(currentUser);
            DataContext = mainViewModel;
            UserControl usc = new Home(currentUser);
            GridMain.Children.Clear();
            GridMain.Children.Add(usc);
        }
        public MainWindow() {
            InitializeComponent();
            User currentUser = new User() { Name = "Loh", Email = "kishk@bk.cu" };
            mainViewModel = new MainViewModel(currentUser);
            DataContext = mainViewModel;
            UserControl usc = new Home(currentUser);
            GridMain.Children.Clear();
            GridMain.Children.Add(usc);
        }

        private void ButtonOpenMenu_Click(object sender, RoutedEventArgs e) {
            ButtonCloseMenu.Visibility = Visibility.Visible;
            ButtonOpenMenu.Visibility = Visibility.Collapsed;
        }

        private void ButtonCloseMenu_Click(object sender, RoutedEventArgs e) {
            ButtonCloseMenu.Visibility = Visibility.Collapsed;
            ButtonOpenMenu.Visibility = Visibility.Visible;
        }

        private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            UserControl usc = null;
            GridMain.Children.Clear();

            switch (((ListViewItem)((ListView)sender).SelectedItem).Name) {
                case "ItemHome":
                    usc = new Home(mainViewModel.CurrentUser);
                    GridMain.Children.Add(usc);
                    break;
                case "ItemCatalog":
                    usc = new Catalog(mainViewModel.CurrentUser);
                    GridMain.Children.Add(usc);
                    break;
                case "ItemCart":
                    usc = new CartView(mainViewModel.CurrentUser);
                    GridMain.Children.Add(usc);
                    break;
                case "ItemPreferenceForm":
                    usc = new PreferenceForm(mainViewModel.CurrentUser);
                    GridMain.Children.Add(usc);
                    break;
                case "ItemExit":
                    Application.Current.Shutdown();
                    break;
                default:
                    break;
            }
        }

        private void ButtonLogout_Click(object sender, RoutedEventArgs e) {
            StartupWindow startupWindow = new StartupWindow();
            startupWindow.Show();
            Close();
        }
    }
}
