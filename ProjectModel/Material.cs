using System;

namespace ProjectModel
{
    [Serializable]
    public class Material
    {
        /// <summary>
        /// Nombre del material
        /// </summary>
        public string Name { get => name; }
        private string name;

        /// <summary>
        /// Precio por unidad del material
        /// </summary>
        public int Price { get => price; }
        private int price;

        /// <summary>
        /// Unidades en las que se mide la cantidad de material (Kg, m3, etc)
        /// </summary>
        public string Units { get => units; }
        private string units;

        /// <summary>
        /// Inicializa un nuevo material
        /// </summary>
        /// <param name="name">Nombre del material</param>
        /// <param name="price">Precio por unidad del material</param>
        /// <param name="units">Unidades en las que se mide la cantidad de material (Kg, m3, etc)</param>
        public Material(string name, int price, string units)
        {
            this.name = name;
            this.price = price;
            this.units = units;
        }
    }
}
