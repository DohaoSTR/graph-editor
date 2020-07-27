using GraphModel.Assets.Model.GraphElements;
using System.Collections.Generic;

namespace GraphModel.Assets.Model
{
    public class FieldGraph
    {
        public delegate void OnAddElement(IElement element);

        public delegate void OnDeleteElement(IElement element);

        public event OnAddElement AddedElement;

        public event OnDeleteElement DeletedElement;

        public List<Vertex> ListVertices { get; set; }

        public List<Edge> ListEdges { get; set; }

        public FieldGraph()
        {
            ListVertices = new List<Vertex>();
            ListEdges = new List<Edge>();
        }

        public void AddElement(IElement element)
        {
            element.Add();
            AddedElement(element);
        }

        public void RemoveElement(IElement element)
        {
            element.Remove();
            DeletedElement(element);
        }
    }
}
