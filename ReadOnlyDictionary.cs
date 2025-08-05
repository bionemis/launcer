using System;
using System.Collections;
using System.Collections.Generic;

public class ReadOnlyDictionary<a, b> : IDictionary<a, b>, ICollection<KeyValuePair<a, b>>, IEnumerable<KeyValuePair<a, b>>, IEnumerable
{
	private readonly IDictionary<a, b> _dictionary;

	public ICollection<a> Keys => _dictionary.Keys;

	public ICollection<b> Values => _dictionary.Values;

	public b this[a a] => _dictionary[a];

	b IDictionary<a, b>.this[a a]
	{
		get
		{
			return this[a];
		}
		set
		{
			throw ReadOnlyException();
		}
	}

	public int Count => _dictionary.Count;

	public bool IsReadOnly => true;

	public ReadOnlyDictionary(IDictionary<a, b> a)
	{
		_dictionary = a;
	}

	void IDictionary<a, b>.Add(a a, b b)
	{
		throw ReadOnlyException();
	}

	public bool ContainsKey(a a)
	{
		return _dictionary.ContainsKey(a);
	}

	bool IDictionary<a, b>.Remove(a a)
	{
		throw ReadOnlyException();
	}

	public bool TryGetValue(a a, out b b)
	{
		return _dictionary.TryGetValue(a, out b);
	}

	void ICollection<KeyValuePair<a, b>>.Add(KeyValuePair<a, b> a)
	{
		throw ReadOnlyException();
	}

	void ICollection<KeyValuePair<a, b>>.Clear()
	{
		throw ReadOnlyException();
	}

	public bool Contains(KeyValuePair<a, b> a)
	{
		return _dictionary.Contains(a);
	}

	public void CopyTo(KeyValuePair<a, b>[] a, int b)
	{
		_dictionary.CopyTo(a, b);
	}

	bool ICollection<KeyValuePair<a, b>>.Remove(KeyValuePair<a, b> a)
	{
		throw ReadOnlyException();
	}

	public IEnumerator<KeyValuePair<a, b>> GetEnumerator()
	{
		return _dictionary.GetEnumerator();
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return GetEnumerator();
	}

	private static Exception ReadOnlyException()
	{
		return new NotSupportedException("This dictionary is read-only");
	}
}
