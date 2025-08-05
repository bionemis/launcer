using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Threading;
using UnityEngine;

public class bf
{
	private SerialPort dz;

	private Thread ea;

	private List<byte> eb;

	private int ec = 1;

	private int ed;

	private int ee;

	private Queue<byte[]> ef;

	public Action<bool> eg;

	private float ei;

	private float ej = 5f;

	public Action<byte[]> ek;

	private byte el = 1;

	public bool br { get; private set; }

	public bf()
	{
		dz = new SerialPort("COM1", 115200, Parity.None, 8, StopBits.One);
		eb = new List<byte>();
		ef = new Queue<byte[]>();
	}

	public bool @is()
	{
		try
		{
			dz.Open();
			ea = new Thread(iz);
			ea.IsBackground = true;
			ea.Start();
			iw();
			iy(1);
			return true;
		}
		catch (Exception ex)
		{
			k.ca(ex.Message);
			return false;
		}
	}

	public void Update()
	{
		while (ef.Count > 0)
		{
			if (br)
			{
				Debug.Log("重新连接回调！");
				br = false;
				eg?.Invoke(obj: false);
			}
			byte[] obj = ef.Dequeue();
			ek?.Invoke(obj);
		}
		ei += Time.deltaTime;
		if (ei >= ej)
		{
			ei = 0f;
			iw();
			if (!br)
			{
				br = true;
				eg?.Invoke(obj: true);
			}
		}
	}

	private void it(byte[] a)
	{
		dz.Write(a, 0, a.Length);
	}

	public void iu(byte a, byte b, byte c, byte d)
	{
		it(iv(a, b, c, d));
	}

	private byte[] iv(byte a, byte b, byte c, byte d)
	{
		byte b2 = (byte)((241 + a + b + c + d) % 256);
		byte[] obj = new byte[10] { 255, 76, 7, 241, 0, 0, 0, 0, 0, 143 };
		obj[4] = a;
		obj[5] = b;
		obj[6] = c;
		obj[7] = d;
		obj[8] = b2;
		return obj;
	}

	private void iw()
	{
		iu(0, 0, 0, 0);
	}

	public void ix(byte a, byte b)
	{
		iu(el, a, b, 0);
	}

	public void iy(byte a)
	{
		el = a;
	}

	private void iz()
	{
		while (true)
		{
			int bytesToRead = dz.BytesToRead;
			byte[] array = new byte[bytesToRead];
			dz.Read(array, 0, bytesToRead);
			if (array != null && array.Length != 0)
			{
				ei = 0f;
				byte[] array2 = array;
				foreach (byte b2 in array2)
				{
					ja(b2);
				}
			}
			Thread.Sleep(1);
		}
	}

	private void ja(byte a)
	{
		switch (ec)
		{
		case 1:
			ee = 0;
			ed = 0;
			eb.Clear();
			jb(a == byte.MaxValue, a);
			break;
		case 2:
			jb(a == 76, a);
			break;
		case 3:
			eb.Add(a);
			ed = a;
			ec++;
			break;
		default:
			jc(a);
			break;
		}
	}

	private void jb(bool a, byte b)
	{
		if (a)
		{
			eb.Add(b);
			ec++;
			return;
		}
		eb.Clear();
		ec = 1;
		if (b == byte.MaxValue)
		{
			ja(b);
		}
	}

	private void jc(byte a)
	{
		if (ed <= 0 && ec < 4)
		{
			ec = 1;
		}
		else if (ec - 4 == ed)
		{
			eb.Add(a);
			if (ee == a)
			{
				ec++;
			}
			else
			{
				ec = 1;
			}
		}
		else if (ec - 3 == ed)
		{
			eb.Add(a);
			if (a == 143)
			{
				if (eb.Count <= 0)
				{
					return;
				}
				ef.Enqueue(eb.ToArray());
			}
			ed = 0;
			ec = 1;
		}
		else
		{
			eb.Add(a);
			ee += a;
			ec++;
		}
	}

	public void Close()
	{
		try
		{
			if (ef != null)
			{
				ef.Clear();
				ef = null;
			}
			if (ea != null)
			{
				ea.Abort();
				ea = null;
			}
			if (dz != null)
			{
				dz.Close();
				dz = null;
			}
		}
		catch (Exception)
		{
			throw;
		}
	}
}
