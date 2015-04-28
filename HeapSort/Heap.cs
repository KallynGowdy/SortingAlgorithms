using System;
using System.Collections;
using System.Collections.Generic;

namespace HeapSort
{
    /// <summary>
    /// Defines a heap of comparable items.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Heap<T> : IList<T> where T : IComparable<T>
    {
        private List<T> internalList = new List<T>();

        /// <summary>
        /// Gets the index of the parent node that owns the node at the given index.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public int GetParentIndex(int index)
        {
            return (index - 1)/2;
        }

        /// <summary>
        /// Gets the index of the left child for the node at the given index.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public int GetLeftChildIndex(int index)
        {
            return (index*2) + 1;
        }

        /// <summary>
        /// Gets the index of the right child for the node at the given index.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public int GetRightChildIndex(int index)
        {
            return (index * 2) + 2;
        }

        /// <summary>
        /// Gets the parent for the given node.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public T GetParent(int index)
        {
            return this[GetParentIndex(index)];
        }

        /// <summary>
        /// Gets the right child for the node at the given index.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public T GetRightChild(int index)
        {
            return this[GetRightChildIndex(index)];
        }

        /// <summary>
        /// Gets the left child for the node at the given index.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public T GetLeftChild(int index)
        {
            return this[GetLeftChildIndex(index)];
        }

        public IEnumerator<T> GetEnumerator()
        {
            return internalList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Adds the given item as a child of the given node.
        /// </summary>
        /// <param name="item"></param>
        /// <param name="nodeIndex"></param>
        private void AddInternal(T item, int nodeIndex)
        {
            
        }

        public void Add(T item)
        {
            for (int i = 0; i < Count; i++)
            {
                
            }
        }

        public void Clear()
        {
            internalList.Clear();
        }

        public bool Contains(T item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public bool Remove(T item)
        {
            throw new NotImplementedException();
        }

        public int Count
        {
            get { return internalList.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public int IndexOf(T item)
        {
            return internalList.IndexOf(item);
        }

        public void Insert(int index, T item)
        {
            throw new NotImplementedException();
        }

        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        public T this[int index]
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }
    }
}