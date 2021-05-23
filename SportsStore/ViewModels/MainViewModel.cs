using Microsoft.Win32;
using SportsStore.Models;
using SportsStore.Server;
using SportsStore.ViewModels.Commands;
using System;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Linq;
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
                _imagePathForEdit = GetFullPathInDocuments(SelectedProduct.ImagePath);
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

        string GetFullPathInDocuments(string imageFileName) {
            var baseFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var appStorageFolder = Path.Combine(baseFolder, "SportsStore");
            var fullPath = Path.Combine(appStorageFolder, $"Images\\{imageFileName}");
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
            string imagePath = productData.Substring(0, productData.IndexOf("\n"));
            productData = productData.Remove(0, productData.IndexOf("\n") + 1);
            StoreInfo.BestProduct = new Product(id, name, price, description, imagePath);
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
                string imagePath = productData.Substring(0, productData.IndexOf("\n"));
                productData = productData.Remove(0, productData.IndexOf("\n") + 1);
                _products.Add(new Product(id, name, price, description, imagePath));
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
            int MaterialsSum = 0;
            int PriceSum = 0;
            int KindsOfSportsSum = 0;
            int GuaranteeSum = 0;
            int EcoFriendlySum = 0;
            int Sum = 0;
            do {
                byte materials = Convert.ToByte(data.Substring(0, data.IndexOf("\n")));
                data = data.Remove(0, data.IndexOf("\n") + 1);
                byte price = Convert.ToByte(data.Substring(0, data.IndexOf("\n")));
                data = data.Remove(0, data.IndexOf("\n") + 1);
                byte sport = Convert.ToByte(data.Substring(0, data.IndexOf("\n")));
                data = data.Remove(0, data.IndexOf("\n") + 1);
                byte guarantee = Convert.ToByte(data.Substring(0, data.IndexOf("\n")));
                data = data.Remove(0, data.IndexOf("\n") + 1);
                byte eco = Convert.ToByte(data.Substring(0, data.IndexOf("\n")));
                data = data.Remove(0, data.IndexOf("\n") + 1);
                string email = data.Substring(0, data.IndexOf("\n"));
                data = data.Remove(0, data.IndexOf("\n") + 1);
                MaterialsSum += (5 - materials);
                PriceSum += (5 - price);
                KindsOfSportsSum += (5 - sport);
                GuaranteeSum += (5 - guarantee);
                EcoFriendlySum += (5 - eco);
                _preferences.Add(new Preference(materials,price,sport,guarantee,eco,email));
                ServerOperations.SendData("next");
                data = ServerOperations.ReceiveData();
            } while (data != "end");
             Sum = (MaterialsSum + PriceSum + KindsOfSportsSum + GuaranteeSum + EcoFriendlySum);
            _preferenceSums.Add(new PreferenceSums(MaterialsSum, PriceSum, KindsOfSportsSum, GuaranteeSum, EcoFriendlySum, Sum));
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
                string imagePath = productData.Substring(0, productData.IndexOf("\n"));
                productData = productData.Remove(0, productData.IndexOf("\n") + 1);
                string dateAdded = productData.Substring(0, productData.IndexOf("\n"));
                productData = productData.Remove(0, productData.IndexOf("\n") + 1); 
                _selectedCart.Products.Add(new Product(id, name, price, description, imagePath, dateAdded));
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
                          SelectedProduct.Price = Convert.ToDouble(parameters[1]);
                          SelectedProduct.Description = (string)parameters[2];
                          ServerOperations.SendData($"AddProduct\n{SelectedProduct.Name}\n{SelectedProduct.Price}\n{SelectedProduct.Description}\n");
                          if (ServerOperations.ReceiveData() == "success") {
                              if (SelectedProduct.ImagePath == "Assets/no_foto.jpg") {
                                  ServerOperations.SendData("default");
                              }
                              else {
                                  ServerOperations.SendData("notdefault");
                                  ServerOperations.ReceiveData();
                                  //byte[] imageArray = System.IO.File.ReadAllBytes(ImagePath);
                                  Image image = Image.FromFile(ImagePath);
                                  image.Save(GetFullPathInDocuments($"{SelectedProduct.Name}.jpg"));
                                  //ServerOperations.SendByteData(imageArray);
                              }
                              if (ServerOperations.ReceiveData() == "success")
                                  MessageBox.Show("Товар добавлен");
                              else
                                  MessageBox.Show("Ошибка загрузки изображения!");
                          }
                          else
                              MessageBox.Show("Ошибка добавления");
                      }));
            }
        }

        private RelayCommand addPreferenceForm;
        public RelayCommand AddPreferenceForm {
            get {
                return addPreferenceForm ??
                  (addPreferenceForm = new RelayCommand(
                      obj => {
                          ServerOperations.SendData($"AddPreferenceForm\n{Preference.Materials}\n{Preference.Price}\n{Preference.KindsOfSports}\n{Preference.Guarantee}\n{Preference.EcoFriendly}\n{CurrentUser.Email}\n");
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

        private RelayCommand editProductCommand;
        public RelayCommand EditProductCommand {
            get {
                return editProductCommand ??
                  (editProductCommand = new RelayCommand(
                      obj => {
                          ServerOperations.SendData($"EditProduct\n{SelectedProduct.Id}\n{SelectedProduct.Name}\n{SelectedProduct.Price}\n{SelectedProduct.Description}\n");
                          if (ServerOperations.ReceiveData() == "success") {
                              MessageBox.Show("Товар изменён");
                              this.OnClosingRequest();
                          }
                          else
                              MessageBox.Show("Ошибка изменения");
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
                              SelectedProduct.ImagePath = Path.GetFileName(openFileDialog.FileName);
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
            File.Delete(GetFullPathInDocuments($"{product.Name}.jpg"));
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
