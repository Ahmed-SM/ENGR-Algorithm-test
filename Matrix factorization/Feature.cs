using System;
using System.Collections.Generic;
using System.Text;

namespace Matrix_factorization
{
    public class Feature
    {
        public int price { get; set; }
        public Dictionary<string, double> Preferences { get; set; }
        public Feature()
        {
            Preferences = new Dictionary<string, double>();
        }
    }
}
