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
    /// Clase encargada de extraer y parsear la información de elementTypes de un archivo .xlsm
    /// </summary>
    public class ExcelExtractor
    {
        private string Path;
        private Excel.Application xlApp;
        private Excel.Range XlRange;
        private Excel._Worksheet xlWorksheet;
        private Excel.Workbook xlWorkbook;
        private int rowCount;
        private int colCount;

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
        /// Extrae todos los elementTypees del archivo, asociandolas a su familia, fase 
        /// </summary>
        /// <returns>Un diccionario con la lista de elementTypees correspondientes a una misma familia, fase</returns>
        public Dictionary<(string, Phase), List<ElementType>> Extract()
        {
            //List<ElementType> rows_info = new List<ElementType>();
            Dictionary<(string, Phase), List<ElementType>> elementTypesInfo = new Dictionary<(string, Phase), List<ElementType>>();
            Phase currentPhase = "OBRA GRUESA";
            for (int row = 4; row <= rowCount; row++)
            {
                // TODO tiene que haber una forma mas directa de identificar esto
                if (XlRange.Cells[row, 1] != null && XlRange.Cells[row, 1].Value2 != null && XlRange.Cells[row, 2].Value2 == null) 
                {
                    // TODO Sin hardcodeo de etapas
                    foreach(string validPhase in new string[] {"OBRA GRUESA", "TERMINACIONES", "INSTALACIONES"})
                    {
                        string possiblePhase = XlRange.Cells[row, 1].Value2.ToString();
                        if (possiblePhase == validPhase)
                        {
                            currentPhase = possiblePhase;
                        }
                    }
                }
                
                
                // TODO tiene que haber una forma mas directa de identificar esto
                if (XlRange.Cells[row, 2] != null && XlRange.Cells[row, 2].Value2 != null)
                {
                    //Console.WriteLine("row:" + i.ToString());

                    //ElementType newElementType = new ElementType(XlRange.Cells[i,2].Value2.ToString(), Convert.ToInt32(XlRange.Cells[i, 5].Value2) , XlRange.Cells[i,3].Value2.ToString(), XlRange.Cells[i, 1].Value2.ToString());
                    ElementType newElementType = new ElementType(XlRange.Cells[row, 2].Value2, Convert.ToInt32(XlRange.Cells[row, 5].Value2), XlRange.Cells[row, 3].Value2, XlRange.Cells[row, 1].Value2);

                    if (!elementTypesInfo.TryGetValue((newElementType.Family, currentPhase), out List<ElementType> elementTypeList))
                    {
                        elementTypeList = new List<ElementType>();
                        elementTypesInfo.Add((newElementType.Family, currentPhase), elementTypeList);
                    }
                    elementTypeList.Add(newElementType);

                }                
            }
            return elementTypesInfo;


        }
        /// <summary>
        /// Cierra el excel y todos los recursos asociados
        /// </summary>
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
    }

}
