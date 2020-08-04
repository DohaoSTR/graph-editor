namespace GraphsLibrary.GraphElements
{
    public class Vertex : Element
    {
        public Vertex(Point point) => Point = point;

        public Point Point { get; }
    }
}
