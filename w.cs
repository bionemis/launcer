using System;
using System.Collections.Generic;

public class w
{
	public delegate void a(object a);

	public Dictionary<ushort, LinkedList<a>> cp = new Dictionary<ushort, LinkedList<a>>();

	public List<v> cq = new List<v>();

	public Dictionary<string, Action<object>> cr = new Dictionary<string, Action<object>>();

	public void gs(ushort a, a b)
	{
		LinkedList<a> value = null;
		cp.TryGetValue(a, out value);
		if (value == null)
		{
			value = new LinkedList<a>();
			cp[a] = value;
		}
		value.AddLast(b);
	}

	public void gt(string a, Action<object> b)
	{
		if (cr.ContainsKey(a))
		{
			Dictionary<string, Action<object>> dictionary = cr;
			dictionary[a] = (Action<object>)Delegate.Combine(dictionary[a], b);
		}
		else
		{
			cr[a] = b;
		}
	}

	public void gu(ushort a, a b)
	{
		LinkedList<a> value = null;
		cp.TryGetValue(a, out value);
		if (value != null)
		{
			value.Remove(b);
			if (value.Count == 0)
			{
				cp.Remove(a);
			}
		}
	}

	public void gv(string a, Action<object> b)
	{
		if (cr.ContainsKey(a))
		{
			Dictionary<string, Action<object>> dictionary = cr;
			dictionary[a] = (Action<object>)Delegate.Remove(dictionary[a], b);
			if (cr[a] == null)
			{
				cr.Remove(a);
			}
		}
	}

	public void Update()
	{
		for (int num = 0; num < 10; num++)
		{
			if (cq.Count <= 0)
			{
				break;
			}
			gw(cq[0]);
			lock (cq)
			{
				cq.RemoveAt(0);
			}
		}
	}

	private void gw(v a)
	{
		if (cr.ContainsKey(a.Name))
		{
			cr[a.Name](a);
		}
	}

	public void gx(ushort a, object b)
	{
		LinkedList<a> value = null;
		cp.TryGetValue(a, out value);
		if (value != null)
		{
			for (LinkedListNode<a> linkedListNode = value.First; linkedListNode != null; linkedListNode = linkedListNode.Next)
			{
				linkedListNode.Value?.Invoke(b);
			}
		}
	}

	public void gy(ushort a)
	{
		gx(a, null);
	}

	public void Close()
	{
		cp.Clear();
	}
}
