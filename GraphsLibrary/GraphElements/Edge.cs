namespace GraphsLibrary.GraphElements
{
    public class Edge : Element
    {
        public Edge(Vertex start, Vertex end)
        {
            Start = start;
            End = end;
        }

        public Vertex Start { get; }

        public Vertex End { get; }
    }
}