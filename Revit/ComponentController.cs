﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ARUP.IssueTracker.Windows;
using System.Windows;
using Autodesk.Revit.DB;
using System.IO;
using Autodesk.Revit.UI;
using ARUP.IssueTracker.Classes.BCF2;
using System.Xml.Serialization;
using System.Windows.Controls;
using Autodesk.Revit.UI.Selection;

namespace ARUP.IssueTracker.Revit
{
    public class ComponentController : IComponentController
    {
        private RevitWindow window;
        private Document doc;
        private UIDocument uidoc;

        public ComponentController(RevitWindow window) 
        {
            this.window = window;
            this.uidoc = window.uiapp.ActiveUIDocument;
            this.doc = window.uiapp.ActiveUIDocument.Document;
        }

        public void selectElements(List<Component> components)
        {
#if REVIT2014
            SelElementSet elementsToBeSelected = SelElementSet.Create();
            components.ForEach(component =>
            {

                Element e = doc.GetElement(new ElementId(int.Parse(component.AuthoringToolId)));
                if (e != null)
                    elementsToBeSelected.Add(e);

            });
            uidoc.Selection.Elements = elementsToBeSelected;
            uidoc.RefreshActiveView();
#else
            List<ElementId> elementsToBeSelected = new List<ElementId>();
            components.ForEach(component => elementsToBeSelected.Add(new ElementId(int.Parse(component.AuthoringToolId))));
            uidoc.Selection.SetElementIds(elementsToBeSelected);
#endif
        }

    }
}
