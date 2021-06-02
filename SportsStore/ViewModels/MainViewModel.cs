using Microsoft.Win32;
using SportsStore.Models;
using SportsStore.Server;
using SportsStore.ViewModels.Commands;
using System;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;

namespace SportsStore.ViewModels {
    public class MainViewModel : BaseViewModel {

        private ObservableCollection<Product> _products = new ObservableCollection<Product>();

        public ObservableCollection<Product> Products {
            get { return _products; }
            set {
                _products = value;
                OnPropertyChanged("Products");
            }
        }

        private Product _selectedProduct = new Product();
        public Product SelectedProduct {
            get {
                return _selectedProduct;
            }
            set {
                _selectedProduct = value;
                OnPropertyChanged("SelectedProduct");
            }
        }

        private StoreInfo _storeInfo = new StoreInfo();
        public StoreInfo StoreInfo {
            get {
                return _storeInfo;
            }
            set {
                _storeInfo = value;
                OnPropertyChanged("StoreInfo");
            }
        }

        private Preference _preference = new Preference();
        public Preference Preference {
            get {
                return _preference;
            }
            set {
                _preference = value;
                OnPropertyChanged("PreferenceForm");
            }
        }

        private ObservableCollection<PreferenceSums> _preferenceSums = new ObservableCollection<PreferenceSums>();
        public ObservableCollection<PreferenceSums> PreferenceSums {
            get {
                return _preferenceSums;
            }
            set {
                _preferenceSums = value;
                OnPropertyChanged("PreferenceSums");
            }
        }

        private ObservableCollection<Preference> _preferences = new ObservableCollection<Preference>();

        public ObservableCollection<Preference> Preferences {
            get { return _preferences; }
            set {
                _preferences = value;
                OnPropertyChanged("Preferences");
            }
        }

        private Cart _selectedCart = new Cart();
        public Cart SelectedCart {
            get {
                return _selectedCart;
            }
            set {
                _selectedCart = value;
                OnPropertyChanged("SelectedCart");
            }
        }

        private string _imagePath = "Assets/no_foto.jpg";
        public string ImagePath {
            get {
                return _imagePath;
            }
            set {
                _imagePath = value;
                OnPropertyChanged("ImagePath");
            }
        }

        private string _imagePathForEdit;
        public string ImagePathForEdit {
            get {
                if (_imagePathForEdit == null) {
                    _imagePathForEdit = SelectedProduct.ImagePath;
                    return _imagePathForEdit;

                }
                //else if (Path.GetFileName(_imagePathForEdit) != $"{SelectedProduct.Name}.jpg"){
                //    return _imagePathForEdit;
                //}
                //_imagePathForEdit = GetFullPathInDocuments(SelectedProduct.ImagePath);
                return _imagePathForEdit;
            }
            set {
                _imagePathForEdit = value;
                OnPropertyChanged("ImagePathForEdit");
            }
        }

        public User CurrentUser { get; set; }
        public MainViewModel(User currentUser) {
            CurrentUser = currentUser;
        }
        public MainViewModel() {
        }
        public MainViewModel(string selectedItems) {
            if (selectedItems == "products") {
                SelectProducts();   
            }
            else if(selectedItems == "storeInfo") {
                SelectStoreInfo();
            }
            else if (selectedItems == "preferences") {
                SelectPreferences();
            }
            else {
                SelectCartProducts(selectedItems);
            }
        }
        public MainViewModel(Product selectedProduct) {
            SelectedProduct = selectedProduct;
        }

        string GetFullPathInDocuments() {
            var baseFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var fullPath = Path.Combine(baseFolder, "SportsStore\\Images");
            return fullPath;
        }

        public void SelectStoreInfo() {
            ServerOperations.SendData($"SelectStoreInfo\n");
            string productData = ServerOperations.ReceiveData();
            StoreInfo.NumberOfUsers = Convert.ToInt32(productData.Substring(0, productData.IndexOf("\n")));
            productData = productData.Remove(0, productData.IndexOf("\n") + 1);
            StoreInfo.NumberOfProducts = Convert.ToInt32(productData.Substring(0, productData.IndexOf("\n")));
            productData = productData.Remove(0, productData.IndexOf("\n") + 1);
            int id = Convert.ToInt32(productData.Substring(0, productData.IndexOf("\n")));
            productData = productData.Remove(0, productData.IndexOf("\n") + 1);
            string name = productData.Substring(0, productData.IndexOf("\n"));
            productData = productData.Remove(0, productData.IndexOf("\n") + 1);
            double price = Convert.ToDouble(productData.Substring(0, productData.IndexOf("\n")));
            productData = productData.Remove(0, productData.IndexOf("\n") + 1);
            string description = productData.Substring(0, productData.IndexOf("\n"));
            productData = productData.Remove(0, productData.IndexOf("\n") + 1);
            string manufacturer = productData.Substring(0, productData.IndexOf("\n"));
            productData = productData.Remove(0, productData.IndexOf("\n") + 1);
            ServerOperations.SendData("next");
            productData = ServerOperations.ReceiveData();
            string imagePath = productData.Substring(0, productData.IndexOf("\n"));
            productData = productData.Remove(0, productData.IndexOf("\n") + 1);
            StoreInfo.BestProduct = new Product(id, name, price, description, manufacturer, imagePath);
        }

        public void SelectProducts() {
            ServerOperations.SendData($"SelectProducts\n");
            string productData = ServerOperations.ReceiveData();
            if(productData == "NoProducts") {
                MessageBox.Show("Нет товаров в базе данных!");
                return;
            }
            do {
                int id = Convert.ToInt32(productData.Substring(0, productData.IndexOf("\n")));
                productData = productData.Remove(0, productData.IndexOf("\n") + 1);
                string name = productData.Substring(0, productData.IndexOf("\n"));
                productData = productData.Remove(0, productData.IndexOf("\n") + 1);
                double price = Convert.ToDouble(productData.Substring(0, productData.IndexOf("\n")));
                productData = productData.Remove(0, productData.IndexOf("\n") + 1);
                string description = productData.Substring(0, productData.IndexOf("\n"));
                productData = productData.Remove(0, productData.IndexOf("\n") + 1);
                string manufacturer = productData.Substring(0, productData.IndexOf("\n"));
                productData = productData.Remove(0, productData.IndexOf("\n") + 1);
                ServerOperations.SendData("next");
                productData = ServerOperations.ReceiveData();
                string imagePath = productData.Substring(0, productData.IndexOf("\n"));
                productData = productData.Remove(0, productData.IndexOf("\n") + 1);
                _products.Add(new Product(id, name, price, description, manufacturer, imagePath));
                ServerOperations.SendData("next");
                productData = ServerOperations.ReceiveData();
            } while (productData != "end");
        }

        public void SelectPreferences() {
            ServerOperations.SendData($"SelectPreferences\n");
            string data = ServerOperations.ReceiveData();
            if (data == "NoPreferences") {
                MessageBox.Show("Нет оценок в базе данных!");
                return;
            }
            int IsProfessionalSum = 0;
            int PriceSum = 0;
            int KindsOfSportsSum = 0;
            int ManufacturerSum = 0;
            int Sum = 0;
            do {
                byte isProfessional = Convert.ToByte(data.Substring(0, data.IndexOf("\n")));
                data = data.Remove(0, data.IndexOf("\n") + 1);
                byte price = Convert.ToByte(data.Substring(0, data.IndexOf("\n")));
                data = data.Remove(0, data.IndexOf("\n") + 1);
                byte sport = Convert.ToByte(data.Substring(0, data.IndexOf("\n")));
                data = data.Remove(0, data.IndexOf("\n") + 1);
                byte manufacturer = Convert.ToByte(data.Substring(0, data.IndexOf("\n")));
                data = data.Remove(0, data.IndexOf("\n") + 1);
                string email = data.Substring(0, data.IndexOf("\n"));
                data = data.Remove(0, data.IndexOf("\n") + 1);
                IsProfessionalSum += (4 - isProfessional);
                PriceSum += (4 - price);
                KindsOfSportsSum += (4 - sport);
                ManufacturerSum += (4 - manufacturer);
                _preferences.Add(new Preference(isProfessional, price, sport, manufacturer, email));
                ServerOperations.SendData("next");
                data = ServerOperations.ReceiveData();
            } while (data != "end");
             Sum = (IsProfessionalSum + PriceSum + KindsOfSportsSum + ManufacturerSum);
            _preferenceSums.Add(new PreferenceSums(IsProfessionalSum, PriceSum, KindsOfSportsSum, ManufacturerSum, Sum));
        }

        public void SelectCartProducts(string email) {
            ServerOperations.SendData($"SelectCartProducts\n{email}");
            string productData = ServerOperations.ReceiveData();
            if (productData == "NoProducts") {
                MessageBox.Show("Нет товаров в корзине!");
                return;
            }
            do {
                int id = Convert.ToInt32(productData.Substring(0, productData.IndexOf("\n")));
                productData = productData.Remove(0, productData.IndexOf("\n") + 1);
                string name = productData.Substring(0, productData.IndexOf("\n"));
                productData = productData.Remove(0, productData.IndexOf("\n") + 1);
                double price = Convert.ToDouble(productData.Substring(0, productData.IndexOf("\n")));
                productData = productData.Remove(0, productData.IndexOf("\n") + 1);
                string description = productData.Substring(0, productData.IndexOf("\n"));
                productData = productData.Remove(0, productData.IndexOf("\n") + 1);
                string manufacturer = productData.Substring(0, productData.IndexOf("\n"));
                productData = productData.Remove(0, productData.IndexOf("\n") + 1);
                string dateAdded = productData.Substring(0, productData.IndexOf("\n"));
                productData = productData.Remove(0, productData.IndexOf("\n") + 1);
                ServerOperations.SendData("next");
                productData = ServerOperations.ReceiveData();
                string imagePath = productData.Substring(0, productData.IndexOf("\n"));
                productData = productData.Remove(0, productData.IndexOf("\n") + 1);
                _selectedCart.Products.Add(new Product(id, name, price, description, imagePath, manufacturer, dateAdded));
                ServerOperations.SendData("next");
                productData = ServerOperations.ReceiveData();
            } while (productData != "end");
        }

        private RelayCommand addProductCommand;
        public RelayCommand AddProductCommand {
            get {
                return addProductCommand ??
                  (addProductCommand = new RelayCommand(
                      obj => {
                          object[] parameters = obj as object[];
                          SelectedProduct.Name = (string)parameters[0];
                          try {
                            SelectedProduct.Price = Convert.ToDouble(parameters[1]);      
                          }
                          catch {
                              MessageBox.Show("- Некорректная цена. Пример: «18,6»\n");
                              return;
                          }
                          SelectedProduct.Description = (string)parameters[2];
                          SelectedProduct.Manufacturer = (string)parameters[3];
                          ValidationRules validation = new ValidationRules();
                          validation.AddProductValidate(SelectedProduct.Name, SelectedProduct.Price, SelectedProduct.Description, SelectedProduct.Manufacturer);
                          if (!String.IsNullOrEmpty(validation.ValidationErors)) {
                              MessageBox.Show(validation.ValidationErors);
                              return;
                          }
                          ServerOperations.SendData($"AddProduct\n{SelectedProduct.Name}\n{SelectedProduct.Price}\n{SelectedProduct.Description}\n{SelectedProduct.Manufacturer}\n{SelectedProduct.ImagePath}\n");
                            if (ServerOperations.ReceiveData() == "success")
                                MessageBox.Show("Товар добавлен");
                            else
                                MessageBox.Show("Ошибка добавления!");
                      }));
            }
        }

        private RelayCommand addPreferenceForm;
        public RelayCommand AddPreferenceForm {
            get {
                return addPreferenceForm ??
                  (addPreferenceForm = new RelayCommand(
                      obj => {
                          ValidationRules validation = new ValidationRules();
                          validation.EditPreferenceForm(Preference.IsProfessional, Preference.Price, Preference.KindsOfSports, Preference.Manufacturer);
                          if (!String.IsNullOrEmpty(validation.ValidationErors)) {
                              MessageBox.Show(validation.ValidationErors);
                              return;
                          }
                          ServerOperations.SendData($"AddPreferenceForm\n{Preference.IsProfessional}\n{Preference.Price}\n{Preference.KindsOfSports}\n{Preference.Manufacturer}\n{CurrentUser.Email}\n");
                            if (ServerOperations.ReceiveData() == "success")
                                MessageBox.Show("Форма успешно отправлена!");
                            else
                                MessageBox.Show("Ошибка отправки формы!");
                      }));
            }
        }

        private RelayCommand addToCartCommand;
        public RelayCommand AddToCartCommand {
            get {
                return addToCartCommand ??
                  (addToCartCommand = new RelayCommand(
                      obj => {
                          DateTime date = DateTime.Now;
                          ServerOperations.SendData($"AddToCart\n{CurrentUser.Email}\n{obj}\n{date.ToLocalTime()}\n");
                          if (ServerOperations.ReceiveData() == "success")
                              MessageBox.Show("Товар добавлен в корзину!");
                      }));
            }
        }

        private RelayCommand generateReportCommand;
        public RelayCommand GenerateReportCommand {
            get {
                return generateReportCommand ??
                  (generateReportCommand = new RelayCommand(
                      obj => {
                          StringBuilder table = new StringBuilder();
                          table.Append("\tИсходные оценки экспертов\n");
                          table.Append("\t+--+--+--+--+\n");
                          table.Append("\t|A1|A2|A3|A4|\n");
                          table.Append("\t+--+--+--+--+\n");
                          foreach(var preference in Preferences) {
                                table.Append($"\t|{preference.IsProfessional} |{preference.Price} |{preference.Manufacturer} |{preference.KindsOfSports} |\n");
                                table.Append("\t+--+--+--+--+\n");
                          }
                          table.Append("\tПреобразованные оценки экспертов\n");
                          table.Append("\t+--+--+--+--+\n");
                          table.Append("\t|A1|A2|A3|A4|\n");
                          table.Append("\t+--+--+--+--+\n");
                          foreach (var preference in Preferences) {
                              table.Append($"\t|{preference.IsProfessionalReverse} |{preference.PriceReverse} |{preference.ManufacturerReverse} |{preference.KindsOfSportsReverse} |\n");
                              table.Append("\t+--+--+--+--+\n");
                          }
                          table.Append("\tCуммы альтернатив\n");
                          table.Append("\t+--+--+--+--+--+\n");
                          table.Append("\t|C |C1|C2|C3|C4|\n");
                          table.Append("\t+--+--+--+--+\n");
                          foreach (var preference in PreferenceSums) {
                              table.Append($"\t|{preference.IsProfessionalSum} |{preference.PriceSum} |{preference.ManufacturerSum} |{preference.KindsOfSportsSum} |\n");
                              table.Append("\t+--+--+--+--+--+\n");
                          }
                          using (FileStream fstream = new FileStream("report.txt", FileMode.Create)) {
                              // преобразуем строку в байты
                              byte[] array = System.Text.Encoding.Default.GetBytes(table.ToString());
                              // запись массива байтов в файл
                              fstream.Write(array, 0, array.Length);
                              MessageBox.Show("Отчёт успешно сгенерирован!");
                          }
                      }));
            }
        }

        private RelayCommand editProductCommand;
        public RelayCommand EditProductCommand {
            get {
                return editProductCommand ??
                  (editProductCommand = new RelayCommand(
                      obj => {
                          ValidationRules validation = new ValidationRules();
                          validation.EditProductValidate(SelectedProduct.Name, SelectedProduct.Price, SelectedProduct.Description, SelectedProduct.Manufacturer);
                          if (!String.IsNullOrEmpty(validation.ValidationErors)) {
                              MessageBox.Show(validation.ValidationErors);
                              return;
                          }
                          SelectedProduct.ImagePath = ImagePathForEdit;
                          ServerOperations.SendData($"EditProduct\n{SelectedProduct.Id}\n{SelectedProduct.Name}\n{SelectedProduct.Price}\n{SelectedProduct.Description}\n{SelectedProduct.Manufacturer}\n{SelectedProduct.ImagePath}\n");
                              if (ServerOperations.ReceiveData() == "success")
                                  MessageBox.Show("Товар изменён");
                              else
                                  MessageBox.Show("Ошибка изменения!");
                      }));
            }
        }

        private RelayCommand uploadPhotoCommand;
        public RelayCommand UploadPhotoCommand {
            get {
                return uploadPhotoCommand ??
                  (uploadPhotoCommand = new RelayCommand(
                      obj => {
                          OpenFileDialog openFileDialog = new OpenFileDialog();
                          openFileDialog.Filter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg|All files (*.*)|*.*";
                          if (openFileDialog.ShowDialog() == true) {
                              SelectedProduct.ImagePath = openFileDialog.FileName;
                              ImagePath = openFileDialog.FileName;
                              byte[] imageArray = File.ReadAllBytes(openFileDialog.FileName);
                              if (imageArray.Length > 100000) {
                                  MessageBox.Show("Изображение слишком большое");
                                  ImagePath = "Assets/no_foto.jpg";
                                  return;
                              }
                          }
                          else return;
                      }));
            }
        }

        private RelayCommand editPhotoCommand;
        public RelayCommand EditPhotoCommand {
            get {
                return editPhotoCommand ??
                  (editPhotoCommand = new RelayCommand(
                      obj => {
                          OpenFileDialog openFileDialog = new OpenFileDialog();
                          openFileDialog.Filter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg|All files (*.*)|*.*";
                          openFileDialog.InitialDirectory = GetFullPathInDocuments();
                          if (openFileDialog.ShowDialog() == true) {
                              ImagePathForEdit = openFileDialog.FileName;
                              byte[] imageArray = File.ReadAllBytes(openFileDialog.FileName);
                              if (imageArray.Length > 100000) {
                                  MessageBox.Show("Изображение слишком большое");
                                  ImagePathForEdit = "Assets/no_foto.jpg";
                                  return;
                              }
                          }
                          else return;
                      }));
            }
        }

        private RelayCommand deleteProductFromCartCommand;
        public RelayCommand DeleteProductFromCartCommand {
            get {
                return deleteProductFromCartCommand ??
                  (deleteProductFromCartCommand = new RelayCommand(
                      obj => {
                          SelectedCart.Products.Remove(SelectedCart.Products.Where(p => p.Id == (int)obj).Single());
                          ServerOperations.SendData($"DeleteProductFromCart\n{CurrentUser.Email}\n{obj}\n");
                          if (ServerOperations.ReceiveData() == "success") {
                              MessageBox.Show("Товар удалён из корзины!");
                          }
                          else
                              MessageBox.Show("Ошибка удаления");
                      }));
            }
        }



        public void DeleteProduct(Product product) {
            Products.Remove(product);
            ServerOperations.SendData($"DeleteProduct\n{product.Id}\n");
            if (ServerOperations.ReceiveData() == "success") {
                MessageBox.Show("Товар удалён");
            }
            else
                MessageBox.Show("Ошибка удаления");
        }
    }
}
