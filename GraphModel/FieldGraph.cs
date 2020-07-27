using System.Collections.Generic;

namespace GraphModel
{
    public class FieldGraph
    {
        public delegate void OnAddElement(object graphElement);

        public delegate void OnDeleteElement(object graphElement);

        public event OnAddElement AddedElement;

        public event OnDeleteElement DeletedElement;

        public List<Vertex> ListVertices { get; set; }

        public List<Edge> ListEdges { get; set; }

        public FieldGraph()
        {
            ListVertices = new List<Vertex>();
            ListEdges = new List<Edge>();
        }

        public void AddElement(Vertex vertex)
        {
            ListVertices.Add(vertex);
            AddedElement(vertex);
        }

        public void DeleteElement(Vertex vertex)
        {
            CheckVertexForEdges(vertex);
            ListVertices.Remove(vertex);
            DeletedElement(vertex);
        }

        private void CheckVertexForEdges(Vertex vertex)
        {
            foreach (Edge edge in ListEdges)
            {
                if (edge.First == vertex || edge.Second == vertex)
                {
                    DeleteElement(edge);
                }
            }
        }
    }
}
