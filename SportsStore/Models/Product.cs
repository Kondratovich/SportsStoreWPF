using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsStore.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string ImagePath{ get; set; }
        public string DateAdded { get; set; }

        public string FullImagePath { get {
                var baseFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                var appStorageFolder = Path.Combine(baseFolder, "SportsStore");
                var fullPath = Path.Combine(appStorageFolder, $"Images\\{ImagePath}");
                return fullPath;
            } 
        }

        public Product(int id, string name, double price, string description, string imagePath) {
            Id = id;
            Name = name;
            Price = price;
            Description = description;
            ImagePath = imagePath;
        }

        public Product(int id, string name, double price, string description, string imagePath, string dateAdded) {
            Id = id;
            Name = name;
            Price = price;
            Description = description;
            ImagePath = imagePath;
            DateAdded = dateAdded;
        }

        public Product() {
            ImagePath = "Assets/no_foto.jpg";
        }
    }
}
