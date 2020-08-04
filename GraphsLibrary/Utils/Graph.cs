using GraphsLibrary.GraphElements;
using System.Collections.Generic;

namespace GraphsLibrary.Utils
{
    public class Graph
    {
        public Graph()
        {
            Edges = new ElementContainer<Edge>();
            Vertices = new ElementContainer<Vertex>();
            Vertices.ElementRemoved += Vertices_ElementRemoved;
        }

        public ElementContainer<Edge> Edges { get; }

        public ElementContainer<Vertex> Vertices { get; }

        private void Vertices_ElementRemoved(Vertex vertex)
        {
            List<Edge> edges = new List<Edge>(Edges);
            foreach (Edge edge in edges)
            {
                if (edge.Start == vertex || edge.End == vertex)
                {
                    Edges.Remove(edge);
                }
            }
        }
    }
}
