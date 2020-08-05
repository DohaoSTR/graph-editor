using System;
using System.Collections;
using System.Collections.Generic;

namespace GraphsLibrary.GraphElements
{
    public class ElementContainer<Element> : IEnumerable<Element>
    {
        private readonly List<Element> _elements;

        public ElementContainer() => _elements = new List<Element>();

        public ElementContainer(ICollection<Element> elements) => _elements = (List<Element>)elements;

        public event Action<Element> ElementAdded;

        public event Action<Element> ElementRemoved;

        public IEnumerator<Element> GetEnumerator()
        {
            foreach (var element in _elements)
            {
                yield return element;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)this).GetEnumerator();

        public Element this[int index]
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

        public void Add(Element element)
        {
            _elements.Add(element);
            OnAddElement(element);
        }

        public void Remove(Element element)
        {
            _elements.Remove(element);
            OnRemoveElement(element);
        }

        public int IndexOf(Element element) => _elements.IndexOf(element);

        private void OnAddElement(Element element)
        {
            if (ElementAdded == null)
            {
                return;
            }
            ElementAdded(element);
        }

        private void OnRemoveElement(Element element)
        {
            if (ElementRemoved == null)
            {
                return;
            }
            ElementRemoved(element);
        }
    }
}
