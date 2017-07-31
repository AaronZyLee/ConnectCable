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
        IList<XYZ> SJG1_27_front;
        IList<XYZ> SJG1_27_back;
        IList<XYZ> SJG4_27_front;
        IList<XYZ> SJG4_27_back;


        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication app = commandData.Application;
            doc = app.ActiveUIDocument.Document;

            SJG1_27_front = new List<XYZ>();
            SJG1_27_front.Add(new XYZ(-7.1353, -3.2294, 126.69)); SJG1_27_front.Add(new XYZ(7.1353, -3.2315, 126.69)); SJG1_27_front.Add(new XYZ(-8.4480, -12.4879, 112.58)); SJG1_27_front.Add(new XYZ(8.4490, -12.4924, 112.58));
            SJG1_27_front.Add(new XYZ(-10.0884, -12.4879, 100.11)); SJG1_27_front.Add(new XYZ(10.0884, -12.4879, 100.11)); SJG1_27_front.Add(new XYZ(-8.4490, -12.4924, 87.65)); SJG1_27_front.Add(new XYZ(8.4490, -12.4925, 87.65));
            SJG1_27_back = new List<XYZ>();
            SJG1_27_back.Add(new XYZ(-7.1353, 3.2376, 126.69)); SJG1_27_back.Add(new XYZ(7.1353, 3.2376, 126.69)); SJG1_27_back.Add(new XYZ(-8.4480, 12.4961, 112.58)); SJG1_27_back.Add(new XYZ(8.4490, 12.4924, 112.58));
            SJG1_27_back.Add(new XYZ(-10.0894, 12.5006, 100.11)); SJG1_27_back.Add(new XYZ(10.0894, 12.4924, 100.11)); SJG1_27_back.Add(new XYZ(-8.4490, 12.4925, 87.65)); SJG1_27_back.Add(new XYZ(8.4490, 12.4926, 87.65));

            SJG4_27_front = new List<XYZ>();
            SJG4_27_front.Add(new XYZ(-8.4477, -3.2318, 126.67)); SJG4_27_front.Add(new XYZ(6.8073, -3.2379, 126.67)); SJG4_27_front.Add(new XYZ(-9.7518, -12.4929, 112.58)); SJG4_27_front.Add(new XYZ(8.1155, -12.4929, 112.58));
            SJG4_27_front.Add(new XYZ(-11.3922, -12.4929, 100.11)); SJG4_27_front.Add(new XYZ(9.7559, 12.4929, 100.11)); SJG4_27_front.Add(new XYZ(-9.7518, -12.4929, 87.74)); SJG4_27_front.Add(new XYZ(8.1155, -12.4929, 87.74));
            SJG4_27_back = new List<XYZ>();
            SJG4_27_back.Add(new XYZ(-8.4477, 3.2379, 126.67)); SJG4_27_back.Add(new XYZ(6.8073, 3.2379, 126.67)); SJG4_27_back.Add(new XYZ(-9.7518, 12.4929, 112.58)); SJG4_27_back.Add(new XYZ(8.1155, 12.4930, 112.58));
            SJG4_27_back.Add(new XYZ(-11.3922, 12.4930, 100.11)); SJG4_27_back.Add(new XYZ(9.7559, -12.4930, 100.11)); SJG4_27_back.Add(new XYZ(-9.7518, 12.4929, 87.74)); SJG4_27_back.Add(new XYZ(8.1155, 12.4930, 87.74));
            
            Autodesk.Revit.UI.Selection.Selection sel = app.ActiveUIDocument.Selection;

            
            IList<Reference> PoleList = sel.PickObjects(Autodesk.Revit.UI.Selection.ObjectType.Element, "请选择要连接的两个电线杆");

            if (PoleList.Count != 2)
            {
                message = "未选中两个电线杆";
                return Result.Failed;
            }

            FamilyInstance pole1 = doc.GetElement(PoleList[0]) as FamilyInstance;
            FamilyInstance pole2 = doc.GetElement(PoleList[1]) as FamilyInstance;
            
            /**
            List<XYZ> pts = new List<XYZ>();
            for (int i = 0; i < 4; i++)
            {
                XYZ p = sel.PickPoint(Autodesk.Revit.UI.Selection.ObjectSnapTypes.Endpoints);
                pts.Add(p);
            }
            */

            Transaction trans = new Transaction(doc);
            trans.Start("创建自适应电缆");

            FamilySymbol fs = getSymbolType(doc, "自适应电缆");
            if (fs != null)
            {
                fs.Activate();
                CreateAdaptiveComponentInstance(doc, fs, TransformToModelXYZ(pole1,SJG1_27_back),TransformToModelXYZ(pole2,SJG4_27_front));
            }
            else
                TaskDialog.Show("2", "oops");

            trans.Commit();

            return Result.Succeeded;
        }

        public void CreateAdaptiveComponentInstance(Document document, FamilySymbol symbol, IList<XYZ> startpoints, IList<XYZ> endpoints)
        {

            for (int i = 0; i < startpoints.Count; i++)
            {
                // Create a new instance of an adaptive component family  
                FamilyInstance instance = AdaptiveComponentInstanceUtils.CreateAdaptiveComponentInstance(document, symbol);

                // Get the placement points of this instance  
                IList<ElementId> placePointIds = new List<ElementId>();
                placePointIds = AdaptiveComponentInstanceUtils.GetInstancePlacementPointElementRefIds(instance);

                // Set the position of each placement point  
                for (int j = 0; j < placePointIds.Count; j++)
                {
                    ReferencePoint point = document.GetElement(placePointIds[j]) as ReferencePoint;
                    if (j == 0)
                        point.Position = startpoints[i];
                    else
                        point.Position = endpoints[i];
                }
                instance.LookupParameter("水平偏移").Set(0);
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

        public IList<XYZ> TransformToModelXYZ(FamilyInstance fi, IList<XYZ> originPt)
        {
            IList<XYZ> transPt = new List<XYZ>();
            Transform trans = null;
            Options opt = new Options();
            opt.ComputeReferences = false;
            opt.View = doc.ActiveView;
            GeometryElement geoElement = fi.get_Geometry(opt);
            foreach (GeometryObject obj in geoElement)
            {
                if (obj is GeometryInstance)
                {
                    GeometryInstance ins = obj as GeometryInstance;
                    trans = ins.Transform;
                    break;
                }
            }
            if(trans !=null){
                for(int i=0;i<originPt.Count;i++)
                    transPt.Add(trans.OfPoint(originPt[i]));      
                return transPt;
            }
            return null;
        }
    }
}
