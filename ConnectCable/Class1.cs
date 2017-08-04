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
        IList<XYZ> SZG2_27_front;
        IList<XYZ> SZG2_27_back;
        IList<XYZ> SZG2_30_front;
        IList<XYZ> SZG2_30_back;
        IList<XYZ> SJG4_27_half;


        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication app = commandData.Application;
            doc = app.ActiveUIDocument.Document;

            #region 初始化连接点坐标
            SJG1_27_front = new List<XYZ>();
            SJG1_27_front.Add(new XYZ(-7.1353, -3.2294, 126.69)); SJG1_27_front.Add(new XYZ(7.1353, -3.2315, 126.69)); SJG1_27_front.Add(new XYZ(-8.4480, -12.4879, 112.58)); SJG1_27_front.Add(new XYZ(8.4490, -12.4924, 112.58));
            SJG1_27_front.Add(new XYZ(-10.0884, -12.4879, 100.11)); SJG1_27_front.Add(new XYZ(10.0884, -12.4879, 100.11)); SJG1_27_front.Add(new XYZ(-8.4490, -12.4924, 87.65)); SJG1_27_front.Add(new XYZ(8.4490, -12.4925, 87.65));
            SJG1_27_back = new List<XYZ>();
            SJG1_27_back.Add(new XYZ(-7.1353, 3.2376, 126.69)); SJG1_27_back.Add(new XYZ(7.1353, 3.2376, 126.69)); SJG1_27_back.Add(new XYZ(-8.4480, 12.4961, 112.58)); SJG1_27_back.Add(new XYZ(8.4490, 12.4924, 112.58));
            SJG1_27_back.Add(new XYZ(-10.0894, 12.5006, 100.11)); SJG1_27_back.Add(new XYZ(10.0894, 12.4924, 100.11)); SJG1_27_back.Add(new XYZ(-8.4490, 12.4925, 87.65)); SJG1_27_back.Add(new XYZ(8.4490, 12.4926, 87.65));

            SJG4_27_front = new List<XYZ>();
            SJG4_27_front.Add(new XYZ(-8.4477, -3.2318, 126.67)); SJG4_27_front.Add(new XYZ(6.8073, -3.2379, 126.67)); SJG4_27_front.Add(new XYZ(-9.7518, -12.4929, 112.58)); SJG4_27_front.Add(new XYZ(8.1155, -12.4929, 112.58));
            SJG4_27_front.Add(new XYZ(-11.3922, -12.4929, 100.11)); SJG4_27_front.Add(new XYZ(9.7559, -12.4929, 100.11)); SJG4_27_front.Add(new XYZ(-9.7518, -12.4929, 87.74)); SJG4_27_front.Add(new XYZ(8.1155, -12.4929, 87.74));
            SJG4_27_back = new List<XYZ>();
            SJG4_27_back.Add(new XYZ(-8.4477, 3.2379, 126.67)); SJG4_27_back.Add(new XYZ(6.8073, 3.2379, 126.67)); SJG4_27_back.Add(new XYZ(-9.7518, 12.4929, 112.58)); SJG4_27_back.Add(new XYZ(8.1155, 12.4930, 112.58));
            SJG4_27_back.Add(new XYZ(-11.3922, 12.4930, 100.11)); SJG4_27_back.Add(new XYZ(9.7559, 12.4930, 100.11)); SJG4_27_back.Add(new XYZ(-9.7518, 12.4929, 87.74)); SJG4_27_back.Add(new XYZ(8.1155, 12.4930, 87.74));

            SZG2_27_front = new List<XYZ>();
            SZG2_27_front.Add(new XYZ(-6.4797,-0.45,116.12));SZG2_27_front.Add(new XYZ(6.4797,-0.45,116.12));SZG2_27_front.Add(new XYZ(-8.7762,-0.45,106.21));SZG2_27_front.Add(new XYZ(8.7762,-0.45,106.21));
            SZG2_27_front.Add(new XYZ(-10.4167,-0.45,93.42));SZG2_27_front.Add(new XYZ(10.4167,-0.45,93.42));SZG2_27_front.Add(new XYZ(-8.7762,-0.45,80.63));SZG2_27_front.Add(new XYZ(8.7762,-0.45,80.63));      
            SZG2_27_back = new List<XYZ>();
            SZG2_27_back.Add(new XYZ(-6.4797,0.45,116.12));SZG2_27_back.Add(new XYZ(6.4797,0.45,116.12));SZG2_27_back.Add(new XYZ(-8.7762,0.45,106.21));SZG2_27_back.Add(new XYZ(8.7762,0.45,106.21));
            SZG2_27_back.Add(new XYZ(-10.4167,0.45,93.42));SZG2_27_back.Add(new XYZ(10.4167,0.45,93.42));SZG2_27_back.Add(new XYZ(-8.7762,0.45,80.63));SZG2_27_back.Add(new XYZ(8.7762,0.45,80.63));

            SZG2_30_front = new List<XYZ>();
            SZG2_30_front.Add(new XYZ(-7.1358,-0.45,128.13));SZG2_30_front.Add(new XYZ(7.1358,-0.45,128.13));SZG2_30_front.Add(new XYZ(-8.4482,-0.45,115.99));SZG2_30_front.Add(new XYZ(8.4482,-0.45,115.99));
            SZG2_30_front.Add(new XYZ(-10.089,-0.45,103.2));SZG2_30_front.Add(new XYZ(10.089,-0.45,103.2));SZG2_30_front.Add(new XYZ(-8.4479,-0.45,90.4));SZG2_30_front.Add(new XYZ(8.4479,-0.45,90.4));
            SZG2_30_back = new List<XYZ>();
            SZG2_30_back.Add(new XYZ(-7.1358, 0.45, 128.13)); SZG2_30_back.Add(new XYZ(7.1358, 0.45, 128.13)); SZG2_30_back.Add(new XYZ(-8.4482, 0.45, 115.99)); SZG2_30_back.Add(new XYZ(8.4482, 0.45, 115.99));
            SZG2_30_back.Add(new XYZ(-10.089, 0.45, 103.2)); SZG2_30_back.Add(new XYZ(10.089, 0.49, 103.2)); SZG2_30_back.Add(new XYZ(-8.4479, 0.45, 90.4)); SZG2_30_back.Add(new XYZ(8.4479, 0.45, 90.4));

            SJG4_27_half = new List<XYZ>();
            SJG4_27_half.Add(new XYZ(-8.4477, 2.5982, 126.67)); SJG4_27_half.Add(new XYZ(6.8073, 2.5982, 126.67)); SJG4_27_half.Add(new XYZ(-9.7527, 10.7049, 113.56)); SJG4_27_half.Add(new XYZ(8.1164, 10.7049, 113.56));
            SJG4_27_half.Add(new XYZ(-11.3931, 10.7049, 101.1)); SJG4_27_half.Add(new XYZ(9.7568, 10.7049, 101.1)); SJG4_27_half.Add(new XYZ(-9.7527, 10.7049, 88.73)); SJG4_27_half.Add(new XYZ(8.1164, 10.7049, 88.73));

            #endregion

            Autodesk.Revit.UI.Selection.Selection sel = app.ActiveUIDocument.Selection;
            PoleFilter pf = new PoleFilter();
            IList<Reference> PoleList = sel.PickObjects(Autodesk.Revit.UI.Selection.ObjectType.Element, pf, "请选择要连接的两个电线杆");

            if (PoleList.Count != 2)
            {
                message = "未选中两个电线杆";
                return Result.Failed;
            }

            FamilyInstance pole1 = doc.GetElement(PoleList[0]) as FamilyInstance;
            FamilyInstance pole2 = doc.GetElement(PoleList[1]) as FamilyInstance;
            
            //保证电线杆前进方向一致
            if(pole1.FacingOrientation.DotProduct(pole2.FacingOrientation)<0){
                Transaction trans1 = new Transaction(doc);
                trans1.Start("旋转电线杆");
                Line axis = Line.CreateUnbound((pole2.Location as LocationPoint).Point,XYZ.BasisZ);
                ElementTransformUtils.RotateElement(doc, pole2.Id, axis, Math.PI);
                trans1.Commit();
            }            

            Transaction trans = new Transaction(doc);
            trans.Start("创建自适应电缆");

            FamilySymbol fs = getSymbolType(doc, "自适应电缆");
            if (fs != null)
            {
                fs.Activate();
                if (inRightOrder(pole1, pole2))
                    CreateAdaptiveComponentInstance(doc, fs, TransformToModelXYZ(pole1, Direction.back), TransformToModelXYZ(pole2, Direction.front));
                else
                    CreateAdaptiveComponentInstance(doc, fs, TransformToModelXYZ(pole1, Direction.front), TransformToModelXYZ(pole2, Direction.back));
            }
            else
            {
                message = "创建电缆失败";
                return Result.Failed;
            }

            trans.Commit();

            return Result.Succeeded;
        }

        /// <summary>
        /// 创建自适应电缆实例，两个list连接相同index的点
        /// </summary>
        /// <param name="document">项目文件</param>
        /// <param name="symbol">要创建自适应电缆的symbol</param>
        /// <param name="startpoints">起始点集合</param>
        /// <param name="endpoints">终点集合</param>
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

        /// <summary>
        /// 根据名称查找familysymbol
        /// </summary>
        /// <param name="doc">项目文件</param>
        /// <param name="name">symbol名称</param>
        /// <returns>familysymbol</returns>
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

        /// <summary>
        /// 将电线杆族文件中的点转换为项目中的点
        /// </summary>
        /// <param name="fi">项目中电线杆族实例</param>
        /// <param name="dir">电线杆连接的方向：front or back</param>
        /// <returns>电线杆对应方向的连接点在项目文件中的坐标</returns>
        public IList<XYZ> TransformToModelXYZ(FamilyInstance fi, Direction dir)
        {
            IList<XYZ> originPt = findConnectionPoints(fi, dir);
            if (originPt == null)
                return null;

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

        /// <summary>
        /// 判断两个电线杆连接方式是否为pole1到pole2
        /// </summary>
        /// <param name="fi1">电线杆实例1</param>
        /// <param name="fi2">电线杆实例2</param>
        /// <returns>是否同向</returns>
        public bool inRightOrder(FamilyInstance fi1, FamilyInstance fi2)
        {
            XYZ e1 = (fi2.Location as LocationPoint).Point - (fi1.Location as LocationPoint).Point;
            XYZ e2 = fi1.FacingOrientation;
            if (e1.DotProduct(e2) >= 0)
                return true;
            return false;
        }

        /// <summary>
        /// 根据实例的族类型找到对应的预设族坐标集合
        /// </summary>
        /// <param name="fi">需要进行查找的族实例</param>
        /// <param name="dir">连接点所在的方向</param>
        /// <returns>族文件中连接点的坐标集合</returns>
        public IList<XYZ> findConnectionPoints(FamilyInstance fi,Direction dir){
            string name = fi.Symbol.Family.Name;
            switch (name)
            {
                case ("1GGE4-SJG4-27 电缆登杆"):
                    return SJG4_27_half;
                case("1GGE4-SJG1-27 整体"):
                    if (dir == Direction.front)
                        return SJG1_27_front;
                    else
                        return SJG1_27_back;
                case("1GGE4-SJG4-27 整体"):
                    if (dir == Direction.front)
                        return SJG4_27_front;
                    else
                        return SJG4_27_back;
                case("1GGE4-SZG2-27-整体"):
                    if (dir == Direction.front)
                        return SZG2_27_front;
                    else
                        return SZG2_27_back;
                case("1GGE4-SZG2-30-整体"):
                    if (dir == Direction.front)
                        return SZG2_30_front;
                    else
                        return SZG2_30_back;
            }
            return null;
        }

        /// <summary>
        /// 两种方向，front or back
        /// </summary>
        public enum Direction
        {
            front,
            back,
        }
    }


    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    public class ConnectSkippingLine : IExternalCommand
    {
        Document doc;
        IList<XYZ> SJG1_27;
        IList<XYZ> SJG4_27;

        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication app = commandData.Application;
            doc = app.ActiveUIDocument.Document;
            Autodesk.Revit.UI.Selection.Selection sel = app.ActiveUIDocument.Selection;

            SJG1_27 = new List<XYZ>();
            SJG1_27.Add(new XYZ(-8.4480, -12.4879, 112.58)); SJG1_27.Add(new XYZ(8.4490, -12.4924, 112.58));
            SJG1_27.Add(new XYZ(-10.0884, -12.4879, 100.11)); SJG1_27.Add(new XYZ(10.0884, -12.4879, 100.11)); SJG1_27.Add(new XYZ(-8.4490, -12.4924, 87.65)); SJG1_27.Add(new XYZ(8.4490, -12.4925, 87.65));
            SJG1_27.Add(new XYZ(-8.4480, 12.4961, 112.58)); SJG1_27.Add(new XYZ(8.4490, 12.4924, 112.58));
            SJG1_27.Add(new XYZ(-10.0894, 12.5006, 100.11)); SJG1_27.Add(new XYZ(10.0894, 12.4924, 100.11)); SJG1_27.Add(new XYZ(-8.4490, 12.4925, 87.65)); SJG1_27.Add(new XYZ(8.4490, 12.4926, 87.65));

            SJG4_27 = new List<XYZ>();
            SJG4_27.Add(new XYZ(-9.7518, -12.4929, 112.58)); SJG4_27.Add(new XYZ(8.1155, -12.4929, 112.58));
            SJG4_27.Add(new XYZ(-11.3922, -12.4929, 100.11)); SJG4_27.Add(new XYZ(9.7559, -12.4929, 100.11)); SJG4_27.Add(new XYZ(-9.7518, -12.4929, 87.74)); SJG4_27.Add(new XYZ(8.1155, -12.4929, 87.74));
            SJG4_27.Add(new XYZ(-9.7518, 12.4929, 112.58)); SJG4_27.Add(new XYZ(8.1155, 12.4930, 112.58));
            SJG4_27.Add(new XYZ(-11.3922, 12.4930, 100.11)); SJG4_27.Add(new XYZ(9.7559, 12.4930, 100.11)); SJG4_27.Add(new XYZ(-9.7518, 12.4929, 87.74)); SJG4_27.Add(new XYZ(8.1155, 12.4930, 87.74));

            PoleFilter1 pf = new PoleFilter1();
            Reference PoleRf = sel.PickObject(Autodesk.Revit.UI.Selection.ObjectType.Element, pf, "请选择要连接的两个电线杆");

            if (PoleRf == null)
            {
                message = "未选中电线杆";
                return Result.Failed;
            }

            FamilyInstance pole = doc.GetElement(PoleRf) as FamilyInstance;

            Transaction trans = new Transaction(doc);

            FamilySymbol fs = getSymbolType(doc, "跳线");
            fs.Activate();

            IList<XYZ> PointsOnPole = findConnectionPoints(pole);
            if(fs!=null){
                trans.Start("创建跳线");
                for (int i = 0; i < PointsOnPole.Count; i++)
                    CreateSkippingLine(doc, fs, pole, PointsOnPole[i]);
                trans.Commit();
                return Result.Succeeded;
            }
            return Result.Failed;

        }

        public void CreateSkippingLine(Document doc, FamilySymbol fs, FamilyInstance pole,XYZ connectPt)
        {
            FamilyInstance instance = AdaptiveComponentInstanceUtils.CreateAdaptiveComponentInstance(doc, fs);

            IList<ElementId> placePointIds = new List<ElementId>();
            placePointIds = AdaptiveComponentInstanceUtils.GetInstancePlacementPointElementRefIds(instance);

            if (placePointIds.Count == 2)
            {
                ReferencePoint refpt1 = doc.GetElement(placePointIds[0]) as ReferencePoint;
                ReferencePoint refpt2 = doc.GetElement(placePointIds[1]) as ReferencePoint;

                if(connectPt.Y>0)
                    refpt1.Position = TransformToModelXYZ(pole, new XYZ(connectPt.X,connectPt.Y-1.73228,connectPt.Z-0.82677));           
                else
                    refpt1.Position =  TransformToModelXYZ(pole,new XYZ(connectPt.X,connectPt.Y+1.73228,connectPt.Z-0.82677));

                if (connectPt.X > 0)
                {
                    if (connectPt.Y > 0)
                        refpt2.Position = TransformToModelXYZ(pole, new XYZ(connectPt.X - 0.11155, 0.261, connectPt.Z - 5.145));
                    else
                        refpt2.Position = TransformToModelXYZ(pole, new XYZ(connectPt.X - 0.11155, -0.261, connectPt.Z - 5.145));
                }
                else
                {
                    if (connectPt.Y > 0)
                        refpt2.Position = TransformToModelXYZ(pole, new XYZ(connectPt.X + 0.11155, 0.261, connectPt.Z - 5.145));
                    else
                        refpt2.Position = TransformToModelXYZ(pole, new XYZ(connectPt.X + 0.11155, -0.261, connectPt.Z - 5.145));
                }       
            }
        }

        public XYZ TransformToModelXYZ(FamilyInstance fi,XYZ origin)
        {
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
            if (trans != null)  
                return trans.OfPoint(origin);
            return null;
        }

        public IList<XYZ> findConnectionPoints(FamilyInstance fi)
        {
            string name = fi.Symbol.Family.Name;
            switch (name)
            {
                case ("1GGE4-SJG1-27 整体"):
                    return SJG1_27;
                case ("1GGE4-SJG4-27 整体"):
                    return SJG4_27;
            }
            return null;
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


    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    public class FindCoordinate : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication app = commandData.Application;
            Document doc = app.ActiveUIDocument.Document;
            Autodesk.Revit.UI.Selection.Selection sel = app.ActiveUIDocument.Selection;

            for (int i = 0; i < 16; i++)
            {
                XYZ pt = sel.PickPoint();
                TaskDialog.Show("1", pt.ToString());

            }
            return Result.Succeeded;
        }
    }


    public class PoleFilter : Autodesk.Revit.UI.Selection.ISelectionFilter
    {

        public bool AllowElement(Element elem)
        {
            List<string> familyName = new List<string>();
            string[] fn = { "1GGE4-SJG1-27 整体", "1GGE4-SJG4-27 整体", "1GGE4-SZG2-27-整体", "1GGE4-SZG2-30-整体", "1GGE4-SJG4-27 电缆登杆" };
            familyName.AddRange(fn);

            if (elem is FamilyInstance)
            {
                FamilyInstance f = elem as FamilyInstance;
                return familyName.Contains(f.Symbol.Family.Name);
            }
            return false;
        }

        public bool AllowReference(Reference reference, XYZ position)
        {
            return true;
        }
    }

    public class PoleFilter1 : Autodesk.Revit.UI.Selection.ISelectionFilter
    {

        public bool AllowElement(Element elem)
        {
            List<string> familyName = new List<string>();
            string[] fn = { "1GGE4-SJG1-27 整体", "1GGE4-SJG4-27 整体" };
            familyName.AddRange(fn);

            if (elem is FamilyInstance)
            {
                FamilyInstance f = elem as FamilyInstance;
                return familyName.Contains(f.Symbol.Family.Name);
            }
            return false;
        }

        public bool AllowReference(Reference reference, XYZ position)
        {
            return true;
        }
    }
}
