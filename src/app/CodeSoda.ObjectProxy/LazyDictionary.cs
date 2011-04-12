using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeSoda.ObjectProxy
{
	public class LazyDictionary<TKey, TValue> : IDictionary<TKey, TValue>
	{

		private readonly IDictionary<TKey, TValue> _dictionary;
		private readonly Func<TKey, TValue> _loader;

		public LazyDictionary(Func<TKey, TValue> loader)
		{
			this._loader = loader;
			this._dictionary = new Dictionary<TKey, TValue>();
		}

		public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
		{
			return _dictionary.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public void Add(KeyValuePair<TKey, TValue> item)
		{
			_dictionary.Add(item);
		}

		public void Clear()
		{
			_dictionary.Clear();
		}

		public bool Contains(KeyValuePair<TKey, TValue> item)
		{
			return _dictionary.Contains(item);
		}

		public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
		{
			_dictionary.CopyTo(array, arrayIndex);
		}

		public bool Remove(KeyValuePair<TKey, TValue> item)
		{
			return _dictionary.Remove(item);
		}

		public int Count
		{
			get { return _dictionary.Count; }
		}

		public bool IsReadOnly
		{
			get { return _dictionary.IsReadOnly; }
		}

		public bool ContainsKey(TKey key)
		{
			LoadFromKey(key);
			return _dictionary.ContainsKey(key);
		}

		public void Add(TKey key, TValue value)
		{
			_dictionary.Add(key, value);
		}

		public bool Remove(TKey key)
		{
			return _dictionary.Remove(key);
		}

		public bool TryGetValue(TKey key, out TValue value)
		{
			LoadFromKey(key);
			return _dictionary.TryGetValue(key, out value);
		}

		public TValue this[TKey key]
		{
			get
			{
				LoadFromKey(key);
				return _dictionary[key];
			}
			set { _dictionary[key] = value; }
		}

		public ICollection<TKey> Keys
		{
			get { return _dictionary.Keys; }
		}

		public ICollection<TValue> Values
		{
			get { return _dictionary.Values; }
		}


		private void LoadFromKey(TKey key)
		{
			if (!_dictionary.ContainsKey(key))
				_dictionary.Add(key, _loader(key));
		}
	}

	public class LazyDictionary : IDictionary
	{

		private readonly IDictionary _dictionary;
		private readonly Func<object, object> _loader;

		public LazyDictionary(Func<object, object> loader)
		{
			this._loader = loader;
			this._dictionary = new Hashtable();
		}

		public void Clear()
		{
			_dictionary.Clear();
		}

		public int Count
		{
			get { return _dictionary.Count; }
		}

		public bool IsReadOnly
		{
			get { return _dictionary.IsReadOnly; }
		}

		private void LoadFromKey(object key)
		{
			if (!_dictionary.Contains(key))
				_dictionary.Add(key, _loader(key));
		}

		//private bool ContainsKey(object key)
		//{
		//    foreach(var obj in _dictionary.Keys) {
		//        if (key == obj) return true;
		//    }

		//    return false;
		//}

		#region IDictionary Members

		public void Add(object key, object value)
		{
			_dictionary.Add(key, value);
		}

		public bool Contains(object key)
		{
			LoadFromKey(key);
			return _dictionary.Contains(key);
		}

		IDictionaryEnumerator IDictionary.GetEnumerator()
		{
			return _dictionary.GetEnumerator();
		}

		public bool IsFixedSize
		{
			get { return _dictionary.IsFixedSize; }
		}

		ICollection IDictionary.Keys
		{
			get { return _dictionary.Keys; }
		}

		public void Remove(object key)
		{
			_dictionary.Remove(key);
		}

		ICollection IDictionary.Values
		{
			get { return _dictionary.Values; }
		}

		public object this[object key]
		{
			get
			{
				LoadFromKey(key);
				return _dictionary[key];
			}
			set
			{
				_dictionary[key] = value;
			}
		}

		#endregion

		#region ICollection Members

		public void CopyTo(Array array, int index)
		{
			_dictionary.CopyTo(array, index);
		}

		public bool IsSynchronized
		{
			get { return _dictionary.IsSynchronized; }
		}

		public object SyncRoot
		{
			get { return _dictionary.SyncRoot; }
		}

		#endregion

		#region Implementation of IEnumerable

		public IEnumerator GetEnumerator()
		{
			return _dictionary.GetEnumerator();
		}

		#endregion
	}
}
