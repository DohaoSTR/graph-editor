namespace GraphsLibrary.GraphElements
{
    public class Edge
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