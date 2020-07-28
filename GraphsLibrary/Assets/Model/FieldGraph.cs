using GraphModel.Assets.Model.GraphElements;

namespace GraphModel.Assets.Model
{
    public class FieldGraph
    {
        public delegate void OnAddElement(IElement element);

        public delegate void OnDeleteElement(IElement element);

        public event OnAddElement AddedElement;

        public event OnDeleteElement DeletedElement;

        public void AddElement(IElement element)
        {
            element.Add();
            AddedElement(element);
        }

        public void RemoveElement(IElement element)
        {
            element.Remove();
            DeletedElement(element);
        }
    }
}
