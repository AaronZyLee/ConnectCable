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
            throw new NotImplementedException();
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
    }
}
