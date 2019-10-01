using System;
using System.Collections.Generic;
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
        public Excel.Range XlRange;
        int rowCount;
        int colCount;

        /// <summary>
        /// Abre un archivo de Xlrange de Microsoft Excel
        /// </summary>
        /// <param name="path">Ruta al archivo .xlsm</param>
        public ExcelExtractor(string path)
        {
            Path = path;
            Excel.Application xlApp = new Excel.Application();
            Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(path);
            Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
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

            for (int i = 4; i <= rowCount; i++)
            {
                Phase currentPhase = Phase.OBRA_GRUESA;
                if (XlRange.Cells[i, 1] != null &&  XlRange.Cells[i, 1].Value2 != null) 
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
                    //the data is preprocess by a dictionary so the parser is agnostic to the order of the columns
                    Dictionary<string, string> row_info = new Dictionary<string, string>();
                    for (int j = 1; j <= colCount; j++)
                    {
                        if (XlRange.Cells[i, j] != null && XlRange.Cells[i, j].Value2 != null)
                        {
                            //Console.WriteLine(XlRange.Cells[3, j].Value2.ToString() + "/" + XlRange.Cells[i, j].Value2.ToString());
                            row_info[XlRange.Cells[3, j].Value2.ToString()] = XlRange.Cells[i, j].Value2.ToString();
                        }
                    }
                    Material newMaterial = new Material(row_info["Tipo de familia"], Convert.ToInt32(row_info["Precio Unitario del item"]),row_info["Unidad"], row_info["Familia"]);

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
