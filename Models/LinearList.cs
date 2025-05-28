using System.Collections.Generic;
using System.Linq;



namespace LinearListApp.Models
{
    public class LinearList<T>
    {
        private readonly List<T> _items;
        private int _currentIndex;

        // Constructor (empty initialization)
        public LinearList()
        {
            _items = new List<T>();
            _currentIndex = -1; // -1 means no current element yet
        }

        // Current element property
        public T CurrentElement => !IsEmpty && _currentIndex >= 0 ? _items[_currentIndex] : default!;

        // Count property
        public int Count => _items.Count;

        // IsEmpty property
        public bool IsEmpty => !_items.Any();

        // Add element method
        public void Add(T item)
        {
            _items.Add(item);
            if (_currentIndex == -1)
                _currentIndex = 0; // Set current element to the first added item
        }

        // Remove element method
        public bool Remove(T item)
        {
            int indexToRemove = _items.IndexOf(item);
            if (indexToRemove == -1)
                return false;

            _items.RemoveAt(indexToRemove);

            if (!_items.Any())
            {
                _currentIndex = -1; // reset if empty
            }
            else if (_currentIndex >= _items.Count)
            {
                _currentIndex = _items.Count - 1;
            }

            return true;
        }

        // Move to next element method
        public bool MoveNext()
        {
            if (_currentIndex >= 0 && _currentIndex < _items.Count - 1)
            {
                _currentIndex++;
                return true;
            }
            return false; // No next element
        }

        // Move to first element method
        public void MoveFirst()
        {
            if (_items.Any())
                _currentIndex = 0;
        }
    }
}
