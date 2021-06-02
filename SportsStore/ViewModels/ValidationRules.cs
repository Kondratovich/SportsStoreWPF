using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace SportsStore.ViewModels {
    class ValidationRules {
        public string ValidationErors { get; set; }

        public void RegistrationFormValidate(string email ,string name, string surname, string password) {
            string nameSurnamePattern = @"^([А-Я]{1}[а-яё]{1,23}|[A-Z]{1}[a-z]{1,23})$";
            string emailPattern = @"^([a-z0-9_-]+\.)*[a-z0-9_-]+@[a-z0-9_-]+(\.[a-z0-9_-]+)*\.[a-z]{2,6}$";
            if (String.IsNullOrEmpty(name) || name.Length < 1 || name.Length > 10) {
                ValidationErors += "- Размер имени должен быть от 1 до 10 символов.\n";   
            }
            else if(!Regex.IsMatch(name, nameSurnamePattern)) {
                ValidationErors += "- Некорректное имя\n";   
            }
            if (String.IsNullOrEmpty(surname) || name.Length < 1 || name.Length > 15) {
                ValidationErors += "- Размер фамилии должен быть от 1 до 15 символов.\n";
            }
            else if(!Regex.IsMatch(surname, nameSurnamePattern)) {
                ValidationErors += "- Некорректная фамилия\n";
            }
            if (!Regex.IsMatch(email, emailPattern)) {
                ValidationErors += "- Некорректный Email. Пример: «nick@mail.com»\n";
            }
            if (String.IsNullOrEmpty(password) || password.Length < 1 || password.Length > 10) {
                ValidationErors += "- Размер Пароля быть от 1 до 15 символов.\n";
            }
        }

        public void LoginFormValidate(string email, string password) {
            if (String.IsNullOrEmpty(email) || email.Length < 1 || email.Length > 30) {
                ValidationErors += "- Размер логина быть от 1 до 30 символов.\n";
            }
            if (String.IsNullOrEmpty(password) || password.Length < 1 || password.Length > 15) {
                ValidationErors += "- Размер Пароля быть от 1 до 15 символов.\n";
            }
        }

        public void AddProductValidate(string name, double price, string description, string manuf) {
            if (String.IsNullOrEmpty(name) || name.Length < 1 || name.Length > 20) {
                ValidationErors += "- Размер названия товара должен быть от 1 до 20 символов.\n";
            }
            else if(price <= 0 || price > 10000) {
                ValidationErors += "- Цена должна быть больше 0 и меньше 10000 \n";
            }
            if (String.IsNullOrEmpty(description) || description.Length < 1 || description.Length > 20) {
                ValidationErors += "- Размер описания товара должен быть от 1 до 20 символов.\n";
            }
            if (String.IsNullOrEmpty(manuf) || description.Length < 1 || description.Length > 20) {
                ValidationErors += "- Размер описания товара должен быть от 1 до 20 символов.\n";
            }
        }

        public void EditProductValidate(string name, double price, string description, string manuf) {
            if (String.IsNullOrEmpty(name) || name.Length < 1 || name.Length > 20) {
                ValidationErors += "- Размер названия товара должен быть от 1 до 20 символов.\n";
            }
            if (price <= 0 || price > 10000) {
                ValidationErors += "- Цена должна быть больше 0 и меньше 10000 \n";
            }
            if (String.IsNullOrEmpty(description) || description.Length < 1 || description.Length > 80) {
                ValidationErors += "- Размер описания товара должен быть от 1 до 80 символов.\n";
            }
            if (String.IsNullOrEmpty(manuf) || manuf.Length < 1 || manuf.Length > 20) {
                ValidationErors += "- Размер производителя товара должен быть от 1 до 20 символов.\n";
            }
        }

        public void EditPreferenceForm(byte isProf, byte price, byte kindsOfSports, byte manuf) {
            if (price > 4 || price < 0 || kindsOfSports > 4 || kindsOfSports < 0 || isProf > 4 || isProf < 0 || manuf > 4 || manuf < 0) {
                ValidationErors += "- Размер оценки альтернативы должен быть от 0 до 4\n";
            }
        }
    }
}
