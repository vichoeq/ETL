using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
namespace Lab1PlaceGroup
{

    [Transaction(TransactionMode.Manual)]

    [Regeneration(RegenerationOption.Manual)]

    public class Class1 : IExternalCommand

    {

        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)

        {
            // crear aplicación
            Application app = commandData.Application.Application;
            Document activeDoc = commandData.Application.ActiveUIDocument.Document;

            // Ventana inicial 
            TaskDialog mainDialog = new TaskDialog("Hello");
            mainDialog.MainInstruction = "Bienvenidos!";
            mainDialog.MainContent =
             "Ingresa los archivos Excel y Project de tu proyecto.";
            mainDialog.AddCommandLink(TaskDialogCommandLinkId.CommandLink1,
                          "Seleccione archivo Excel");
            mainDialog.AddCommandLink(TaskDialogCommandLinkId.CommandLink2,
                                      "Seleccione archivo Project");

            TaskDialogResult tResult = mainDialog.Show();

            // Según opción seleccionada abre archivo con OpenFileDialog
            if (TaskDialogResult.CommandLink1 == tResult)
                {
                FileOpenDialog openFileDialog1 = new FileOpenDialog("Excel files|*.xlsm;xls");
                openFileDialog1.Show();
                ModelPath userPath1 = openFileDialog1.GetSelectedModelPath();

                //Se pasa ModelPath a string
                ModelPathUtils.ConvertModelPathToUserVisiblePath(userPath1);
                TaskDialog userDialog1 = new TaskDialog("Archivo");
                userDialog1.MainContent = "Seleccionaste " + userPath1;
                userDialog1.Show();
            }

            else if (TaskDialogResult.CommandLink2 == tResult)
            {
                FileOpenDialog openFileDialog2 = new FileOpenDialog("Project files|*.mpp");
                openFileDialog2.Show();
                ModelPath userPath2 =  openFileDialog2.GetSelectedModelPath();
                ModelPathUtils.ConvertModelPathToUserVisiblePath(userPath2);
                TaskDialog userDialog2 = new TaskDialog("Archivo seleccionado");
                userDialog2.MainContent = "Seleccionaste " + userPath2;
                userDialog2.Show();

            }


            return Autodesk.Revit.UI.Result.Succeeded;

        }

    }

}
