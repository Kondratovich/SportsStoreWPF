using System;
using System.Collections.Generic;
using System.Text;

namespace SportsStore.Models {
    public class PreferenceSums {
        public int Sum { get; set; }
        public int IsProfessionalSum { get; set; }
        public int PriceSum { get; set; }
        public int KindsOfSportsSum { get; set; }
        public int ManufacturerSum { get; set; }
        public float IsProfessionalWeight { get; set; }
        public float PriceWeight { get; set; }
        public float KindsOfSportsWeight { get; set; }
        public float ManufacturerWeight { get; set; }

        public PreferenceSums(int a, int b, int c, int d, int f) {
            IsProfessionalSum = a;
            PriceSum = b;
            KindsOfSportsSum = c;
            ManufacturerSum = d;
            Sum = f;
            IsProfessionalWeight = IsProfessionalSum / (float)Sum;
            PriceWeight = PriceSum / (float)Sum;
            KindsOfSportsWeight = KindsOfSportsSum / (float)Sum;
            ManufacturerWeight = ManufacturerSum / (float)Sum;
        }

    }
}
