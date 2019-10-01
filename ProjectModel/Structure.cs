using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectModel
{
    /// <summary>
    /// Representa un elemento específico de la construcción
    /// </summary>
    [Serializable]
    public class Structure
    {
        // TODO private 3D model
        
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
        /// Tarea asociada a este elemento de la construcción
        /// </summary>
        public Task Task { get => task; }
        private Task task;

        /// <summary>
        /// Inicializa un elemento de la construcción con los parámetros dados
        /// </summary>
        /// <param name="material">Material del que está compuesto este elemento de la construcción</param>
        /// <param name="zone">Zona en la que está este elemento de la construcción</param>
        /// <param name="task">Tarea asociada a este elemento de la construcción</param>
        public Structure(Material material, int zone, Task task)
        {
            this.material = material;
            this.zone = zone;
            this.task = task;
        }
    }
}
