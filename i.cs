using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class i
{
	public Task s;

	public CancellationTokenSource n { get; private set; }

	public CancellationToken o { get; private set; }

	public i()
	{
		n = new CancellationTokenSource();
		o = n.Token;
	}

	public void Close()
	{
		try
		{
			if (n != null)
			{
				n.Cancel();
				n.Dispose();
				n = null;
			}
		}
		catch (Exception ex)
		{
			Debug.LogError(ex.Message);
		}
	}
}
