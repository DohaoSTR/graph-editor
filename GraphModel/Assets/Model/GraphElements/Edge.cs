using System.Collections.Generic;

namespace GraphModel.Assets.Model.GraphElements
{
    public class Edge : Element
    {
        public static List<Edge> GetEdges { get; }
        
        public Vertex First { get; set; }

        public Vertex Second { get; set; }

        public Edge(Vertex first, Vertex second)
        {
            First = first;
            Second = second;
        }

        static Edge()
        {
            GetEdges = new List<Edge>();
        }

        public override void Add()
        {
            GetEdges.Add(this);
        }

        public override void Remove()
        {
            GetEdges.Remove(this);
        }
    }
}
