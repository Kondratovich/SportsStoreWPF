using SportsStore.Models;
using SportsStore.ViewModels;
using System.Windows;

namespace SportsStore.Views {
    /// <summary>
    /// Interaction logic for EditProduct.xaml
    /// </summary>
    public partial class EditProduct : Window {
        public EditProduct(Product selectedProduct) {
            InitializeComponent();
            MainViewModel mainViewModel = new MainViewModel(selectedProduct);
            DataContext = mainViewModel;
            mainViewModel.ClosingRequest += (sender, e) => this.Close();
        }
    }
}
