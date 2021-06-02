using SportsStore.Models;
using SportsStore.Server;
using SportsStore.ViewModels.Commands;
using SportsStore.Views;
using System;
using System.Security.Cryptography;
using System.Windows;

namespace SportsStore.ViewModels {
    class LoginViewModel : BaseViewModel {

        private string _name;
        private string _surname;
        private string _email;
        private static string _password;

        public string Name {
            get {
                return _name;
            }
            set {
                _name = value;
                OnPropertyChanged("Name");

            }
        }
        public string Surname {
            get {
                return _surname;
            }
            set {
                _surname = value;
                OnPropertyChanged("Surname");

            }
        }
        public string Email {
            get {
                return _email;
            }
            set {
                _email = value;
                OnPropertyChanged("Email");
            }
        }
        public static string Password {
            get {
                return _password;
            }
            set {
                _password = value;
                //OnPropertyChanged("Password");
            }
        }

        private string GenerateHash(string pswrd, string salt) {
            var data = System.Text.Encoding.ASCII.GetBytes(pswrd + salt);
            data = System.Security.Cryptography.MD5.Create().ComputeHash(data);
            return Convert.ToBase64String(data);
        }

        private RelayCommand loginCommand;
        public RelayCommand LoginCommand {
            get {
                return loginCommand ??
                  (loginCommand = new RelayCommand(
                      obj => {
                          ValidationRules validation = new ValidationRules();
                          validation.LoginFormValidate(Email, Password);
                          if (!String.IsNullOrEmpty(validation.ValidationErors)) {
                              MessageBox.Show(validation.ValidationErors);
                              return;
                          }
                          ServerOperations.SendData($"Login\n{Email}");
                          string salt = ServerOperations.ReceiveData();
                          if (salt == "NoUsers") {
                              MessageBox.Show("Нет пользователя с таким логином");
                              return;
                          }

                          Password = GenerateHash(Password, salt);
                          ServerOperations.SendData($"{Password}");
                          string result = ServerOperations.ReceiveData();
                          if (result.Substring(0, result.IndexOf("\n")) == "success") {
                              result = result.Remove(0, result.IndexOf("\n") + 1);
                              Name = result.Substring(0, result.IndexOf("\n"));
                              result = result.Remove(0, result.IndexOf("\n") + 1);
                              Surname = result.Substring(0, result.IndexOf("\n"));
                              if(Email == "admin") {
                                  AdminMainWindow adminMainWindow = new AdminMainWindow(new User() { Name = Name, Surname = Surname, Email = Email });
                                  adminMainWindow.Show();
                              }
                              else {
                                MainWindow mainWindow = new MainWindow(new User() { Name = Name, Surname = Surname, Email = Email });
                                mainWindow.Show();
                              }
                              MessageBox.Show("Авторизация прошла успешно");
                              this.OnClosingRequest();
                          }
                          else
                              MessageBox.Show("Неправильный пароль!");
                      }));
            }
        }

        private RelayCommand registrationCommand;
        public RelayCommand RegistrationCommand {
            get {
                return registrationCommand ??
                  (registrationCommand = new RelayCommand(
                      obj => {
                          object[] parameters = obj as object[];
                          Name = (string)parameters[0];
                          Surname = (string)parameters[1];
                          Email = (string)parameters[2];
                          ValidationRules validation = new ValidationRules();
                          validation.RegistrationFormValidate(Email, Name, Surname, Password);
                          if (!String.IsNullOrEmpty(validation.ValidationErors)) {
                              MessageBox.Show(validation.ValidationErors);
                              return;
                          }
                          //Генерируем соль
                          RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
                          byte[] buffer = new byte[4];
                          rng.GetBytes(buffer);
                          string salt = BitConverter.ToString(buffer, 0);
                          Password = GenerateHash(Password, salt);
                          //отправить на сервер user
                          ServerOperations.SendData($"Registration\n{Name}\n{Surname}\n{Email}\n{Password}\n{salt}\n");
                          if (ServerOperations.ReceiveData() == "success") {
                              MainWindow mainWindow = new MainWindow(new User() { Name=Name, Surname=Surname, Email=Email});
                              mainWindow.Show();
                              MessageBox.Show("Пользователь зарегистрирован!");
                              this.OnClosingRequest();
                          }
                          else
                              MessageBox.Show("Ошибка регистрации");
                      }));
            }
        }
    }
}
