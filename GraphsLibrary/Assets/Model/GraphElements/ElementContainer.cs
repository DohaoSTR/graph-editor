using System;
using System.Collections.Generic;

namespace GraphsLibrary.Assets.Model.GraphElements
{
    public class ElementContainer<IElement>
    {
        public event Action<IElement> ElementAdded;

        public event Action<IElement> ElementRemoved;

        private readonly List<IElement> _elements;

        public IReadOnlyCollection<IElement> Elements => _elements;

        public ElementContainer() => _elements = new List<IElement>();

        public ElementContainer(List<IElement> elements) => _elements = elements;

        public void Add(IElement element)
        {
            _elements.Add(element);
            OnAddElement(element);
        }

        public void Remove(IElement element)
        {
            _elements.Remove(element);
            OnRemoveElement(element);
        }

        public int IndexOf(IElement element)
        {
            for (int i = 0; i <= _elements.Count; i++)
            {
                if (_elements[i].Equals(element))
                {
                    return i;
                }
            }
            throw new ArgumentNullException("Элемент не найден!");
        }

        private void OnAddElement(IElement element)
        {
            if (ElementAdded == null)
            {
                return;
            }
            ElementAdded(element);
        }

        private void OnRemoveElement(IElement element)
        {
            if (ElementRemoved == null)
            {
                return;
            }
            ElementRemoved(element);
        }
    }
}
