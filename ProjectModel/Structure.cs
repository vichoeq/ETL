using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectModel
{

    /// <summary>
    /// Representa un elemento específico de la construcción
    /// </summary>
    [Serializable]
    class Structure
    {
        // TODO private 3D model ?
        
        /// <summary>
        /// Material del que está compuesto este elemento de la construcción
        /// </summary>
        public Material Material { get => material; }
        private Material material;

        /// <summary>
        /// Zona en la que está este elemento de la construcción
        /// </summary>
        public int Zone { get => zone; }
        private int zone;

        /// <summary>
        /// Tarea a la que corresponde este elemento de la construcción
        /// </summary>
        public Task Task { get => task; }
        private Task task;

        /// <summary>
        /// Familia de elementos de construcción a la que corresponde
        /// </summary>
        public string Family { get => family; }
        private string family;

    }
}
