using System.Collections.Generic;

namespace GraphModel.Assets.Model.GraphElements
{
    public class Edge : Element
    {
        private static readonly List<Edge> _edges = new List<Edge>();

        public static IReadOnlyCollection<Edge> Edges => _edges;
        
        public Vertex First { get; }

        public Vertex Second { get; }

        public Edge(Vertex first, Vertex second)
        {
            First = first;
            Second = second;
        }

        public override void Add() => _edges.Add(this);

        public override void Remove() => _edges.Remove(this);
    }
}
