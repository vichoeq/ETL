using System;

namespace ProjectModel
{
    /// <summary>
    /// Representa un material de construcción
    /// </summary>
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
        /// Familia de elementos de construcción a la que corresponde (pared, ventana, tubería, etc)
        /// </summary>        
        public string Family { get => family; }
        private string family;

        /// <summary>
        /// Inicializa un nuevo material
        /// </summary>
        /// <param name="name">Nombre del material</param>
        /// <param name="price">Precio por unidad del material</param>
        /// <param name="units">Unidades en las que se mide la cantidad de material (Kg, m3, etc)</param>
        /// <param name="family">Familia de elementos de construcción a la que corresponde (pared, ventana, tubería, etc)</param>
        public Material(string name, int price, string units, string family)
        {
            this.name = name;
            this.price = price;
            this.units = units;
            this.family = family;
        }
    }
}
