using System;
using System.Collections.Generic;
using System.Text;

namespace SportsStore.Models {
    public class PreferenceSums {
        public int Sum { get; set; }
        public int MaterialsSum { get; set; }
        public int PriceSum { get; set; }
        public int KindsOfSportsSum { get; set; }
        public int GuaranteeSum { get; set; }
        public int EcoFriendlySum { get; set; }
        public float MaterialsWeight { get; set; }
        public float PriceWeight { get; set; }
        public float KindsOfSportsWeight { get; set; }
        public float GuaranteeWeight { get; set; }
        public float EcoFriendlyWeight { get; set; }

        public PreferenceSums(int a, int b,int c, int d, int f, int g) {
            MaterialsSum = a;
            PriceSum = b;
            KindsOfSportsSum = c;
            GuaranteeSum = d;
            EcoFriendlySum = f;
            Sum = g;
            MaterialsWeight = MaterialsSum / (float)Sum;
            PriceWeight = PriceSum / (float)Sum;
            KindsOfSportsWeight = KindsOfSportsSum / (float)Sum;
            GuaranteeWeight = GuaranteeSum / (float)Sum;
            EcoFriendlyWeight = EcoFriendlySum / (float)Sum;
        }

    }
}
