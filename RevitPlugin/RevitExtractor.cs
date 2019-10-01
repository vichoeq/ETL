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
    /// Extrae la información relevante del archivo .rvt para integrarla con el resto del proyecto
    /// </summary>
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]        
    public class RevitExtractor : IExternalCommand
    {
        private string excelPath = "";
        private string projectPath = "";

        /// <summary>
        /// Comando ejecutado desde la aplicación de Autodesk Revit
        /// </summary>
        /// <param name="commandData">Información sobre la aplicación que lanzó el comando.</param>
        /// <param name="message">Mensaje especial. No nos interesa.</param>
        /// <param name="elements">Elementos seleccionados. No nos interesa.</param>
        /// <returns>Éxito, Fracaso o "Cancelado" según como salieron las cosas</returns>
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication app = commandData.Application;
            Document activeDoc = app.ActiveUIDocument.Document;
            
            while(true)
            {
                TaskDialog mainDialog = new TaskDialog("Revit Extractor")
                {
                    MainInstruction = "Bienvenidos!",
                    MainContent = "Ingresa los archivos Excel y Project de tu proyecto"
                };
                
                bool ready = true;

                string excelDialog;
                if(string.Equals(excelPath, ""))
                {
                    ready = false;
                    excelDialog = "Selecciona archivo Excel";
                }
                else
                {
                    excelDialog = "EXCEL: " + Path.GetFileNameWithoutExtension(excelPath) + ". ¿Escoger otro?";
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
                    projectDialog = "PROJECT: " + Path.GetFileNameWithoutExtension(projectPath) + ". ¿Escoger otro?";
                }

                mainDialog.AddCommandLink(TaskDialogCommandLinkId.CommandLink2, projectDialog);

                if(ready)
                {
                    mainDialog.AddCommandLink(TaskDialogCommandLinkId.CommandLink3, "Exportar");
                }

                TaskDialogResult dialogResult = mainDialog.Show();

                mainDialog.Dispose();

                switch(dialogResult)
                {
                    case TaskDialogResult.CommandLink1:
                        excelPath = ChooseFile("Excel files|*.xlsm;xls");
                        break;
                    case TaskDialogResult.CommandLink2:
                        projectPath = ChooseFile("Project files|*.mpp");
                        break;
                    case TaskDialogResult.CommandLink3:
                        return ExtractAndTransform(excelPath, projectPath, activeDoc);
                    case TaskDialogResult.Close:
                        return Result.Cancelled;
                    default:
                        return Result.Failed;
                }
            }
        }

        private Result ExtractAndTransform(string excelPath, string projectPath, Document revit)
        {                  

            // ExcelExtractor excelExtractor = new ExcelExtractor(excelPath);

            // MicrosoftProjectFile mpp = new MicrosoftProjectFile(projectPath);

            // Hace cosas del ETL

            TaskDialog mainDialog = new TaskDialog("Revit Extractor")
            {
                MainInstruction = "Archivos integrados exitosamente"
            };

            mainDialog.Show();

            return Result.Succeeded;
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
