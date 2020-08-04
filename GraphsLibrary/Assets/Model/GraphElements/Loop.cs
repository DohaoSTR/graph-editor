namespace GraphsLibrary.Assets.Model.GraphElements
{
    public class Loop : Edge
    {
        public Loop(Vertex vertex) : base(vertex, vertex)
        { }
    }
}
