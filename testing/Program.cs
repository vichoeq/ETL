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
            string pathExcel = @"C:\Users\Pedro Bahamondes\Desktop\Capstone\Fuentes de información\Estimación de costos.xlsm";
            string pathProject = @"C:\Users\Pedro Bahamondes\Desktop\Capstone\Fuentes de información\Modelo de proceso.mpp";
            MicrosoftProjectExtractor projectFileExtractor = new MicrosoftProjectExtractor(pathProject);
            DateTime time = DateTime.Now;
            ExcelExtractor excelExtractor = new ExcelExtractor(pathExcel);
            Dictionary<(string, Phase), List<ElementType>> materials = excelExtractor.Extract();
            Console.WriteLine(DateTime.Now - time);
            Dictionary<(string, Phase, Zone), List<Task>> tasks = projectFileExtractor.Extract();
            Console.WriteLine("Materials:");
            foreach (KeyValuePair<(string, Phase), List<ElementType>> entry in materials)
            {
                Console.WriteLine(entry.Key.Item1 + "/" + entry.Key.Item2.ToString() + ":");
                foreach (ElementType material in entry.Value)
                {
                    Console.WriteLine(material.Name);
                }
            }
            Console.WriteLine("Task:");
            foreach (KeyValuePair<(string, Phase, Zone), List<Task>> entry in tasks)
            {
                Console.WriteLine(entry.Key.Item1 + "/" + entry.Key.Item2.ToString() + "/" + entry.Key.Item3.ToString() + ":" );
                foreach (Task task in entry.Value)
                {
                    Console.WriteLine(task.Name);
                }
            }

            var key = Console.ReadKey();
            excelExtractor.Close();


        }
    }
}
