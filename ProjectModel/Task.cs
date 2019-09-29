using System;

namespace ProjectModel
{
    [Serializable]
    public class Task
    {
        // TODO Requisitos?

        /// <summary>
        /// Nombre de la tarea
        /// </summary>
        public string Name { get => name; }
        private string name;

        /// <summary>
        /// Fase de la construcción en la que ocurre esta tarea
        /// </summary>
        public Phase Phase { get => phase; }
        private Phase phase;

        /// <summary>
        /// Zona en la que ocurre esta tarea
        /// </summary>
        public int Zone { get => zone; }
        private int zone;

        /// <summary>
        /// Fecha en la que debería comenzar la ejecución de la tarea
        /// </summary>
        public DateTime StartDate { get => startDate; }
        private DateTime startDate;

        /// <summary>
        /// Fecha en la que debería terminar la ejecución de la tarea
        /// </summary>
        public DateTime DueDate { get => dueDate; }
        private DateTime dueDate;
        
        /// <summary>
        /// Representa una tarea dentro de la construcción
        /// </summary>
        /// <param name="phase">Fase de la construcción en la que ocurre esta tarea</param>
        /// <param name="zone">Zona en la que ocurre esta tarea</param>
        /// <param name="name">Nombre de la tarea</param>
        /// <param name="startDate">Fecha en la que debería comenzar la ejecución de la tarea</param>
        /// <param name="dueDate">Fecha en la que debería terminar la ejecución de la tarea</param>
        public Task(Phase phase, int zone, string name, DateTime startDate, DateTime dueDate)
        {
            this.phase = phase;
            this.zone = zone;
            this.name = name;
            this.startDate = startDate;
            this.dueDate = dueDate;
        }

    }
}