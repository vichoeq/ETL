using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectModel
{
    enum Phase
    {
        OBRA_GRUESA,
        TERMINACIONES,
        INSTALACIONES
    };
    
    class Project
    {
        private Dictionary<(Phase, int), List<Structure>> structures;

        /// <summary>
        /// Inicializa un proyecto vacío
        /// </summary>
        public Project()
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

            List<Structure> structure_list;

            if(!structures.TryGetValue((phase, zone), out structure_list))
            {
                structure_list = new List<Structure>();
                structures.Add((phase, zone), structure_list);
            }

            structure_list.Add(structure);
        }        
    }
}
