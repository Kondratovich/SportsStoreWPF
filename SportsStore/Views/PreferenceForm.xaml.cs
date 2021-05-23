using SportsStore.Models;
using SportsStore.ViewModels;
using System.Windows.Controls;

namespace SportsStore {
    /// <summary>
    /// Interaction logic for Account.xaml
    /// </summary>
    public partial class PreferenceForm : UserControl {
        MainViewModel mainViewModel;
        public PreferenceForm(User currentUser) {
            InitializeComponent();
            mainViewModel = new MainViewModel();
            mainViewModel.CurrentUser = currentUser;
            DataContext = mainViewModel;
        }
    }
}
