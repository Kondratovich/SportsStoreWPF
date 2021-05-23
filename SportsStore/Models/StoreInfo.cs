using System;
using System.Collections.Generic;
using System.Text;

namespace SportsStore.Models {
    public class StoreInfo {
        public int NumberOfUsers { get; set; }
        public int NumberOfProducts { get; set; }
        public Product BestProduct { get; set; }
    }
}
