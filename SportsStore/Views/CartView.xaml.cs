using SportsStore.Models;
using SportsStore.ViewModels;
using SportsStore.Views;
using System.Windows.Controls;

namespace SportsStore {
    /// <summary>
    /// Interaction logic for Cart.xaml
    /// </summary>
    public partial class CartView : UserControl {
        MainViewModel mainViewModel;
        public CartView(User currentUser) {
            InitializeComponent();
            mainViewModel = new MainViewModel(currentUser.Email);
            mainViewModel.CurrentUser = currentUser;
            DataContext = mainViewModel;
        }
    }
}
