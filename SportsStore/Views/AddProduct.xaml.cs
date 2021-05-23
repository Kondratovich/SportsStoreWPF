using Microsoft.Win32;
using SportsStore.ViewModels;
using System.Windows.Controls;

namespace SportsStore.Views {
    /// <summary>
    /// Interaction logic for AddProduct.xaml
    /// </summary>
    public partial class AddProduct : UserControl {
        public AddProduct() {
            InitializeComponent();
            DataContext = new MainViewModel();
        }

        private void UploadImageBtn_Click(object sender, System.Windows.RoutedEventArgs e) {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpeg)|*.png;*.jpeg|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true) {
                string imagePath = openFileDialog.FileName;
            }
        }
    }
}
