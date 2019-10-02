using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace ProjectModel
{    
    /// <summary>
    /// Asocia las fases y zonas con las componentes estructurales de la construcción
    /// </summary>
    [Serializable]
    public class Building
    {
        private Dictionary<(Phase, Zone, Level), List<Element>> elements;

        private Dictionary<(string, Phase), List<ElementType>> elementTypes;

        private Dictionary<(string, Phase, Zone), List<Task>> tasks;

        /// <summary>
        /// Inicializa un proyecto de construcción vacío
        /// </summary>
        public Building()
        {
            elements = new Dictionary<(Phase, Zone, Level), List<Element>>();
        }

        /// <summary>
        /// Agrega esta lista de tareas al modelo del edificio
        /// </summary>
        /// <param name="tasks">Listas de tareas, agrupadas por nombre, etapa y zona</param>
        public void AddTasks(Dictionary<(string, Phase, Zone), List<Task>> tasks)
        {
            this.tasks = tasks;
        }

        /// <summary>
        /// Agrega esta lista de tareas al modelo del edificio
        /// </summary>
        /// <param name="elementTypes">Listas de tipos de elemento, agrupadas por nombre y etapa</param>
        public void AddElementTypes(Dictionary<(string, Phase), List<ElementType>> elementTypes)
        {
            this.elementTypes = elementTypes;
        }

        /// <summary>
        /// Agrega un elemento estructural de la construcción al proyecto
        /// </summary>
        /// <param name="element">Elemento de la estructura del proyecto de construcción</param>
        public void AddElement(Element element)
        {
            // TODO mejor asignar aquí las tareas y los tipos de familia

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

        /// <summary>
        /// Guarda el objeto como un binario
        /// </summary>
        /// <param name="path">Ruta donde deberá guardarse el objeto</param>
        public void Serialize(string path)
        {
            using(FileStream fs = new FileStream(path, FileMode.Create))
            {
                BinaryFormatter bf = new BinaryFormatter();

                bf.Serialize(fs, this);
            }
        }

        /// <summary>
        /// Reconstruye un edificio a partir de su serialización
        /// </summary>
        /// <param name="path">Ruta del archivo previamente serializado</param>
        /// <returns>El edificio reconstruido a partir de su serialización</returns>
        public static (Building, string) Deserialize(string path)
        {
            Building building = null;

            using (FileStream fs = new FileStream(path, FileMode.Open))
            {
                BinaryFormatter bf = new BinaryFormatter();

                try
                {
                    building = (Building) bf.Deserialize(fs);
                }
                catch(Exception e)
                {
                    return (null, e.ToString());
                }
            }

            return (building, "Archivo decodificado correctamente");
        }
    }
}
