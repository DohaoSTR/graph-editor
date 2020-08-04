using GraphsLibrary.Assets.Model.Utils;

namespace GraphsLibrary.Assets.Model.GraphElements
{
    public class Vertex : Element
    {
        public Vertex(Point point) => Point = point;

        public Point Point { get; }
    }
}
