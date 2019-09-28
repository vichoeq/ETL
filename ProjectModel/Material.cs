using System;

namespace ProjectModel
{
    public class Material
    {
        private string name;
        private int price;
        private string units;


        public string Name { get => name; }
        public int Price { get => price; }
        public string Units { get => units; }

        public Material(string name, int price, string units)
        {
            this.name = name;
            this.price = price;
            this.units = units;
        }
    }
}
