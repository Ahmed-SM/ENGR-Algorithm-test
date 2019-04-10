using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Matrix_factorization
{
    public class Item
    {
        public String Name { get; set; }
        public Feature Feature { get; set; }
        public Item MostSimiler { get; set; }
        public double value{ get; set; }
        public Item()
        {
            Feature = new Feature();
        }
    }
}
