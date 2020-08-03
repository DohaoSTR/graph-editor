using System.Collections.Generic;

namespace GraphModel.Assets.Model.GraphElements
{
    public class Edge : Element
    {
        public Vertex Start { get; }

        public Vertex End { get; }

        public Edge(Vertex first, Vertex second)
        {
            Start = first;
            End = second;
        }
    }
}