using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using Extractors;

namespace CIPYCS
{
    /// <summary>
    /// 
    /// </summary>
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]        
    public class Extractor : IExternalCommand
    {
        private string excelPath = "";
        private string projectPath = "";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="commandData"></param>
        /// <param name="message"></param>
        /// <param name="elements"></param>
        /// <returns></returns>
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            Document activeDoc = commandData.Application.ActiveUIDocument.Document;

            bool done = false;

            while(!done)
            {
                TaskDialog mainDialog = new TaskDialog("CIPYCS Extractor");
                mainDialog.MainInstruction = "Bienvenidos!";
                mainDialog.MainContent =
                 "Ingresa los archivos Excel y Project de tu proyecto";

                bool ready = true;

                string excelDialog;
                if(string.Equals(excelPath, ""))
                {
                    ready = false;
                    excelDialog = "Selecciona archivo Excel";
                }
                else
                {
                    excelDialog = "" + Path.GetFileName(excelPath) + " seleccionado. ¿Escoger otro?";
                }
                
                mainDialog.AddCommandLink(TaskDialogCommandLinkId.CommandLink1, excelDialog);

                string projectDialog;
                if(string.Equals(projectPath, ""))
                {
                    ready = false;
                    projectDialog = "Selecciona archivo Project";
                }
                else
                {
                    projectDialog = "" + Path.GetFileName(projectPath) + " seleccionado. ¿Escoger otro?";
                }

                mainDialog.AddCommandLink(TaskDialogCommandLinkId.CommandLink2, projectDialog);

                if(ready)
                {
                    mainDialog.AddCommandLink(TaskDialogCommandLinkId.CommandLink3, "Exportar");
                }

                TaskDialogResult tResult = mainDialog.Show();

                switch(tResult)
                {
                    case TaskDialogResult.CommandLink1:
                        excelPath = ChooseFile("Excel files|*.xlsm;xls");
                        break;
                    case TaskDialogResult.CommandLink2:
                        projectPath = ChooseFile("Project files|*.mpp");
                        break;
                    case TaskDialogResult.CommandLink3:
                        ExtractAndTransform(excelPath, projectPath, activeDoc, ".");
                        done = true;
                        break;
                    default:
                        throw new Exception("Comando inválido");
                }
            }

            return Result.Succeeded;
        }

        private void ExtractAndTransform(string excelPath, string projectPath, Document revit, string outputPath)
        {
            // ExcelExtractor excelExtractor = new ExcelExtractor(excelPath);

            // MicrosoftProjectFile mpp = new MicrosoftProjectFile(projectPath);
            
            // Hace cosas del ETL
        }

        private string ChooseFile(string format)
        {
            FileOpenDialog openFileDialog = new FileOpenDialog(format);
            openFileDialog.Show();
            ModelPath userPath = openFileDialog.GetSelectedModelPath();
            ModelPathUtils.ConvertModelPathToUserVisiblePath(userPath);
            return ModelPathUtils.ConvertModelPathToUserVisiblePath(userPath);
        }

    }

}
