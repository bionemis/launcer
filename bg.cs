using System;
using UnityEngine;

public class bg
{
	private bf em;

	public Action<byte> en;

	private byte eo;

	public Action<byte> ep;

	private byte eq;

	public Action er;

	private byte es;

	public void jd()
	{
		em = new bf();
		if (em.@is())
		{
			Debug.Log("连接串口成功");
			em.ek = jf;
			em.eg = je;
		}
	}

	private void je(bool a)
	{
	}

	public void Update()
	{
		if (!(MainControl.a == null))
		{
			em?.Update();
		}
	}

	private void jf(byte[] a)
	{
		byte b2 = a[5];
		byte b3 = a[8];
		em.ix(b2, es);
		if (es != b3 && b2 > 0)
		{
			es = b3;
			er?.Invoke();
			MainControl.LobbyPanel.ta(b2);
		}
		eo = jg(a[6], eo, 1);
		eq = jg(a[7], eq, 2);
	}

	private byte jg(byte a, byte b, int c)
	{
		if (a == 0 && b == 0)
		{
			return a;
		}
		switch (c)
		{
		case 1:
			en?.Invoke(a);
			break;
		case 2:
			ep?.Invoke(a);
			break;
		}
		return a;
	}

	public void jh(byte a)
	{
		em.iy(a);
	}

	public void Close()
	{
		try
		{
			em?.Close();
		}
		catch (Exception ex)
		{
			k.ca("Serial:" + ex.Message);
		}
	}
}
