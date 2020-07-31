using GraphsLibrary.Assets.Model.Utils;
using System;
using System.Collections.Generic;

namespace GraphModel.Assets.Model.GraphElements
{
    public class Vertex : Element
    {
        private static readonly List<Vertex> _vertices = new List<Vertex>();

        public static IReadOnlyCollection<Vertex> Vertices => _vertices;

        public int GetNumber
        {
            get
            {
                for (int i = 0; i <= _vertices.Count; i++)
                {
                    if (_vertices[i] == this)
                    {
                        return i;
                    }
                }
                throw new ArgumentNullException("Вершина не найдена!");
            }
        }

        public Point GetPoint { get; }

        public Vertex(Point point) => GetPoint = point;

        public override void Add() => _vertices.Add(this);

        public override void Remove()
        {
            RemoveEdges();
            _vertices.Remove(this);
        }

        private void RemoveEdges()
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
