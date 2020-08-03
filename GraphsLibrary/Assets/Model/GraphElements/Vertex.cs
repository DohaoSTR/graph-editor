using GraphsLibrary.Assets.Model.Utils;

namespace GraphModel.Assets.Model.GraphElements
{
    public class Vertex : Element
    {
        public Point GetPoint { get; }

        public Vertex(Point point) => GetPoint = point;
    }
}
