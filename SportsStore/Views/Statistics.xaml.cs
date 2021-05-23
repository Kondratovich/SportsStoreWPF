using SportsStore.ViewModels;
using System.Windows.Controls;

namespace SportsStore.Views {
    /// <summary>
    /// Interaction logic for Statistics.xaml
    /// </summary>
    public partial class Statistics : UserControl {
        MainViewModel mainViewModel;
        public Statistics() {
            InitializeComponent();
            mainViewModel = new MainViewModel("preferences");
            DataContext = mainViewModel;
        }
    }
}
