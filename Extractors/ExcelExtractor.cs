using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;
using ProjectModel;

namespace Extractors
{
    public class ExcelExtractor
    {
        public string Path;
        public Excel.Range XlRange;
        int rowCount;
        int colCount;

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

        public List<Material> Extract()
        {
            List<Material> rows_info = new List<Material>();

            for (int i = 1; i <= rowCount; i++)
            {
                if (XlRange.Cells[i, 2] != null && XlRange.Cells[i, 2].Value2 != null)
                {
                    //the data is preprocess by a dictionary so the parser is agnostic to the order of the columns
                    Dictionary<string, string> row_info = new Dictionary<string, string>();
                    for (int j = 1; j <= colCount; j++)
                    {
                        if (XlRange.Cells[i, j] != null && XlRange.Cells[i, j].Value2 != null)
                        {
                            row_info[XlRange.Cells[3, j].Value2.ToString()] = XlRange.Cells[i, j].Value2.ToString();
                        }
                    }
                    Material newMaterial = new Material(row_info["Tipo de familia"], Convert.ToInt32(row_info["Precio Unitario del item"]),
                                                        row_info["Unidad"], row_info["Familia"]);
                    rows_info.Add(newMaterial);
                }                
            }
            return rows_info;
        }



    }
}
