using SportsStore.Models;
using SportsStore.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace SportsStore.Views {
    /// <summary>
    /// Interaction logic for AdminMainWindow.xaml
    /// </summary>
    public partial class AdminMainWindow : Window {
        public AdminMainWindow(User currentUser) {
            InitializeComponent();
            DataContext = new MainViewModel(currentUser);
            UserControl usc = new Home(currentUser);
            GridMain.Children.Clear();
            GridMain.Children.Add(usc);
        }

        public AdminMainWindow() {
            InitializeComponent();
            var mainViewModel = new MainViewModel(new User() { Name = "Loh" });
            DataContext = mainViewModel;
            UserControl usc = new ListProducts();
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
                case "ItemListProducts":
                    usc = new ListProducts();
                    GridMain.Children.Add(usc);
                    break;
                case "ItemAddProduct":
                    usc = new AddProduct();
                    GridMain.Children.Add(usc);
                    break;
                case "ItemStatistics":
                    usc = new Statistics();
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
