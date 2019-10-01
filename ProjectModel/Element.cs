using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectModel
{
    /// <summary>
    /// Representa un elemento específico de la construcción
    /// </summary>
    [Serializable]
    public class Element
    {
        // TODO private 3D model
        
        /// <summary>
        /// Material del que está compuesto este elemento de la construcción
        /// </summary>
        public ElementType ElementType { get => elementType; }
        private ElementType elementType;

        /// <summary>
        /// Zona en la que está este elemento de la construcción
        /// </summary>
        public Zone Zone { get => zone; }
        private Zone zone;

        /// <summary>
        /// Piso en el que está este elemento de la construcción
        /// </summary>
        public Level Level { get => level; }
        private Level level;

        /// <summary>
        /// Tareas asociada a este elemento de la construcción
        /// </summary>
        public List<Task> Tasks { get => tasks; }
        private List<Task> tasks;

        /// <summary>
        /// Inicializa un elemento de la construcción con los parámetros dados
        /// </summary>
        /// <param name="elementType">Tipo del que está compuesto este elemento de la construcción</param>
        /// <param name="zone">Zona en la que está este elemento de la construcción</param>
        /// <param name="level">Piso en el que está este elemento de la construcción</param>
        /// <param name="tasks">Tareas asociada a este elemento de la construcción</param>
        public Element(ElementType elementType, Zone zone, Level level, List<Task> tasks)
        {
            this.elementType = elementType;
            this.zone = zone;
            this.level = level;
            this.tasks = tasks;
        }
    }
}
