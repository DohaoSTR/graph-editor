using GraphModel.GraphElements;

namespace GraphModel
{
    public class Vertex : GraphElement
    {
        public Point GetPoint { get; }

        public Vertex(Point point)
        {
            GetPoint = point;
        }
    }
}
