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
        public bool isVisited { get; set; }
        public Dictionary<double, Item> ConnectedItem;
        public Item()
        {
            isVisited = false;
            Feature = new Feature();
            ConnectedItem = new Dictionary<double, Item>(){
                { 1.0, null },
                { 0.9, null },
                { 0.8, null },
                { 0.7, null },
                { 0.6, null },
                { 0.5, null },
                { 0.4, null },
                { 0.3, null },
                { 0.2, null },
                { 0.1, null },
                { 0.0, null },
            };
        }
    }
}
