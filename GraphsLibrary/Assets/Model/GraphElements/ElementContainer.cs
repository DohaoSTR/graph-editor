using System;
using System.Collections;
using System.Collections.Generic;

namespace GraphsLibrary.Assets.Model.GraphElements
{
    public class ElementContainer<IElement> : IEnumerable<IElement>
    {
        public IEnumerator<IElement> GetEnumerator()
        {
            foreach (var element in _elements)
            {
                yield return element;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)this).GetEnumerator();

        public event Action<IElement> ElementAdded;

        public event Action<IElement> ElementRemoved;

        private readonly List<IElement> _elements;

        public IElement this[int index]
        {
            set
            {
                _elements[index] = value;
            }
            get
            {
                return _elements[index];
            }
        }

        public int Count => _elements.Count;

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

        public int IndexOf(IElement element) => _elements.IndexOf(element);

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
