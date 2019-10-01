using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Extractors;
using ProjectModel;

namespace testing
{
    class Program
    {
        static void Main(string[] args)
        {
            string pathExcel = @"C:\Users\Usuario\Documents\archibos proyecto capstone\costos.xlsm";
            string pathProject = @"C:\Users\Usuario\Documents\archibos proyecto capstone\Modelo de proceso.mpp";
            ExcelExtractor excelExtractor = new ExcelExtractor(pathExcel);
            MicrosoftProjectFile projectFileExtractor = new MicrosoftProjectFile(pathProject);
            DateTime time = DateTime.Now;
            Dictionary<(string, Phase), List<Material>> materials = excelExtractor.Extract();
            Console.WriteLine(DateTime.Now - time);
            Dictionary<(string, Phase, int), List<Task>> tasks = projectFileExtractor.Extract();
            Console.WriteLine("Materials:");
            foreach (KeyValuePair<(string, Phase), List<Material>> entry in materials)
            {
                Console.WriteLine(entry.Key.Item1 + "/" + entry.Key.Item2.ToString() + ":");
                foreach (Material material in entry.Value)
                {
                    Console.WriteLine(material.Name);
                }
            }
            Console.WriteLine("Task:");
            foreach (KeyValuePair<(string, Phase, int), List<Task>> entry in tasks)
            {
                Console.WriteLine(entry.Key.Item1 + "/" + entry.Key.Item2.ToString() + "/" + entry.Key.Item3.ToString() + ":" );
                foreach (Task task in entry.Value)
                {
                    Console.WriteLine(task.Name);
                }
            }

            var key = Console.ReadKey();


        }
    }
}
