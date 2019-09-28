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

        public Project()
        {
            structures = new Dictionary<(Phase, int), List<Structure>>();
        }

        public void AddStructure(Structure structure)
        {
            Phase phase = Phase.OBRA_GRUESA;
            int zone = 0;

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
