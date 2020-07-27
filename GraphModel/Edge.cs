namespace GraphModel
{
    public class Edge
    {
        public Vertex First { get; set; }

        public Vertex Second { get; set; }

        public Edge(Vertex first, Vertex second)
        {
            First = first;
            Second = second;
        }
    }
}
