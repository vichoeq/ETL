using System;
using System.Collections.Generic;
using net.sf.mpxj.reader;
using net.sf.mpxj.mpp;
using MPXJ = net.sf.mpxj;
using ProjectModel;

namespace Extractors
{
    /// <summary>
    /// Clase encargada de extraer la información de tareas de un archivo .MPP
    /// </summary>
    public class MicrosoftProjectExtractor
    {
        private MPXJ.ProjectFile mpp;

        /// <summary>
        /// Abre un archivo de proyecto de Microsoft Project
        /// </summary>
        /// <param name="path">Ruta al archivo .MPP</param>
        public MicrosoftProjectExtractor(string path)
        {
            AbstractProjectReader reader = new MPPReader();

            mpp = reader.Read(path);
        }

        /// <summary>
        /// Extrae todas las tareas del archivo, asociandolas a su familia, fase y zona
        /// </summary>
        /// <returns>Un diccionario con la lista de tareas correspondientes a una misma familia, fase y zona</returns>
        public Dictionary<(string, Phase, int), List<Task>> Extract()
        {
            Dictionary<(string, Phase, int), List<Task>> task_info = new Dictionary<(string, Phase, int), List<Task>>();

            MPXJ.TaskContainer tasks = mpp.Tasks;

            foreach (MPXJ.Task t in tasks)
            {
                if(t.Type.ToString() == "FIXED_UNITS") 
                {                    
                    DateTime taskStart = FromJavaToDateTime(t.Start.getTime());              
                    DateTime taskEnd = FromJavaToDateTime(t.Finish.getTime());                   

                    // En los archivos de ejemplo las tareas eran todas de la forma "<nombre>, <tipo familia>, <zona>"
                    string[] taskFields = t.Name.Split(new string[] { ", " }, StringSplitOptions.None);

                    string taskName = taskFields[0];
                    string taskFamily = taskFields[1];
                    string taskZone = taskFields[2];

                    // En los archivos de ejemplo una tarea puede estar en más de una zona. En ese caso aparece como "zona 1,2"
                    
                    string taskPhase;
                    // TODO es esto importante? 
                    string taskNivel;           

                    if (t.ParentTask.Name.ToLower() == taskZone) 
                    {
                        taskPhase = t.ParentTask.ParentTask.Name;
                        taskNivel = t.ParentTask.ParentTask.ParentTask.Name;
                    }
                    else 
                    {
                        taskPhase = t.ParentTask.Name;
                        taskNivel = t.ParentTask.ParentTask.Name;
                    }

                    taskZone = taskZone.Replace("zona", "").Trim();
                    string[] zones = taskZone.Split(',');
                    // En caso de que una tarea abarque más de una zona, se convierte en múltiples tareas, una para cada zona.
                    foreach (string z in zones)
                    {
                        int zone = int.Parse(z);
                        Phase phase = PhaseFromString(taskPhase);
                        Task newTask = new Task(phase, zone, taskName, taskStart, taskEnd);

                        // Si no hay ninguna lista de tareas para esta famila, fase y zona, creamos una
                        if (!task_info.TryGetValue((taskFamily, phase, zone), out List<Task> taskList))
                        {
                            taskList = new List<Task>();
                            task_info.Add((taskFamily, phase, zone), taskList);
                        }
                        taskList.Add(newTask);
                    }
                }
            }

            return task_info;
        }
        
        /// <summary>
        /// Obtiene el enum Phase a partir del string que aparece en el MPP
        /// </summary>
        /// <param name="phase">Fase del proyecto como string</param>
        /// <returns>Fase del proyecto como enum</returns>
        private static Phase PhaseFromString(string phase)
        {
            switch(phase)
            {
                case "Obra gruesa": 
                    return Phase.OBRA_GRUESA;
                case "Terminaciones":
                    return Phase.TERMINACIONES;
                case "Instalaciones":
                    return Phase.INSTALACIONES;
                default:
                    throw new Exception("Fase inválida: \"" + phase + "\"");
            }
        }

        /// <summary>
        /// Convierte una fecha en formato Java a una fecha en formato C#
        /// </summary>
        /// <param name="miliseconds">Milisegundos desde 1970</param>
        /// <returns>La fecha en formato C#</returns>
        private DateTime FromJavaToDateTime(long miliseconds)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return epoch.AddMilliseconds(miliseconds);
        }

    }
}

