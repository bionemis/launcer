using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cn : IDictionary<string, Transform>, ICollection<KeyValuePair<string, Transform>>, IEnumerable<KeyValuePair<string, Transform>>, IEnumerable
{
	private Dictionary<string, Transform> kz = new Dictionary<string, Transform>();

	public int Count => kz.Count;

	public Transform this[string a]
	{
		get
		{
			try
			{
				return kz[a];
			}
			catch (KeyNotFoundException)
			{
				throw new KeyNotFoundException($"A Prefab with the name '{a}' not found. \nPrefabs={ToString()}");
			}
		}
		set
		{
			throw new NotImplementedException("Read-only.");
		}
	}

	public ICollection<string> Keys => kz.Keys;

	public ICollection<Transform> Values => kz.Values;

	private bool dh => true;

	bool ICollection<KeyValuePair<string, Transform>>.IsReadOnly => true;

	public override string ToString()
	{
		string[] array = new string[kz.Count];
		kz.Keys.CopyTo(array, 0);
		return string.Format("[{0}]", string.Join(", ", array));
	}

	internal void zd(string a, Transform b)
	{
		kz.Add(a, b);
	}

	public bool ze(string a)
	{
		return kz.Remove(a);
	}

	internal void zf()
	{
		kz.Clear();
	}

	public bool ContainsKey(string a)
	{
		return kz.ContainsKey(a);
	}

	public bool TryGetValue(string a, out Transform b)
	{
		return kz.TryGetValue(a, out b);
	}

	public void Add(string a, Transform b)
	{
		throw new NotImplementedException("Read-Only");
	}

	public bool Remove(string a)
	{
		throw new NotImplementedException("Read-Only");
	}

	public bool Contains(KeyValuePair<string, Transform> a)
	{
		throw new NotImplementedException("Use Contains(string prefabName) instead.");
	}

	public void Add(KeyValuePair<string, Transform> a)
	{
		throw new NotImplementedException("Read-only");
	}

	public void Clear()
	{
		throw new NotImplementedException();
	}

	private void zi(KeyValuePair<string, Transform>[] a, int b)
	{
		throw new NotImplementedException("Cannot be copied");
	}

	private void zj(KeyValuePair<string, Transform>[] a, int b)
	{
		throw new NotImplementedException("Cannot be copied");
	}

	void ICollection<KeyValuePair<string, Transform>>.CopyTo(KeyValuePair<string, Transform>[] a, int b)
	{
		//ILSpy generated this explicit interface implementation from .override directive in zj
		this.zj(a, b);
	}

	public bool Remove(KeyValuePair<string, Transform> a)
	{
		throw new NotImplementedException("Read-only");
	}

	public IEnumerator<KeyValuePair<string, Transform>> GetEnumerator()
	{
		return kz.GetEnumerator();
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return kz.GetEnumerator();
	}
}
