using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectModel
{
    /// <summary>
    /// Representa una fase en el proyecto de la construcción
    /// </summary>
    public enum Phase
    {
        OBRA_GRUESA,
        TERMINACIONES,
        INSTALACIONES
    };
    
    /// <summary>
    /// Asocia las fases y zonas con las componentes estructurales de la construcción
    /// </summary>
    public class Building
    {
        private Dictionary<(Phase, int), List<Structure>> structures;

        /// <summary>
        /// Inicializa un proyecto de construcción vacío
        /// </summary>
        public Building()
        {
            structures = new Dictionary<(Phase, int), List<Structure>>();
        }

        /// <summary>
        /// Agrega un elemento estructural de la construcción al proyecto
        /// </summary>
        /// <param name="structure">Elemento de la estructura del proyecto de construcción</param>
        public void AddStructure(Structure structure)
        {
            Phase phase = structure.Task.Phase;
            int zone = structure.Task.Zone;

            if (!structures.TryGetValue((phase, zone), out List<Structure> structure_list))
            {
                structure_list = new List<Structure>();
                structures.Add((phase, zone), structure_list);
            }

            structure_list.Add(structure);
        }
    }
}
