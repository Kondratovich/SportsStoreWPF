using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace SportsStore.Models {
    public class Cart {
        public int Id { get; set; }
        public ObservableCollection<Product> Products { get; set; } = new ObservableCollection<Product>();
        public Cart(int id, ObservableCollection<Product> products) {
            Id = id;
            Products = products;
        }
        public Cart() {

        }
    }
}
