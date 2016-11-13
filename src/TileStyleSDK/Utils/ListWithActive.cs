using System;
using System.Collections.Generic;

namespace TileStyleSDK
{
    /// <summary>
    /// Represents a data structure which is a List wrapper with added capability to support
    /// a single index being "active" at a time. This is used in several places throughout the program (Tilesets, 
    /// Palettes, Buttons etc.) and can also be used for other programs which utilise the SDK (e.g. an A* pathfinding
    /// class would have a list of Nodes with a single Node active at a time)
    /// </summary>
    public class ListWithActive<T>
    {
        /// <summary>
        /// The active list item.
        /// </summary>
        private T _active;

        /// <summary>
        /// The index of the active list item. This is used to speed up the process of
        /// changing the active list item as directly accessing an index is far quicker 
        /// than comparing each index to find the index of the active one.
        /// </summary>
        private int _activeIndex;

        /// <summary>
        /// The list.
        /// </summary>
        private readonly List<T> _list = new List<T>();

        /// <summary>
        /// Gets or sets the active list item. Quickest changing is done via the
        /// Previous() and Next() methods, but if this property is used to change the
        /// active list item, the list will need to re-evaluate the index of the active item.
        /// </summary>
        /// <value>The active list item.</value>
        public T Active
        {
            get
            {
                return _active;
            }

            set
            {
                _active = value;
                _activeIndex = _list.IndexOf(value);
            }
        } 

        /// <summary>
        /// Gets or sets the list item at the specified index.
        /// Default property indexer allows items to be accessed with 
        /// ListObject[index] rather than ListObject.List[index]
        /// </summary>
        /// <param name="index">The index.</param>
        public T this[int index]
        {
            get {return _list[index];}
            set {_list[index] = value;}
        }

        /// <summary>
        /// Gets the list
        /// </summary>
        /// <value>The list.</value>
        public List<T> List
        {
            get {return _list;}
        }

        /// <summary>
        /// Wrapper for returning the number of items in the list.
        /// </summary>
        public int Count
        {
            get {return _list.Count;}
        }

        /// <summary>
        /// Wrapper for adding an item to the list.
        /// </summary>
        public void Add(T toAdd)
        {
            _list.Add(toAdd);
        }

        /// <summary>
        /// Wrapper for removing an item from the list.
        /// </summary>
        public void Remove(T toRemove)
        {
            _list.Remove(toRemove);
        }

        /// <summary>
        /// Wrapper for determining if the list contains a specified item.
        /// </summary>
        public bool Contains(T item)
        {
            return _list.Contains(item);
        }

        /// <summary>
        /// Changes the active item to the next one in the list.
        /// Supports continuous scrolling.
        /// </summary>
        public void Next()
        {
            if (_activeIndex < _list.Count - 1 && _list.Count > 1)
                _active = _list[++_activeIndex];
            else
            {
                _active = _list[0];
                _activeIndex = 0;
            }
        }

        /// <summary>
        /// Changes the active item to the previous one in the list.
        /// Supports continuous scrolling.
        /// </summary>
        public void Previous()
        {
            if (_activeIndex > 0)
            {
                _active = _list[--_activeIndex];
            }
            else
            {
                _activeIndex = _list.Count - 1;
                _active = _list[_activeIndex];
            }
        }
    }
}