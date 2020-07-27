using GraphModel.GraphElements;
using System.Collections.Generic;

namespace GraphModel
{
    public class Edge : GraphElement
    {
        public Vertex First { get; set; }

        public Vertex Second { get; set; }

        public Edge(Vertex first, Vertex second)
        {
            First = first;
            Second = second;
        }
    }
}
