using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectModel
{
    class Structure
    {
        // TODO private 3D model
        private Material material;
        private int zone;
        private Tarea tarea;
        private string family;

        public Material Material { get => material; }
        public int Zone { get => zone; }
        public Tarea Tarea { get => tarea; }
        public string Family { get => family; }
    }
}
