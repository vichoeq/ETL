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
            string pathExcel = @"C:\Users\Vicho\Documents\PUC\2019-2\Capstone\ETL\data\Estimación de costos.xlsm";
            string pathProject = @"C:\Users\Vicho\Documents\PUC\2019-2\Capstone\ETL\data\Modelo de proceso.mpp";
            ExcelExtractor excelExtractor = new ExcelExtractor(pathExcel);
            MicrosoftProjectFile projectFileExtractor = new MicrosoftProjectFile(pathProject);
            List<Material> materials = excelExtractor.Extract();
            Dictionary<(string, Phase, int), List<Task>> tasks = projectFileExtractor.Extract();
            Console.WriteLine("Materials:");
            foreach (Material material in materials)
            {
                Console.WriteLine(material.Name);
            }
            Console.WriteLine("Task:");
            foreach (KeyValuePair<(string, Phase, int), List<Task>> entry in tasks)
            {
                Console.WriteLine(entry.Key.Item1 + "/" + entry.Key.Item2.ToString() + "/" + entry.Key.Item3.ToString() + ":" );
                foreach (Task task in entry.Value)
                {
                    Console.WriteLine(task.Name);
                }
                // do something with entry.Value or entry.Key
            }

            var key = Console.ReadKey();


        }
    }
}
