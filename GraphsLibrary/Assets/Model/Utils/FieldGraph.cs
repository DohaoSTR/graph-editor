using GraphModel.Assets.Model.GraphElements;
using System;

namespace GraphsLibrary.Assets.Model.Utils
{
    public class FieldGraph
    {
        public event Action<IElement> AddedElement;

        public event Action<IElement> RemovedElement;

        public void AddElement(IElement element)
        {
            element.Add();
            OnAddElement(element);
        }

        public void RemoveElement(IElement element)
        {
            element.Remove();
            OnRemoveElement(element);
        }

        private void OnAddElement(IElement element)
        {
            if (AddedElement == null)
                return;
            AddedElement(element);
        }

        private void OnRemoveElement(IElement element)
        {
            if (RemovedElement == null)
                return;
            RemovedElement(element);
        }
    }
}
