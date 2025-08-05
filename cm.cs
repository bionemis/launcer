using System;
using System.Collections;
using System.Collections.Generic;
using PathologicalGames;
using UnityEngine;

public class cm : IDictionary<string, SpawnPool>, ICollection<KeyValuePair<string, SpawnPool>>, IEnumerable<KeyValuePair<string, SpawnPool>>, IEnumerable
{
	public delegate void a(SpawnPool a);

	internal Dictionary<string, a> kd = new Dictionary<string, a>();

	private Dictionary<string, SpawnPool> ke = new Dictionary<string, SpawnPool>();

	public int Count => ke.Count;

	public SpawnPool this[string a]
	{
		get
		{
			try
			{
				return ke[a];
			}
			catch (KeyNotFoundException)
			{
				throw new KeyNotFoundException($"A Pool with the name '{a}' not found. \nPools={ToString()}");
			}
		}
		set
		{
			throw new NotImplementedException("Cannot set PoolManager.Pools[key] directly. SpawnPools add themselves to PoolManager.Pools when created, so there is no need to set them explicitly. Create pools using PoolManager.Pools.Create() or add a SpawnPool component to a GameObject.");
		}
	}

	public ICollection<string> Keys
	{
		get
		{
			throw new NotImplementedException("If you need this, please request it.");
		}
	}

	public ICollection<SpawnPool> Values
	{
		get
		{
			throw new NotImplementedException("If you need this, please request it.");
		}
	}

	private bool dh => true;

	bool ICollection<KeyValuePair<string, SpawnPool>>.IsReadOnly => true;

	public void vu(string a, a b)
	{
		if (!kd.ContainsKey(a))
		{
			kd.Add(a, b);
			Debug.Log($"Added onCreatedDelegates for pool '{a}': {b.Target}");
		}
		else
		{
			Dictionary<string, a> dictionary = kd;
			dictionary[a] = (a)Delegate.Combine(dictionary[a], b);
		}
	}

	public void vv(string a, a b)
	{
		if (!kd.ContainsKey(a))
		{
			throw new KeyNotFoundException("No OnCreatedDelegates found for pool name '" + a + "'.");
		}
		Dictionary<string, a> dictionary = kd;
		dictionary[a] = (a)Delegate.Remove(dictionary[a], b);
		Debug.Log($"Removed onCreatedDelegates for pool '{a}': {b.Target}");
	}

	public SpawnPool vw(string a)
	{
		return new GameObject(a + "Pool").AddComponent<SpawnPool>();
	}

	public SpawnPool vx(string a, GameObject b)
	{
		if (!vy(a))
		{
			return null;
		}
		string name = b.gameObject.name;
		try
		{
			b.gameObject.name = a;
			return b.AddComponent<SpawnPool>();
		}
		finally
		{
			b.gameObject.name = name;
		}
	}

	private bool vy(string a)
	{
		string text = a.Replace("Pool", "");
		if (text != a)
		{
			Debug.LogWarning($"'{a}' has the word 'Pool' in it. This word is reserved for GameObject defaul naming. The pool name has been changed to '{text}'");
			a = text;
		}
		if (ContainsKey(a))
		{
			Debug.Log($"A pool with the name '{a}' already exists");
			return false;
		}
		return true;
	}

	public override string ToString()
	{
		string[] array = new string[ke.Count];
		ke.Keys.CopyTo(array, 0);
		return string.Format("[{0}]", string.Join(", ", array));
	}

	public bool vz(string a)
	{
		if (!ke.TryGetValue(a, out var value))
		{
			Debug.LogError($"PoolManager: Unable to destroy '{a}'. Not in PoolManager");
			return false;
		}
		UnityEngine.Object.Destroy(value.gameObject);
		ke.Remove(value.poolName);
		return true;
	}

	public void wa()
	{
		foreach (KeyValuePair<string, SpawnPool> item in ke)
		{
			Debug.Log("DESTROYING: " + item.Value.gameObject.name);
			UnityEngine.Object.Destroy(item.Value.gameObject);
		}
		ke.Clear();
	}

	internal void wb(SpawnPool a)
	{
		if (ContainsKey(a.poolName))
		{
			Debug.LogError($"A pool with the name '{a.poolName}' already exists. This should only happen if a SpawnPool with this name is added to a scene twice.");
			return;
		}
		ke.Add(a.poolName, a);
		Debug.Log($"Added pool '{a.poolName}'");
		if (kd.ContainsKey(a.poolName))
		{
			kd[a.poolName](a);
		}
	}

	public void Add(string a, SpawnPool b)
	{
		throw new NotImplementedException("SpawnPools add themselves to PoolManager.Pools when created, so there is no need to Add() them explicitly. Create pools using PoolManager.Pools.Create() or add a SpawnPool component to a GameObject.");
	}

	internal bool wc(SpawnPool a)
	{
		if (!wd(a) & Application.isPlaying)
		{
			Debug.LogError($"PoolManager: Unable to remove '{a.poolName}'. Pool not in PoolManager");
			return false;
		}
		ke.Remove(a.poolName);
		return true;
	}

	public bool Remove(string a)
	{
		throw new NotImplementedException("SpawnPools can only be destroyed, not removed and kept alive outside of PoolManager. There are only 2 legal ways to destroy a SpawnPool: Destroy the GameObject directly, if you have a reference, or use PoolManager.Destroy(string poolName).");
	}

	public bool ContainsKey(string a)
	{
		return ke.ContainsKey(a);
	}

	public bool wd(SpawnPool a)
	{
		return ke.ContainsValue(a);
	}

	public bool TryGetValue(string a, out SpawnPool b)
	{
		return ke.TryGetValue(a, out b);
	}

	public bool Contains(KeyValuePair<string, SpawnPool> a)
	{
		throw new NotImplementedException("Use PoolManager.Pools.ContainsKey(string poolName) or PoolManager.Pools.ContainsValue(SpawnPool pool) instead.");
	}

	public void Add(KeyValuePair<string, SpawnPool> a)
	{
		throw new NotImplementedException("SpawnPools add themselves to PoolManager.Pools when created, so there is no need to Add() them explicitly. Create pools using PoolManager.Pools.Create() or add a SpawnPool component to a GameObject.");
	}

	public void Clear()
	{
		throw new NotImplementedException("Use PoolManager.Pools.DestroyAll() instead.");
	}

	private void wg(KeyValuePair<string, SpawnPool>[] a, int b)
	{
		throw new NotImplementedException("PoolManager.Pools cannot be copied");
	}

	private void wh(KeyValuePair<string, SpawnPool>[] a, int b)
	{
		throw new NotImplementedException("PoolManager.Pools cannot be copied");
	}

	void ICollection<KeyValuePair<string, SpawnPool>>.CopyTo(KeyValuePair<string, SpawnPool>[] a, int b)
	{
		//ILSpy generated this explicit interface implementation from .override directive in wh
		this.wh(a, b);
	}

	public bool Remove(KeyValuePair<string, SpawnPool> a)
	{
		throw new NotImplementedException("SpawnPools can only be destroyed, not removed and kept alive outside of PoolManager. There are only 2 legal ways to destroy a SpawnPool: Destroy the GameObject directly, if you have a reference, or use PoolManager.Destroy(string poolName).");
	}

	public IEnumerator<KeyValuePair<string, SpawnPool>> GetEnumerator()
	{
		return ke.GetEnumerator();
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return ke.GetEnumerator();
	}
}
