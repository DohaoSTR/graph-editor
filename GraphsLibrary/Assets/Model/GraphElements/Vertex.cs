using System.Collections.Generic;

namespace GraphModel.Assets.Model.GraphElements
{
    public class Vertex : Element
    {
        private static readonly List<Vertex> _vertices = new List<Vertex>();

        public static IReadOnlyCollection<Vertex> Vertices => _vertices;

        public Point GetPoint { get; }

        public Vertex(Point point) => GetPoint = point;

        public override void Add() => _vertices.Add(this);

        public override void Remove()
        {
            CheckVertexForEdges();
            _vertices.Remove(this);
        }

        private void CheckVertexForEdges()
        {
            List<Edge> edges = new List<Edge>(Edge.Edges);
            foreach (Edge edge in edges)
            {
                if (edge.Start == this || edge.End == this)
                {
                    edge.Remove();
                }
            }
        }
    }
}
