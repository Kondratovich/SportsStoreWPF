using System;
using System.Collections.Generic;
using System.Text;

namespace SportsStore.Models {
    public class Preference {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string UserEmail { get; set; }
        public byte Materials { get; set; }
        public byte Price { get; set; }
        public byte KindsOfSports { get; set; }
        public byte Guarantee { get; set; }
        public byte EcoFriendly { get; set; }

        public byte MaterialsReverse { get { return (byte)(5 - Materials); } }
        public byte PriceReverse { get { return (byte)(5 - Price); } }
        public byte KindsOfSportsReverse { get { return (byte)(5 - KindsOfSports); } }
        public byte GuaranteeReverse { get { return (byte)(5 - Guarantee); } }
        public byte EcoFriendlyReverse { get { return (byte)(5 - EcoFriendly); } }
        public Preference() {

        }

        public Preference(int userId, int userID, byte materials, byte price, byte kindsOfSports, byte guarantee, byte ecoFriendly) {
            Id = userId;
            UserId = userID;
            Materials = materials;
            Price = price;
            KindsOfSports = kindsOfSports;
            Guarantee = guarantee;
            EcoFriendly = ecoFriendly;
        }
        public Preference(byte materials, byte price, byte kindsOfSports, byte guarantee, byte ecoFriendly, string email) {
            Materials = materials;
            Price = price;
            KindsOfSports = kindsOfSports;
            Guarantee = guarantee;
            EcoFriendly = ecoFriendly;
            UserEmail = email;
        }
    }
}
