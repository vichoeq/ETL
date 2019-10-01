using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectModel
{    
    /// <summary>
    /// Asocia las fases y zonas con las componentes estructurales de la construcción
    /// </summary>
    public class Building
    {
        private Dictionary<(Phase, Zone, Level), List<Element>> elements;

        /// <summary>
        /// Inicializa un proyecto de construcción vacío
        /// </summary>
        public Building()
        {
            elements = new Dictionary<(Phase, Zone, Level), List<Element>>();
        }

        /// <summary>
        /// Agrega un elemento estructural de la construcción al proyecto
        /// </summary>
        /// <param name="element">Elemento de la estructura del proyecto de construcción</param>
        public void AddElement(Element element)
        {
            foreach(Task t in element.Tasks)
            {
                Phase phase = t.Phase;
                Zone zone = t.Zone;
                Level level = element.Level;

                if (!elements.TryGetValue((phase, zone, level), out List<Element> element_list))
                {
                    element_list = new List<Element>();
                    elements.Add((phase, zone, level), element_list);
                }

                element_list.Add(element);
            }



            
        }
    }
}
