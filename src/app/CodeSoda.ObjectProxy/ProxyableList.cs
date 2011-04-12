using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeSoda.ObjectProxy
{
	public class LazyList<T> : IList<T>
	{

		private readonly List<T> _list;

		public LazyList()
		{
			this._list = new List<T>();
		}

		virtual public IEnumerator<T> GetEnumerator()
		{
			return _list.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		virtual public void Add(T item)
		{
			_list.Add(item);
		}

		virtual public void Clear()
		{
			_list.Clear();
		}

		virtual public bool Contains(T item)
		{
			return _list.Contains(item);
		}

		virtual public void CopyTo(T[] array, int arrayIndex)
		{
			_list.CopyTo(array, arrayIndex);
		}

		virtual public bool Remove(T item)
		{
			return _list.Remove(item);
		}

		virtual public int Count
		{
			get { return _list.Count; }
		}

		virtual public bool IsReadOnly
		{
			get { return ((ICollection<T>)_list).IsReadOnly; }
		}

		virtual public int IndexOf(T item)
		{
			return _list.IndexOf(item);
		}

		virtual public void Insert(int index, T item)
		{
			_list.Insert(index, item);
		}

		virtual public void RemoveAt(int index)
		{
			_list.RemoveAt(index);
		}

		virtual public T this[int index]
		{
			get { return _list[index]; }
			set { _list[index] = value; }
		}

	}
}
