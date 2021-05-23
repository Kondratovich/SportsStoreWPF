using SportsStore.Models;
using SportsStore.ViewModels;
using System.Collections.Generic;
using System.Windows.Controls;

namespace SportsStore.Views {
    /// <summary>
    /// Interaction logic for ListProducts.xaml
    /// </summary>
    public partial class ListProducts : UserControl {
        MainViewModel mainViewModel;
        public ListProducts() {
            InitializeComponent();
            mainViewModel = new MainViewModel("products");
            DataContext = mainViewModel;
        }

        private void EditBtn_Click(object sender, System.Windows.RoutedEventArgs e) {   
            var usc = new EditProduct((Product)ProductsDataGrid.SelectedItem);
            usc.Show();
        }
        private void DeleteBtn_Click(object sender, System.Windows.RoutedEventArgs e) {
            mainViewModel.DeleteProduct((Product)ProductsDataGrid.SelectedItem);
        }
    }
}
