using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;
using ProjectModel;

namespace Extractors
{
    /// <summary>
    /// Clase encargada de extraer y paresar la información de materiales de un archivo .xlsm
    /// </summary>
    public class ExcelExtractor
    {
        public string Path;
        public Excel.Application xlApp;
        public Excel.Range XlRange;
        public Excel._Worksheet xlWorksheet;
        public Excel.Workbook xlWorkbook;
        int rowCount;
        int colCount;

        /// <summary>
        /// Abre un archivo de Xlrange de Microsoft Excel
        /// </summary>
        /// <param name="path">Ruta al archivo .xlsm</param>
        public ExcelExtractor(string path)
        {
            Path = path;
            xlApp = new Excel.Application();
            xlWorkbook = xlApp.Workbooks.Open(path);
            xlWorksheet = xlWorkbook.Sheets[1];
            XlRange = xlWorksheet.UsedRange;
            rowCount = XlRange.Rows.Count;
            colCount = XlRange.Columns.Count;
        }

        /// <summary>
        /// Extrae todos los materiales del archivo, asociandolas a su familia, fase 
        /// </summary>
        /// <returns>Un diccionario con la lista de materiales correspondientes a una misma familia, fase</returns>
        public Dictionary<(string, Phase), List<Material>> Extract()
        {
            //List<Material> rows_info = new List<Material>();
            Dictionary<(string, Phase), List<Material>> materialsInfo = new Dictionary<(string, Phase), List<Material>>();
            Phase currentPhase = Phase.OBRA_GRUESA;
            for (int i = 4; i <= rowCount; i++)
            {
                
                if (XlRange.Cells[i, 1] != null && XlRange.Cells[i, 1].Value2 != null && XlRange.Cells[i, 2].Value2 == null) 
                {
                    foreach(string phase in Enum.GetNames(typeof(Phase)))
                    {

                        if (phase == XlRange.Cells[i, 1].Value2.ToString().Replace(" ", "_"))
                        {
                            //Console.WriteLine("Phase:" + phase );
                            currentPhase = PhaseFromString(XlRange.Cells[i, 1].Value2.ToString());
                        }
                    }
                }
                
                
                if (XlRange.Cells[i, 2] != null && XlRange.Cells[i, 2].Value2 != null)
                {
                    //Console.WriteLine("row:" + i.ToString());
                    
                    //Material newMaterial = new Material(XlRange.Cells[i,2].Value2.ToString(), Convert.ToInt32(XlRange.Cells[i, 5].Value2) , XlRange.Cells[i,3].Value2.ToString(), XlRange.Cells[i, 1].Value2.ToString());
                    Material newMaterial = new Material(XlRange.Cells[i, 2].Value2, Convert.ToInt32(XlRange.Cells[i, 5].Value2), XlRange.Cells[i, 3].Value2, XlRange.Cells[i, 1].Value2);

                    if (!materialsInfo.TryGetValue((newMaterial.Family, currentPhase), out List<Material> materialList))
                    {
                        materialList = new List<Material>();
                        materialsInfo.Add((newMaterial.Family, currentPhase), materialList);
                    }
                    materialList.Add(newMaterial);

                }                
            }
            return materialsInfo;


        }
        public void Close()
        {
            //cleanup
            GC.Collect();
            GC.WaitForPendingFinalizers();
            //rule of thumb for releasing com objects:
            //  never use two dots, all COM objects must be referenced and released individually
            //  ex: [somthing].[something].[something] is bad
            //release com objects to fully kill excel process from running in the background
            Marshal.ReleaseComObject(XlRange);
            Marshal.ReleaseComObject(xlWorksheet);
            //close and release
            xlWorkbook.Close();
            Marshal.ReleaseComObject(xlWorkbook);
            //quit and release
            xlApp.Quit();
            Marshal.ReleaseComObject(xlApp);
        }
        private static Phase PhaseFromString(string phase)
        {
            switch (phase)
            {
                case "OBRA GRUESA":
                    return Phase.OBRA_GRUESA;
                case "TERMINACIONES":
                    return Phase.TERMINACIONES;
                case "INSTALACIONES":
                    return Phase.INSTALACIONES;
                default:
                    throw new Exception("Fase inválida: \"" + phase + "\"");
            }
        }
    }

}
