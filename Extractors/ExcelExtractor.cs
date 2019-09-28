using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

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

        public List<Dictionary<string, string>> Extract()
        {
            List<Dictionary<string, string>> rows_info = new List<Dictionary<string, string>>();

            for (int i = 1; i <= rowCount; i++)
            {
                if (XlRange.Cells[i, 2] != null && XlRange.Cells[i, 2].Value2 != null)
                {
                    Dictionary<string, string> row_info = new Dictionary<string, string>();
                    for (int j = 1; j <= colCount; j++)
                    {
                        if (XlRange.Cells[i, j] != null && XlRange.Cells[i, j].Value2 != null)
                        {
                            row_info[XlRange.Cells[3, j].Value2.ToString()] = XlRange.Cells[i, j].Value2.ToString();
                        }
                    }
                    rows_info.Add(row_info);
                }                
            }
            return rows_info;
        }



    }
}
