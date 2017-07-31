using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Autodesk.Revit.DB;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.UI;

namespace ConnectCable
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]

    
    public class connectCable : IExternalCommand
    {
        Document doc;

        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication app = commandData.Application;
            doc = app.ActiveUIDocument.Document;

            Autodesk.Revit.UI.Selection.Selection sel = app.ActiveUIDocument.Selection;

            /**
            IList<Reference> PoleList = sel.PickObjects(Autodesk.Revit.UI.Selection.ObjectType.Element, "请选择要连接的两个电线杆");

            if (PoleList.Count != 2)
            {
                message = "未选中两个电线杆";
                return Result.Failed;
            }

            FamilyInstance pole1 = doc.GetElement(PoleList[0]) as FamilyInstance;
            FamilyInstance pole2 = doc.GetElement(PoleList[1]) as FamilyInstance;
             * */

            List<XYZ> pts = new List<XYZ>();
            for (int i = 0; i < 4; i++)
            {
                XYZ p = sel.PickPoint(Autodesk.Revit.UI.Selection.ObjectSnapTypes.Endpoints);
                pts.Add(p);
            }

            Transaction trans = new Transaction(doc);
            trans.Start("创建自适应电缆");

            FamilySymbol fs = getSymbolType(doc, "自适应电缆");
            if (fs != null)
            {
                fs.Activate();
                CreateAdaptiveComponentInstance(doc, fs, pts);
            }
            else
                TaskDialog.Show("2", "oops");

            trans.Commit();

            return Result.Succeeded;
        }

        public void CreateAdaptiveComponentInstance(Document document, FamilySymbol symbol, List<XYZ> points)
        {
            // Create a new instance of an adaptive component family  
            FamilyInstance instance = AdaptiveComponentInstanceUtils.CreateAdaptiveComponentInstance(document, symbol);

            // Get the placement points of this instance  
            IList<ElementId> placePointIds = new List<ElementId>();
            placePointIds = AdaptiveComponentInstanceUtils.GetInstancePlacementPointElementRefIds(instance);

            // Set the position of each placement point  
            for (int i = 0; i < placePointIds.Count; i++)
            {
                ReferencePoint point = document.GetElement(placePointIds[i]) as ReferencePoint;
                point.Position = points[i];
            }
        }

        public FamilySymbol getSymbolType(Document doc, string name)
        {
            FilteredElementIdIterator workWellItrator = new FilteredElementCollector(doc).OfClass(typeof(Family)).GetElementIdIterator();
            workWellItrator.Reset();
            FamilySymbol getsymbol = null;
            while (workWellItrator.MoveNext())
            {
                Family family = doc.GetElement(workWellItrator.Current) as Family;
                foreach (ElementId id in family.GetFamilySymbolIds())
                {
                    FamilySymbol symbol = doc.GetElement(id) as FamilySymbol;
                    if (symbol.Name == name)
                    {
                        getsymbol = symbol;
                    }
                }
            }
            return getsymbol;

        }
    }
}
