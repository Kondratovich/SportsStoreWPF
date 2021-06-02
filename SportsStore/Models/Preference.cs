using System;
using System.Collections.Generic;
using System.Text;

namespace SportsStore.Models {
    public class Preference {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string UserEmail { get; set; }
        public byte IsProfessional { get; set; }
        public byte Price { get; set; }
        public byte KindsOfSports { get; set; }
        public byte Manufacturer { get; set; }

        public byte IsProfessionalReverse { get { return (byte)(4 - IsProfessional); } }
        public byte PriceReverse { get { return (byte)(4 - Price); } }
        public byte KindsOfSportsReverse { get { return (byte)(4 - KindsOfSports); } }
        public byte ManufacturerReverse { get { return (byte)(4 - Manufacturer); } }
        public Preference() {

        }

        public Preference(int userId, int userID, byte isProffesional, byte price, byte kindsOfSports, byte manufacturer) {
            Id = userId;
            UserId = userID;
            IsProfessional = isProffesional;
            Price = price;
            KindsOfSports = kindsOfSports;
            Manufacturer = manufacturer;
        }
        public Preference(byte isProffesional, byte price, byte kindsOfSports, byte manufacturer, string email) {
            IsProfessional = isProffesional;
            Price = price;
            KindsOfSports = kindsOfSports;
            Manufacturer = manufacturer;
            UserEmail = email;
        }
    }
}
