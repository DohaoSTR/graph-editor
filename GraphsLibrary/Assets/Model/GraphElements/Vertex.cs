using System.Collections.Generic;

namespace GraphModel.Assets.Model.GraphElements
{
    public class Vertex : Element
    {
        public static List<Vertex> GetVertices { get; }

        public Point GetPoint { get; }

        public Vertex(Point point)
        {
            GetPoint = point;
        }

        static Vertex()
        {
            GetVertices = new List<Vertex>();
        }

        public override void Add()
        {
            GetVertices.Add(this);
        }

        public override void Remove()
        {
            CheckVertexForEdges();
            GetVertices.Remove(this);
        }

        private void CheckVertexForEdges()
        {
            foreach (Edge edge in Edge.GetEdges)
            {
                if (edge.First == this || edge.Second == this)
                {
                    Edge.GetEdges.Remove(edge);
                }
            }
        }
    }
}
